using UnityEngine;

namespace RogerAssetBundle
{
	class LoadedAssetBundle
	{
		internal AssetBundle AssetBundle
		{
			get;
			set;
		}

		internal int ReferencedCount
		{
			get;
			set;
		}

		internal string AssetBundleName
		{
			get;
			set;
		}

		internal string[] Dependencies
		{
			get;
			set;
		}

		public LoadedAssetBundle (string assetBundleName, AssetBundle assetBundle, string[] dependencies)
		{
			AssetBundleName = assetBundleName;
			AssetBundle = assetBundle;
			Dependencies = dependencies;
			ReferencedCount = 1;
		}
	}
}
