using UnityEditor;
using UnityEngine;
using System.IO;
using RogerAssetBundle;
using System.Collections.Generic;

namespace RogerAssetBundle
{
    public static class AssetBundleTools
    {
        private const string CLEAR_CACHE = "Tools/AssetBundle/Clear Cache";
        private const string CREATE_CSV = "Tools/AssetBundle/Create CSV";
        private const string BUILD_ASSETBUNDLES = "Tools/AssetBundle/Build AssetBundles";

		[MenuItem("Tools/AssetBundle/Server CSV Hash")]
		private static void DebugServerCSV()
		{
			Debug.Log(FileManager.GetFileHash (PathConstant.CLIENT_SERVER_VERSION_CSV));
		}

        [MenuItem(CLEAR_CACHE)]
        private static void ClearCache()
        {
            Caching.CleanCache();
        }

        [MenuItem(BUILD_ASSETBUNDLES)]
        private static void BuildAssetBundles()
        {
            BuildPipeline.BuildAssetBundles(CreateAssetBundleDirectory(), BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);
            if (FileManager.CheckIfCSVExist())
            {
                CreateCSV();
            }
            AssetBundleInfoManager.Create();
        }

        private static string CreateAssetBundleDirectory()
        {
            if (!Directory.Exists(PathConstant.CLIENT_ASSETBUNDLES_PATH))
            {
                Directory.CreateDirectory(PathConstant.CLIENT_ASSETBUNDLES_PATH);
            }

            return PathConstant.CLIENT_ASSETBUNDLES_PATH;
        }

        private static void CreateCSV()
        {
            Caching.CleanCache();
            Object[] objs = Resources.LoadAll<TextAsset>(PathConstant.CSV_PATH);
            var outputList = new List<Object>();
            var assetPathList = new List<string>();

            foreach (Object obj in objs)
            {
                string fileAssetPath = AssetDatabase.GetAssetPath(obj);
                string fileWholePath = Application.dataPath + fileAssetPath.Substring(fileAssetPath.IndexOf("/", System.StringComparison.Ordinal));
                SoCsv csv = ScriptableObject.CreateInstance<SoCsv>();
                csv.FileName = obj.name;
                csv.Content = File.ReadAllBytes(fileWholePath);
                string assetPath = fileAssetPath.Split('.')[0] + ".asset";
                assetPathList.Add(assetPath);
                AssetDatabase.CreateAsset(csv, assetPath);
                outputList.Add(AssetDatabase.LoadAssetAtPath(assetPath, typeof(SoCsv)));
            }

            Object[] outputArray = outputList.ToArray();

            if (BuildPipeline.BuildAssetBundle(null, outputArray, Path.Combine(PathConstant.CLIENT_ASSETBUNDLES_PATH, PathConstant.CSV), BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.UncompressedAssetBundle, EditorUserBuildSettings.activeBuildTarget))
            {
                AssetDatabase.Refresh();
            }

            int length = assetPathList.Count;
            for (int i = 0; i < length; i++)
            {
                AssetDatabase.DeleteAsset(assetPathList[i]);
            }
        }
    }
}
