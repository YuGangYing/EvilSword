using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RogerAssetBundle
{
	class AssetResourceLoader : MonoBehaviour
	{
		private const string AssetBundleManifestName = "AssetBundleManifest";
		internal UnityAction<Object> LoadAssetResourceCompleteCallback
		{
			get;
			set;
		}

		internal UnityAction<Dictionary<string, Object>> LoadAssetsResourceCompleteCallback
		{
			get;
			set;
		}

		internal UnityAction<string[]> LoadAssetManifestCompleteCallback
		{
			get;
			set;
		}

		private System.Type Type
		{
			get;
			set;
		}

		internal void LoadResource (LoadedAssetBundle loadedAssetBundle, string assetName, System.Type type)
		{
			SetType (type);
			StartCoroutine (StartLoadResource (loadedAssetBundle, assetName));
		}

		internal void LoadResource (LoadedAssetBundle loadedAssetBundle, List<string> assetNameList, System.Type type)
		{
			SetType (type);
			StartCoroutine (StartLoadResource (loadedAssetBundle, assetNameList));
		}

		private void SetType (System.Type type)
		{
			Type = type;
		}

		private IEnumerator StartLoadResource (LoadedAssetBundle loadedAssetBundle, string assetName)
		{
			AssetBundleRequest assetBundleRequest;
			assetBundleRequest = Type == null ? loadedAssetBundle.AssetBundle.LoadAssetAsync (assetName) : loadedAssetBundle.AssetBundle.LoadAssetAsync (assetName, Type);
			yield return assetBundleRequest;

			if (LoadAssetResourceCompleteCallback != null)
			{
				LoadAssetResourceCompleteCallback (assetBundleRequest.asset);
			}
		}

		private IEnumerator StartLoadResource (LoadedAssetBundle loadedAssetBundle, List<string> assetNameList)
		{
			var dictionary = new Dictionary<string, Object>();

			foreach (var assetName in assetNameList)
			{
				AssetBundleRequest assetBundleRequest;
				assetBundleRequest = Type == null ? loadedAssetBundle.AssetBundle.LoadAssetAsync (assetName) : loadedAssetBundle.AssetBundle.LoadAssetAsync (assetName, Type);
				yield return assetBundleRequest;
				dictionary.Add (assetBundleRequest.asset.name, assetBundleRequest.asset);
			}

			if (LoadAssetsResourceCompleteCallback != null)
			{
				LoadAssetsResourceCompleteCallback (dictionary);
			}
		}

		private void Dispose (LoadedAssetBundle loadedAssetBundle)
		{
			AssetBundlePool.Dispose (loadedAssetBundle.AssetBundleName);
			Destroy (this);
		}

		internal void LoadManifest (LoadedAssetBundle loadedAssetBundle, string assetBundleName)
		{
			StartCoroutine (StartLoadManifest (loadedAssetBundle, assetBundleName));
		}

		private IEnumerator StartLoadManifest (LoadedAssetBundle loadedAssetBundle, string assetBundleName)
		{
			var assetBundleRequest = loadedAssetBundle.AssetBundle.LoadAssetAsync<AssetBundleManifest> (AssetBundleManifestName);
			yield return assetBundleRequest;
			var assetBundleManifest = assetBundleRequest.asset as AssetBundleManifest;
			string[] dependencies = assetBundleManifest.GetAllDependencies (assetBundleName);

			if (LoadAssetManifestCompleteCallback != null)
			{
				LoadAssetManifestCompleteCallback (dependencies);
			}

			Destroy (this);
		}
	}
}
