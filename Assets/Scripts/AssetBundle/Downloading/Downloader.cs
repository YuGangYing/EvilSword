using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RogerAssetBundle
{
    class Downloader : MonoBehaviour
    {
        internal string downloaderName;
        private UnityWebRequest www;
        internal DownloadingFileData downloadingFileData;
        private const float INCORRECT_PROGRESS = 0.5f;

        internal UnityAction<Downloader, DownloadingFileData, AssetBundle> downloaderComplete;
        internal UnityAction<Downloader, DownloadingFileData, string> downloaderError;
        private float reachedSize;

        internal void StartDownload(DownloadingFileData downloadingFileData)
        {
            reachedSize = 0;
            this.downloadingFileData = downloadingFileData;
            StartCoroutine(Download());
        }

        internal void ReDownload()
        {
            StartCoroutine(Download());
        }

        private IEnumerator Download()
        {
            www = UnityWebRequest.Get(GetPathFromDownloadingFileType(downloadingFileData.FileType, true) + downloadingFileData.FileName + "?" + Random.Range(0, int.MaxValue));
            yield return www.Send();

            if (www.isDone && string.IsNullOrEmpty(www.error))
            {
                FileManager.WriteAllBytes(GetPathFromDownloadingFileType(downloadingFileData.FileType, false) + downloadingFileData.FileName, www.downloadHandler.data);
                downloaderComplete(this, downloadingFileData, downloadingFileData.IsAssetBundle == 1 ? ((DownloadHandlerAssetBundle)www.downloadHandler).assetBundle : null);
            }
            else
            {
                Debug.Log(www.error);
            }
        }

        private string GetPathFromDownloadingFileType(DownloadingFileTypeEnum downloadingFileTypeEnum, bool isServer)
        {
            if (isServer)
            {
                if (downloadingFileTypeEnum == DownloadingFileTypeEnum.CSV)
                {
                    return PathConstant.SERVER_VERSION_PATH;
                }
                else if (downloadingFileTypeEnum == DownloadingFileTypeEnum.Assets)
                {
                    return PathConstant.SERVER_ASSETBUNDLES_PATH;
                }
            }
            else
            {
                if (downloadingFileTypeEnum == DownloadingFileTypeEnum.CSV)
                {
                    return PathConstant.CLIENT_VERSION_PATH;
                }
                else if (downloadingFileTypeEnum == DownloadingFileTypeEnum.Assets)
                {
                    return PathConstant.CLIENT_ASSETBUNDLES_PATH;
                }
            }

            return null;
        }

        private void Update()
        {
            if (www != null && !string.IsNullOrEmpty(www.error))
            {
                Retry();
            }
        }

        internal float CurrentSize
        {
            get
            {
                // if (string.IsNullOrEmpty(www.error) && www.downloadProgress != INCORRECT_PROGRESS)
                // {
                //     float currentSize = downloadingFileData.FileSize * www.downloadProgress;

                //     if (currentSize > reachedSize)
                //     {
                //         reachedSize = currentSize;
                //     }
                // }

                // return reachedSize;
                return downloadingFileData.FileSize * www.downloadProgress;
            }
        }

        private void OnDestroy()
        {
            www.Dispose();
            www = null;
        }

        private void Retry()
        {
            www.Dispose();
            www = null;
            ReDownload();
        }
    }
}
