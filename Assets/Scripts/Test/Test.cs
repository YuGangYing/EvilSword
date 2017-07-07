using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour {

	void Awake(){
		AssetBundle ab = AssetBundle.LoadFromFile (PathConstant.CLIENT_ASSETBUNDLES_PATH + "scene_halloween.assetbundle");
		SceneManager.LoadScene ("Halloween_Level",LoadSceneMode.Additive);
	}
}
