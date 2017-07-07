﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace RogerAssetBundle
{
	class DownloadingFileFilter : MonoBehaviour
	{
		internal List<VersionCSVStructure> Filter (IEnumerable<VersionCSVStructure> versionCSVStructureCollections)
		{
			DeleteUnusedAssets (versionCSVStructureCollections);
			List<VersionCSVStructure> filteredVersionCSVStructureList = new List<VersionCSVStructure> ();
	
			foreach (var item in versionCSVStructureCollections)
			{
				string existingFileHashCode = FileManager.GetFileHash (PathConstant.CLIENT_ASSETBUNDLES_PATH + item.FileName);
	
				if (item.HashCode != existingFileHashCode)
				{
					filteredVersionCSVStructureList.Add (item);
				}
			}
	
			return filteredVersionCSVStructureList;
		}
	
		private void DeleteUnusedAssets (IEnumerable<VersionCSVStructure> versionCSVStructureCollections)
		{
			if (FileManager.DirectoryExists (PathConstant.CLIENT_ASSETBUNDLES_PATH))
			{
				string[] stringArray = FileManager.GetFiles (PathConstant.CLIENT_ASSETBUNDLES_PATH, "*", SearchOption.TopDirectoryOnly);
	
				foreach (var item in stringArray)
				{
					string[] nameArray = item.Split ('/');
					string fileName = nameArray [nameArray.Length - 1];
	
					if (versionCSVStructureCollections.FirstOrDefault (result => result.FileName == fileName) == null)
					{
						FileManager.DeleteFile (item);
					}
				}
			}
		}
	}
}
