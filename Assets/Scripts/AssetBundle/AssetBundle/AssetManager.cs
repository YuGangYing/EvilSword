using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RogerAssetBundle
{
	public static class AssetManager
	{
		private const string ASSET_GETTER = "AssetGetter";
		private static GameObject go;
		private static AssetGetter assetGetter;

		public static void UnloadAssetbundle(string assetBundleName,bool isUnloadAll){
			AssetBundlePool.Dispose(assetBundleName,isUnloadAll);
		}

		public static T GetAssetFromLocal<T>(string assetBundleName,string assetName) where T: Object{
			AssetBundle ab = GetAssetBundleFromLocal (assetBundleName);
			if(ab != null){
				return ab.LoadAsset<T> (assetName);
			}
			return null;
		}

		public static T[] GetAssetsFromLocal<T>(string assetBundleName) where T: Object{
			AssetBundle ab = GetAssetBundleFromLocal (assetBundleName);
			if(ab != null){
				return ab.LoadAllAssets<T>();
			}
			return null;
		}

		public static AssetBundle GetAssetBundleFromLocal(string assetBundleName){
			LoadedAssetBundle loadedAssetBundle = AssetBundlePool.Get (assetBundleName);
			if (loadedAssetBundle == null) {
				if(FileManager.Exists(PathConstant.CLIENT_ASSETBUNDLES_PATH + assetBundleName)){
					AssetBundle ab = AssetBundle.LoadFromFile (PathConstant.CLIENT_ASSETBUNDLES_PATH + assetBundleName);
					loadedAssetBundle = new LoadedAssetBundle (assetBundleName, ab, null);
					AssetBundlePool.Add (assetBundleName, loadedAssetBundle);
				}
			}
			if (loadedAssetBundle == null)
				return null;
			return loadedAssetBundle.AssetBundle;
		}

		public static void DisposeAssetBundle(string assetBundleName,bool isUnloadAll = false){
			AssetBundlePool.Dispose (assetBundleName, isUnloadAll);
		}

		public static void GetOrDownloadAssetBundle(string assetBundleName,UnityAction<AssetBundle> completeCallback){
			GameObject go = new GameObject ("AssetBundleGetter");
			AssetBundleGetter assetBundleGetter = go.AddComponent<AssetBundleGetter> ();
			assetBundleGetter.Get (assetBundleName, (LoadedAssetBundle ab)=>{
				completeCallback(ab.AssetBundle);
			}, null);
		}

		public static void Get (string assetBundleName, string assetName, UnityAction<Object> loadAssetCompleteCallback)
		{
			PrepareGet();
			assetGetter.Get (assetBundleName, assetName, loadAssetCompleteCallback);
		}

		public static void Get<T> (string assetBundleName, string assetName, UnityAction<Object> loadAssetCompleteCallback)
		{
			PrepareGet();
			assetGetter.Get<T> (assetBundleName, assetName, loadAssetCompleteCallback);
		}

		public static void Get (string assetBundleName, List<string> assetNameList, UnityAction<Dictionary<string, Object>> loadAssetsCompleteCallback)
		{
			PrepareGet();
			assetGetter.Get (assetBundleName, assetNameList, loadAssetsCompleteCallback);
		}

		public static void Get<T> (string assetBundleName, List<string> assetNameList, UnityAction<Dictionary<string, Object>> loadAssetsCompleteCallback)
		{
			PrepareGet();
			assetGetter.Get<T> (assetBundleName, assetNameList, loadAssetsCompleteCallback);
		}

		private static void PrepareGet()
		{
			go = new GameObject (ASSET_GETTER);
			assetGetter = go.AddComponent<AssetGetter>();
		}
	}
}
