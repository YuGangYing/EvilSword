using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RogerAssetBundle
{
    class AssetLoader : MonoBehaviour
    {
        private AssetBundleGetter assetBundleGetter;

        private string AssetBundleName
        {
            get;
            set;
        }

        private string AssetName
        {
            get;
            set;
        }

        private List<string> AssetNameList
        {
            get;
            set;
        }

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

        private System.Type Type
        {
            get;
            set;
        }

        internal IEnumerator Load(string assetBundleName, string assetName, UnityAction<Object> loadAssetCompleteCallback)
        {
            SetProperty(assetBundleName, assetName, loadAssetCompleteCallback);
            yield return null;
        }

        internal IEnumerator Load<T>(string assetBundleName, string assetName, UnityAction<Object> loadAssetCompleteCallback)
        {
            Type = typeof(T);
            SetProperty(assetBundleName, assetName, loadAssetCompleteCallback);
            yield return null;
        }

        internal IEnumerator Load(string assetBundleName, List<string> assetNameList, UnityAction<Dictionary<string, Object>> loadAssetsCompleteCallback)
        {
            SetProperty(assetBundleName, assetNameList, loadAssetsCompleteCallback);
            yield return null;
        }

        internal IEnumerator Load<T>(string assetBundleName, List<string> assetNameList, UnityAction<Dictionary<string, Object>> loadAssetsCompleteCallback)
        {
            Type = typeof(T);
            SetProperty(assetBundleName, assetNameList, loadAssetsCompleteCallback);
            yield return null;
        }

        private void SetProperty(string assetBundleName, string assetName, UnityAction<Object> loadAssetCompleteCallback)
        {
            AssetName = assetName;
            LoadAssetCompleteCallback = loadAssetCompleteCallback;
            SetAssetBundle(assetBundleName);
        }

        private void SetProperty(string assetBundleName, List<string> assetNameList, UnityAction<Dictionary<string, Object>> loadAssetsCompleteCallback)
        {
            AssetNameList = assetNameList;
            LoadAssetsCompleteCallback = loadAssetsCompleteCallback;
            SetAssetBundle(assetBundleName);
        }

        private void SetAssetBundle(string assetBundleName)
        {
            AssetBundleName = assetBundleName;
            assetBundleGetter = gameObject.AddComponent<AssetBundleGetter>();
            assetBundleGetter.Get(PathConstant.ASSETBUNDLES, DownloadManifestCompleteHandler);
        }

        private void DownloadManifestCompleteHandler(LoadedAssetBundle loadedAssetBundle)
        {
            var assetResourceLoader = gameObject.AddComponent<AssetResourceLoader>();
            assetResourceLoader.LoadAssetManifestCompleteCallback = dependencies =>
            {
                foreach (var assetBundleName in dependencies)
                {
                    assetBundleGetter.Get(assetBundleName);
                }

                assetBundleGetter.Get(AssetBundleName, DownloadAssetBundleCompleteHandler, dependencies);
            };
            assetResourceLoader.LoadManifest(loadedAssetBundle, AssetBundleName);
        }

        private void DownloadAssetBundleCompleteHandler(LoadedAssetBundle loadedAssetBundle)
        {
            var assetResourceLoader = gameObject.AddComponent<AssetResourceLoader>();

            if (!string.IsNullOrEmpty(AssetName))
            {
                assetResourceLoader.LoadAssetResourceCompleteCallback = obj =>
                {
                    if (LoadAssetCompleteCallback != null)
                    {
                        LoadAssetCompleteCallback(obj);
                    }

                    Dispose(assetBundleGetter);
                };
                assetResourceLoader.LoadResource(loadedAssetBundle, AssetName, Type);
            }
            else if (AssetNameList != null)
            {
                assetResourceLoader.LoadAssetsResourceCompleteCallback = dict =>
                {
                    if (LoadAssetsCompleteCallback != null)
                    {
                        LoadAssetsCompleteCallback(dict);
                    }

                    Dispose(assetBundleGetter);
                };
                assetResourceLoader.LoadResource(loadedAssetBundle, AssetNameList, Type);
            }
        }

        private void Dispose(AssetBundleGetter assetBundleGetter)
        {
            Destroy(assetBundleGetter);
            Destroy(this);
        }
    }
}
