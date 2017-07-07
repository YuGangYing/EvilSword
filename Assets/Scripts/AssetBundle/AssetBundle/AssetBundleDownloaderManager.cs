using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RogerAssetBundle
{
	class AssetBundleDownloaderManager : MonoBehaviour
	{
		private static Dictionary<string, List<UnityAction<LoadedAssetBundle>>> DownloadingDictionary
		{
			get;
			set;
		}
	
		static AssetBundleDownloaderManager ()
		{
			DownloadingDictionary = new Dictionary<string, List<UnityAction<LoadedAssetBundle>>>();
		}
	
		internal bool CheckIfAlreadyStartDownloading (string assetBundleName, UnityAction<LoadedAssetBundle> downloadCompleteCallback)
		{
			if (DownloadingDictionary.ContainsKey (assetBundleName))
			{
				List<UnityAction<LoadedAssetBundle>> list;
				DownloadingDictionary.TryGetValue (assetBundleName, out list);
				list.Add (downloadCompleteCallback);
				return true;
			}
			else
			{
				DownloadingDictionary.Add (assetBundleName, new List<UnityAction<LoadedAssetBundle>>
				{
					downloadCompleteCallback
				});
				return false;
			}
		}
	
		internal void Load (string assetBundleName, string[] dependencies)
		{
			var downloader = gameObject.AddComponent<AssetBundleDownloader>();
			downloader.DownloadComplete = (string loadedAssetBundleName, LoadedAssetBundle loadedAssetBundle) =>
			{
				SendEvent (loadedAssetBundleName, loadedAssetBundle);
	
				if (DownloadingDictionary.ContainsKey (loadedAssetBundleName))
				{
					DownloadingDictionary.Remove (loadedAssetBundleName);
				}
			};
			downloader.Load (assetBundleName, dependencies);
		}
	
		private void SendEvent (string assetBundleName, LoadedAssetBundle loadedAssetBundle)
		{
			List<UnityAction<LoadedAssetBundle>> list;
			DownloadingDictionary.TryGetValue (assetBundleName, out list);
			loadedAssetBundle.ReferencedCount = list.Count;
	
			foreach (var downloadComplete in list)
			{
				if (downloadComplete != null)
				{
					downloadComplete (loadedAssetBundle);
				}
			}
	
			Destroy (this);
		}
	}
}
