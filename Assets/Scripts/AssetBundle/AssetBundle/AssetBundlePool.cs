using System.Collections.Generic;

namespace RogerAssetBundle
{
	static class AssetBundlePool
	{
		private static Dictionary<string, LoadedAssetBundle> Pool
		{
			get;
			set;
		}

		internal static LoadedAssetBundle Get (string assetBundleName)
		{
			LoadedAssetBundle loadedAssetBundle;
			Pool.TryGetValue (assetBundleName, out loadedAssetBundle);
			return loadedAssetBundle;
		}

		static AssetBundlePool ()
		{
			Pool = new Dictionary<string, LoadedAssetBundle> ();
		}

		internal static void Add (string assetBundleName, LoadedAssetBundle loadedAssetBundle)
		{
			if (!Pool.ContainsKey (assetBundleName))
			{
				Pool.Add (assetBundleName, loadedAssetBundle);
			}
		}

		internal static void Remove (string assetBundleName)
		{
			Pool.Remove (assetBundleName);
		}

		internal static void Dispose (string assetBundleName, bool isUnloadAll = false)
		{
			if (Pool.ContainsKey (assetBundleName))
			{
				LoadedAssetBundle loadedAssetBundle;
				Pool.TryGetValue (assetBundleName, out loadedAssetBundle);

				if (loadedAssetBundle != null)
				{
					Unload (loadedAssetBundle, isUnloadAll);
				}
			}
		}

		internal static void DisposeAll (bool isUnloadAll = false)
		{
			foreach (KeyValuePair<string, LoadedAssetBundle> pair in Pool)
			{
				if (pair.Value != null)
				{
					Unload (pair.Value, isUnloadAll);
				}
			}

			Pool.Clear ();
		}

		internal static void Unload (LoadedAssetBundle loadedAssetBundle, bool isUnloadAll)
		{
			UnloadDependencies (loadedAssetBundle, isUnloadAll);

			if (loadedAssetBundle.AssetBundle != null)
			{
				loadedAssetBundle.AssetBundle.Unload (isUnloadAll);
			}

			Remove (loadedAssetBundle.AssetBundleName);
		}

		private static void CheckIfCanBeUnload (LoadedAssetBundle loadedAssetBundle, int referencedCount, bool isUnloadAll)
		{
			loadedAssetBundle.ReferencedCount -= referencedCount;

			if (loadedAssetBundle.ReferencedCount == 0)
			{
				Unload (loadedAssetBundle, isUnloadAll);
			}
		}

		private static void UnloadDependencies (LoadedAssetBundle loadedAssetBundle, bool isUnloadAll)
		{
			if (loadedAssetBundle.Dependencies != null)
			{
				foreach (var item in loadedAssetBundle.Dependencies)
				{
					var dependency = AssetBundlePool.Get (item);

					if (dependency != null)
					{
						CheckIfCanBeUnload (dependency, loadedAssetBundle.ReferencedCount, isUnloadAll);
					}
				}
			}
		}
	}
}
