using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace RogerAssetBundle
{
	class AssetBundleDownloader : MonoBehaviour
	{
		internal UnityAction<string, LoadedAssetBundle> DownloadComplete
		{
			get;
			set;
		}

		private string[] Dependencies
		{
			get;
			set;
		}

		internal void Load (string assetBundleName, string[] dependencies)
		{
			Dependencies = dependencies;
			StartCoroutine (StartLoad (assetBundleName));
		}

		private IEnumerator StartLoad (string assetBundleName)
		{
			AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.LoadFromFileAsync (PathConstant.CLIENT_ASSETBUNDLES_PATH + assetBundleName);
			yield return assetBundleCreateRequest;
			CreateAndSend (assetBundleName, assetBundleCreateRequest.assetBundle);
		}

		internal void Download (string assetBundleName, string[] dependencies)
		{
			Dependencies = dependencies;
			StartCoroutine (StartDownload (assetBundleName));
		}

		private IEnumerator StartDownload (string assetBundleName)
		{
			string URL = PathConstant.SERVER_ASSETBUNDLES_PATH + assetBundleName;
			var www = new WWW (URL);
			yield return www;

			if (www.isDone && string.IsNullOrEmpty (www.error))
			{
				var assetBundle = www.assetBundle;
				CreateAndSend (assetBundleName, assetBundle);
			}
			else
			{
				Debug.Log (assetBundleName);
				Debug.Log (www.error);
			}

			www.Dispose();
			www = null;
		}

		private void CreateAndSend (string assetBundleName, AssetBundle assetBundle)
		{
			var loadedAssetBundle = new LoadedAssetBundle (assetBundleName, assetBundle, Dependencies);
			AssetBundlePool.Add (assetBundleName, loadedAssetBundle);

			if (DownloadComplete != null)
			{
				DownloadComplete (assetBundleName, loadedAssetBundle);
			}

			Destroy (this);
		}
	}
}
