using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class PathConstant
{
	public static string SERVER_PATH =
		#if DEVELOP
		"http://54.64.2.40/";
	#elif TEST
		"http://183.182.46.212/";
		
#elif PRODUCT
		"http://183.182.46.212/";
		
#else
		"http://localhost:3000/";
		#endif

	public static string SERVER_DOWNLOAD_PATH =
		#if DEVELOP
		"http://54.64.2.40/";
	#elif TEST
		"http://183.182.46.212/";
		
#elif PRODUCT
		"http://183.182.46.212/";
		
#else
		"http://localhost:3000/";
		#endif


	private const string DEVICE_ID = "deviceid.txt";
	internal const string SERVER_CSV = "server.csv";
	internal const string SERVER_RESOURCE_CSV = "server_resource.csv";
	internal const string CLIENT_CSV = "client.csv";
	internal const string CLIENT_RESOURCE_CSV = "client_resource.csv";
	public const string ASSETBUNDLES = "AssetBundles";
	public const string CSV = "csv";
	public const string CSV_PATH = "CSV/";

	private const string AB_PATH = "AssetBundles/";
	private const string RESOURCES_PATH = "Resources/";
	private const string ASSETBUNDLES_PATH = "assetbundles/";
	private const string VERSION_PATH = "version/";
	private const string ID_PATH = "ID/";

	public static string CLIENT_PATH {
		get {
			#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_OSX
			return Application.dataPath;
			#else
			return Application.persistentDataPath;
			#endif
		}
	}

	public static string CLIENT_AB_PATH {
		get {
			return Path.Combine (CLIENT_PATH, AB_PATH);
		}
	}

	public static string CLIENT_RESOURCES_PATH {
		get {
			return Path.Combine (CLIENT_PATH, RESOURCES_PATH);
		}
	}

	public static string CLIENT_STREAMING_ASSETS_PATH {
		get {
			return Application.streamingAssetsPath;
		}
	}

	public static string CLIENT_CSV_PATH {
		get {
			return Path.Combine (CLIENT_RESOURCES_PATH, CSV_PATH);
		}
	}

	public static string CLIENT_ASSETBUNDLES_PATH {
		get {
			return Path.Combine (Path.Combine (CLIENT_AB_PATH, GetPlatformName ()), ASSETBUNDLES_PATH);
		}
	}

	public static string CLIENT_VERSION_PATH {
		get {
			return Path.Combine (Path.Combine (CLIENT_AB_PATH, GetPlatformName ()), VERSION_PATH);
		}
	}

	public static string SERVER_AB_PATH {
		get {
			return Path.Combine (SERVER_DOWNLOAD_PATH, AB_PATH);
		}
	}

	public static string SERVER_VERSION_PATH {
		get {
			return Path.Combine (Path.Combine (SERVER_AB_PATH, GetPlatformName ()), VERSION_PATH);
		}
	}

	public static string SERVER_ASSETBUNDLES_PATH {
		get {
			return Path.Combine (Path.Combine (SERVER_AB_PATH, GetPlatformName ()), ASSETBUNDLES_PATH);
		}
	}

	public static string CLIENT_SERVER_VERSION_CSV {
		get {
			return Path.Combine (CLIENT_VERSION_PATH, SERVER_CSV);
		}
	}

		public static string CLIENT_SERVER_RESOURCE_VERSION_CSV {
		get {
		return Path.Combine (CLIENT_VERSION_PATH, SERVER_RESOURCE_CSV);
		}
		}

	public static string CLIENT_CLIENT_VERSION_CSV {
		get {
			return Path.Combine (CLIENT_VERSION_PATH, CLIENT_CSV);
		}
	}

	public static string CLIENT_CLIENT_RESOURCE_VERSION_CSV {
		get {
			return Path.Combine (CLIENT_VERSION_PATH, CLIENT_RESOURCE_CSV);
		}
	}


	public static string DEVICEID {
		get {
			return Path.Combine (Path.Combine (CLIENT_RESOURCES_PATH, ID_PATH), DEVICE_ID);
		}
	}

	public static bool CheckIfExistingVersionCSV ()
	{
		return FileManager.Exists (CLIENT_CLIENT_VERSION_CSV);
	}

	public static string GetPlatformName ()
	{
		#if UNITY_EDITOR
		return GetPlatformForAssetBundles (EditorUserBuildSettings.activeBuildTarget);
		#else
		return GetPlatformForAssetBundles (Application.platform);
		#endif
	}

	#if UNITY_EDITOR
	private static string GetPlatformForAssetBundles (BuildTarget target)
	{
		switch (target) {
		case BuildTarget.Android:
			return "android";

		case BuildTarget.iOS:
			return "ios";

		case BuildTarget.WebGL:
			return "webgl";

		default:
			return null;
		}
	}
	#endif

	private static string GetPlatformForAssetBundles (RuntimePlatform platform)
	{
		switch (platform) {
		case RuntimePlatform.Android:
			return "android";

		case RuntimePlatform.IPhonePlayer:
			return "ios";

		case RuntimePlatform.WebGLPlayer:
			return "webgl";

		default:
			return null;
		}
	}
}
