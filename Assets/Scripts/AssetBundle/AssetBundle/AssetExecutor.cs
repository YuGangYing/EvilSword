using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RogerAssetBundle
{
	class AssetExecutor : MonoBehaviour
	{
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

		internal IEnumerator LoadAsset (string assetBundleName, string assetName, UnityAction<Object> loadAssetCompleteCallback)
		{
			LoadAssetCompleteCallback = loadAssetCompleteCallback;
			yield return StartCoroutine (Load (assetBundleName, assetName));
		}

		private IEnumerator Load (string assetBundleName, string assetName)
		{
			yield return StartCoroutine (gameObject.AddComponent<AssetLoader>().Load (assetBundleName, assetName, LoadCompleteHandler));
			yield return null;
		}

		internal IEnumerator LoadAsset (string assetBundleName, List<string> assetNameList, UnityAction<Dictionary<string, Object>> loadAssetsCompleteCallback)
		{
			LoadAssetsCompleteCallback = loadAssetsCompleteCallback;
			yield return StartCoroutine (Load (assetBundleName, assetNameList));
		}

		private IEnumerator Load (string assetBundleName, List<string> assetNameList)
		{
			yield return StartCoroutine (gameObject.AddComponent<AssetLoader>().Load (assetBundleName, assetNameList, LoadListCompleteHandler));
			yield return null;
		}

		internal IEnumerator LoadAsset<T> (string assetBundleName, string assetName, UnityAction<Object> loadAssetCompleteCallback)
		{
			LoadAssetCompleteCallback = loadAssetCompleteCallback;
			yield return StartCoroutine (Load<T> (assetBundleName, assetName));
		}

		private IEnumerator Load<T> (string assetBundleName, string assetName)
		{
			yield return StartCoroutine (gameObject.AddComponent<AssetLoader>().Load<T> (assetBundleName, assetName, LoadCompleteHandler));
			yield return null;
		}

		internal IEnumerator LoadAsset<T> (string assetBundleName, List<string> assetNameList, UnityAction<Dictionary<string, Object>> loadAssetsCompleteCallback)
		{
			LoadAssetsCompleteCallback = loadAssetsCompleteCallback;
			yield return StartCoroutine (Load<T> (assetBundleName, assetNameList));
		}

		private IEnumerator Load<T> (string assetBundleName, List<string> assetNameList)
		{
			yield return StartCoroutine (gameObject.AddComponent<AssetLoader>().Load<T> (assetBundleName, assetNameList, LoadListCompleteHandler));
			yield return null;
		}

		private void LoadCompleteHandler (Object obj)
		{
			if (LoadAssetCompleteCallback != null)
			{
				LoadAssetCompleteCallback (obj);
			}

			Dispose();
		}

		private void LoadListCompleteHandler (Dictionary<string, Object> dict)
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
		}
	}
}
