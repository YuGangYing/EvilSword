namespace RogerAssetBundle
{
	public class AssetBundleInfo
	{
		public int ID
		{
			get;
			set;
		}
	
		public string FileName
		{
			get;
			set;
		}
	
		public long FileSize
		{
			get;
			set;
		}
	
		public int IsCSV
		{
			get;
			set;
		}
	
		public string HashCode
		{
			get;
			set;
		}
	
		public AssetBundleInfo (int id, string fileName)
		{
			ID = id;
			FileName = fileName;
			IsCSV = fileName == "csv" ? 1 : 0;
		}
	}
}
