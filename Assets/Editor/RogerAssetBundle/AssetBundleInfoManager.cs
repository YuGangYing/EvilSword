using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;

namespace RogerAssetBundle
{
    public static class AssetBundleInfoManager
    {
        private static List<AssetBundleInfo> assetBundleInfoList;
        private static long TotalSize;
        private static string VersionHashCode;
        private const string COMMA = ",";
        public static void Create()
        {
            string[] assetBundleNames = AssetDatabase.GetAllAssetBundleNames();
            assetBundleInfoList = new List<AssetBundleInfo>();

            for (int i = 0, length = assetBundleNames.Length; i < length; i++)
            {
                var assetBundleInfo = new AssetBundleInfo(i + 1, assetBundleNames[i]);
                assetBundleInfoList.Add(assetBundleInfo);
            }

            assetBundleInfoList.Add(new AssetBundleInfo(assetBundleInfoList.Count + 1, PathConstant.ASSETBUNDLES));
            if (FileManager.CheckIfCSVExist())
            {
                assetBundleInfoList.Add(new AssetBundleInfo(assetBundleInfoList.Count + 1, PathConstant.CSV));
            }
            CreateInfo();
            CreateVersion();
        }

        private static void CreateInfo()
        {
            TotalSize = 0;

            foreach (var assetBundleInfo in assetBundleInfoList)
            {
                var fileName = Path.Combine(PathConstant.CLIENT_ASSETBUNDLES_PATH, assetBundleInfo.FileName);
                assetBundleInfo.FileSize = new FileInfo(fileName).Length;
                assetBundleInfo.HashCode = FileManager.GetFileHash(fileName);
                TotalSize += assetBundleInfo.FileSize;
            }
        }

        private static void CreateVersion()
        {
            var version = new StringBuilder();
            string title = "ID,FileName,FileSize,IsCSV,HashCode";
            version.AppendLine(title);

            foreach (var assetBundleInfo in assetBundleInfoList)
            {
                version.AppendLine(CreateLine(assetBundleInfo.ID, assetBundleInfo.FileName, assetBundleInfo.FileSize, assetBundleInfo.IsCSV, assetBundleInfo.HashCode).ToString());
            }

            FileManager.WriteString(PathConstant.CLIENT_SERVER_VERSION_CSV, version.ToString());
            VersionHashCode = FileManager.GetFileHash(PathConstant.CLIENT_SERVER_VERSION_CSV);
            AssetDatabase.Refresh();
        }

        private static StringBuilder CreateLine(int id, string name, long size, int isCsv, string hashCode)
        {
            var line = new StringBuilder();
            line.Append(id).Append(COMMA).Append(name).Append(COMMA).Append(size).Append(COMMA).Append(isCsv).Append(COMMA).Append(hashCode);
            return line;
        }
    }
}
