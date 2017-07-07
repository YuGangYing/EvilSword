using UnityEngine;
using UnityEngine.Events;

namespace RogerAssetBundle
{
	class AssetBundleGetter : MonoBehaviour
	{
		internal AssetBundleDownloaderManager assetBundleDownloaderManager;

		internal void Get (string assetBundleName, UnityAction<LoadedAssetBundle> downloadCompleteCallback = null, string[] dependencies = null)
		{
			LoadedAssetBundle loadedAssetBundle = AssetBundlePool.Get (assetBundleName);

			if (loadedAssetBundle != null)
			{
				loadedAssetBundle.ReferencedCount++;

				if (downloadCompleteCallback != null)
				{
					downloadCompleteCallback (loadedAssetBundle);
				}
			}
			else
			{
				assetBundleDownloaderManager = gameObject.AddComponent<AssetBundleDownloaderManager> ();
				var isAlreadyStartDownloading = assetBundleDownloaderManager.CheckIfAlreadyStartDownloading (assetBundleName, downloadCompleteCallback);

				if (!isAlreadyStartDownloading)
				{
					assetBundleDownloaderManager.Load (assetBundleName, dependencies);
				}
			}
		}
	}
}
