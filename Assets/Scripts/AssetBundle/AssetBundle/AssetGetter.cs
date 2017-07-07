using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RogerAssetBundle
{
	class AssetGetter : MonoBehaviour
	{
		private AssetExecutor assetExecutor;
		private UnityAction<Object> LoadAssetCompleteCallback
		{
			get;
			set;
		}

		private UnityAction<Dictionary<string, Object>> LoadAssetsCompleteCallback
		{
			get;
			set;
		}

		internal void Get (string assetBundleName, string assetName, UnityAction<Object> loadAssetCompleteCallback)
		{
			PrepareGet (loadAssetCompleteCallback);
			StartCoroutine (GetAsset (assetBundleName, assetName));
		}

		internal void Get<T> (string assetBundleName, string assetName, UnityAction<Object> loadAssetCompleteCallback)
		{
			PrepareGet (loadAssetCompleteCallback);
			StartCoroutine (GetAsset<T> (assetBundleName, assetName));
		}

		internal void Get (string assetBundleName, List<string> assetNameList, UnityAction<Dictionary<string, Object>> loadAssetsCompleteCallback)
		{
			PrepareGet (loadAssetsCompleteCallback);
			StartCoroutine (GetAsset (assetBundleName, assetNameList));
		}

		internal void Get<T> (string assetBundleName, List<string> assetNameList, UnityAction<Dictionary<string, Object>> loadAssetsCompleteCallback)
		{
			PrepareGet (loadAssetsCompleteCallback);
			StartCoroutine (GetAsset<T> (assetBundleName, assetNameList));
		}

		private void PrepareGet (UnityAction<Object> loadAssetCompleteCallback)
		{
			LoadAssetCompleteCallback = loadAssetCompleteCallback;
			SetAssetxecutor();
		}

		private void PrepareGet (UnityAction<Dictionary<string, Object>> loadAssetsCompleteCallback)
		{
			LoadAssetsCompleteCallback = loadAssetsCompleteCallback;
			SetAssetxecutor();
		}

		private void SetAssetxecutor()
		{
			assetExecutor = gameObject.AddComponent<AssetExecutor>();
		}

		private IEnumerator GetAsset (string assetBundleName, string assetName)
		{
			yield return StartCoroutine (assetExecutor.LoadAsset (assetBundleName, assetName, LoadAssetCompleteHandler));
		}

		private IEnumerator GetAsset<T> (string assetBundleName, string assetName)
		{
			yield return StartCoroutine (assetExecutor.LoadAsset<T> (assetBundleName, assetName, LoadAssetCompleteHandler));
		}

		private IEnumerator GetAsset (string assetBundleName, List<string> assetNameList)
		{
			yield return StartCoroutine (assetExecutor.LoadAsset (assetBundleName, assetNameList, LoadAssetsCompleteHandler));
		}

		private IEnumerator GetAsset<T> (string assetBundleName, List<string> assetNameList)
		{
			yield return StartCoroutine (assetExecutor.LoadAsset<T> (assetBundleName, assetNameList, LoadAssetsCompleteHandler));
		}

		private void LoadAssetCompleteHandler (Object obj)
		{
			if (LoadAssetCompleteCallback != null)
			{
				LoadAssetCompleteCallback (obj);
			}

			Dispose();
		}

		private void LoadAssetsCompleteHandler (Dictionary<string, Object> dict)
		{
			if (LoadAssetsCompleteCallback != null)
			{
				LoadAssetsCompleteCallback (dict);
			}

			Dispose();
		}

		private void Dispose()
		{
			Destroy (this);
			Destroy (gameObject);
		}
	}
}
