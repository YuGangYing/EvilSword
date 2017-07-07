using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using CSV;
using System.Linq;

namespace RogerAssetBundle
{
    public class DownloadingGetter : MonoBehaviour
    {
        private DownloadingExecutor downloadingExecutor;
        private CSVParser csvParser;
        private DownloadingFileFilter downloadingFileFilter;
        private DownloadingFileDataQueueCreator downloadingFileDataQueueCreator;
        private AssetCSVReader assetCSVReader;
        private AssetBundleGetter assetBundleGetter;

        internal UnityAction DownloadingComplete
        {
            get;
            set;
        }

        internal UnityAction<float> DownloadingProgress
        {
            get;
            set;
        }

        internal UnityAction<string, string> DownloadingError
        {
            get;
            set;
        }

        internal void Download()
        {
            Init();
            DownloadCSV();
        }

        private void Init()
        {
            downloadingExecutor = gameObject.AddComponent<DownloadingExecutor>();
            csvParser = gameObject.AddComponent<VersionCSVParser>();
            downloadingFileFilter = gameObject.AddComponent<DownloadingFileFilter>();
            downloadingFileDataQueueCreator = gameObject.AddComponent<DownloadingFileDataQueueCreator>();
            assetCSVReader = gameObject.AddComponent<AssetCSVReader>();

            if (PathConstant.CheckIfExistingVersionCSV())
            {
                FileManager.DeleteFile(PathConstant.CLIENT_SERVER_VERSION_CSV);
                FileManager.DeleteFile(PathConstant.CLIENT_CLIENT_VERSION_CSV);
            }
        }

        private void DownloadAssets()
        {
            csvParser.Parse();

			CsvContext mCsvContext = new CsvContext ();
			IEnumerable<VersionCSVStructure> servers = mCsvContext.Read<VersionCSVStructure> (PathConstant.CLIENT_SERVER_VERSION_CSV);
			IEnumerable<VersionCSVStructure> server_resources = mCsvContext.Read<VersionCSVStructure> (PathConstant.CLIENT_SERVER_RESOURCE_VERSION_CSV);
			List<VersionCSVStructure> filteredVersionCSVStructureList = downloadingFileFilter.Filter(servers);
			List<VersionCSVStructure> filteredResourceVersionCSVStructureList = downloadingFileFilter.Filter(server_resources);
			filteredVersionCSVStructureList.AddRange (filteredResourceVersionCSVStructureList);

            if (filteredVersionCSVStructureList.Count == 0)
            {
                if (DownloadingProgress != null)
                {
                    DownloadingProgress(1);
                }

                DownloadCompleteHandler();
                return;
            }

            downloadingExecutor.DownloadingProgress = progress =>
            {
                if (DownloadingProgress != null)
                {
                    DownloadingProgress(progress);
                }
            };
            downloadingExecutor.DownloadingError = (name, error) =>
            {
                if (DownloadingError != null)
                {
                    DownloadingError(name, error);
                }
            };
            downloadingExecutor.DownloadingComplete = DownloadCompleteHandler;
            Queue<DownloadingFileData> downloadingFileDataQueue = downloadingFileDataQueueCreator.CreateForDownloadingAssets(filteredVersionCSVStructureList);
            downloadingExecutor.StartDownload(downloadingFileDataQueue, true, downloadingFileDataQueueCreator.totalSize);
        }

        private void DownloadCSV()
        {
            downloadingExecutor.DownloadingComplete = DownloadAssets;
            downloadingExecutor.StartDownload(downloadingFileDataQueueCreator.CreateForDownloadingCSV());
        }

        private void DownloadCompleteHandler()
        {
            ParseCSV();
        }

        private void FinishDownloading()
        {
            DisposeObject();
            FileManager.CopyFile(PathConstant.CLIENT_SERVER_VERSION_CSV, PathConstant.CLIENT_CLIENT_VERSION_CSV);
			FileManager.CopyFile(PathConstant.CLIENT_SERVER_RESOURCE_VERSION_CSV, PathConstant.CLIENT_CLIENT_RESOURCE_VERSION_CSV);

            if (DownloadingComplete != null)
            {
                DownloadingComplete();
            }
        }

        private void ParseCSV()
        {
            CsvContext csvContext = new CsvContext();
            VersionCSV.versionCSV = csvContext.Read<VersionCSVStructure>(PathConstant.CLIENT_SERVER_VERSION_CSV);
            if (VersionCSV.versionCSV.FirstOrDefault(x => x.FileName == PathConstant.CSV) != null)
            {
                assetBundleGetter = gameObject.AddComponent<AssetBundleGetter>();
                assetBundleGetter.Get(AssetBundleName.csv.ToString(), loadedAssetBundle =>
                {
                    Destroy(assetBundleGetter);
                    assetCSVReader.Read(loadedAssetBundle.AssetBundle);
                });
            }
            FinishDownloading();
        }

        private void DisposeObject()
        {
            downloadingExecutor.DownloadingComplete = null;
            downloadingExecutor.DownloadingProgress = null;
            downloadingExecutor.DownloadingError = null;
            Object.Destroy(downloadingExecutor);
            Object.Destroy(csvParser);
            Object.Destroy(downloadingFileFilter);
            Object.Destroy(downloadingFileDataQueueCreator);
            Object.Destroy(assetCSVReader);
            Object.Destroy(assetBundleGetter);
        }
    }
}
