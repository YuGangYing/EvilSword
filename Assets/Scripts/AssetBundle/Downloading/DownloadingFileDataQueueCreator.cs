using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RogerAssetBundle
{
	class DownloadingFileDataQueueCreator : MonoBehaviour
	{
		internal int totalSize;

		internal Queue<DownloadingFileData> CreateForDownloadingCSV ()
		{
			Queue<DownloadingFileData> downloadingFileDataQueue = new Queue<DownloadingFileData> ();
			downloadingFileDataQueue.Enqueue (new DownloadingFileData (PathConstant.SERVER_CSV, DownloadingFileTypeEnum.CSV, 0, 0, 0, null));
			downloadingFileDataQueue.Enqueue (new DownloadingFileData (PathConstant.SERVER_RESOURCE_CSV, DownloadingFileTypeEnum.CSV, 0, 0, 0, null));
			return downloadingFileDataQueue;
		}

		internal Queue<DownloadingFileData> CreateForDownloadingAssets (List<VersionCSVStructure> filteredVersionCSVStructureList)
		{
			Queue<DownloadingFileData> downloadingFileDataQueue = new Queue<DownloadingFileData> ();

			for (int i = 0; i < filteredVersionCSVStructureList.Count; i++)
			{
				VersionCSVStructure versionCSVStructure = filteredVersionCSVStructureList [i];
				downloadingFileDataQueue.Enqueue (new DownloadingFileData (versionCSVStructure.FileName, DownloadingFileTypeEnum.Assets, versionCSVStructure.FileSize, versionCSVStructure.IsAssetBundle, versionCSVStructure.IsCSV, versionCSVStructure.HashCode));
				totalSize += versionCSVStructure.FileSize;
			}

			return downloadingFileDataQueue;
		}
	}
}
