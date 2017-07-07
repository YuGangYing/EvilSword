using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : SingleMonoBehaviour<SpawnManager> {

	List<Transform> mMonstarSpawnPoints;
	List<Transform> mPlayerSpawnPoints;

	public Dictionary<Transform,GameObject> monsters;
	ResourceManager mResourceManager;
	public string targetSceneAssetbundle = "halloween.assetbundle";
	public string targetSceneName = "Halloween_Level";
	protected override void Awake(){

	}

	void Start(){
		StartCoroutine (LoadAdditiveScene());
	}

	IEnumerator LoadAdditiveScene(){
		AssetBundle ab = AssetBundle.LoadFromFile (PathConstant.CLIENT_ASSETBUNDLES_PATH + targetSceneAssetbundle);
		AsyncOperation asyn = SceneManager.LoadSceneAsync (targetSceneName, LoadSceneMode.Additive);
		yield return asyn.isDone;
		yield return null;
		GameObject spawnRoot = GameObject.Find ("mob_points");
		mMonstarSpawnPoints = new List<Transform> ();
		for(int i=0;i<spawnRoot.transform.childCount;i++){
			mMonstarSpawnPoints.Add(spawnRoot.transform.GetChild (i));
		}
		mResourceManager = GameObject.FindObjectOfType<ResourceManager> ();
		monsters = new Dictionary<Transform, GameObject> ();
		foreach(Transform trans in mMonstarSpawnPoints){
			monsters.Add (trans,Instantiate(mResourceManager.GetMonster(),trans.position,trans.rotation));
			StartCoroutine (_DelayActive(monsters [trans]));
		}

		GameObject playerSpawnRoot = GameObject.Find ("player_points");
		mPlayerSpawnPoints = new List<Transform> ();
		for(int i=0;i<playerSpawnRoot.transform.childCount;i++){
			mPlayerSpawnPoints.Add (playerSpawnRoot.transform.GetChild(i));
		}
	}

	void Update(){
		if (mMonstarSpawnPoints != null) {
			for (int i = 0; i < mMonstarSpawnPoints.Count; i++) {
				if (monsters [mMonstarSpawnPoints [i]] == null) {
					monsters [mMonstarSpawnPoints [i]] = Instantiate (mResourceManager.GetMonster (), mMonstarSpawnPoints [i].position, mMonstarSpawnPoints [i].rotation);
					StartCoroutine (_DelayActive (monsters [mMonstarSpawnPoints [i]]));
				}
			}
		}
	}

	IEnumerator _DelayActive(GameObject monster){
		yield return new WaitForSeconds (5);
		monster.SetActive (true);
	}

	public Transform GetPlayerSpawnPoint(int index){
		index = Mathf.Clamp (index,0,mPlayerSpawnPoints.Count - 1);
		return mPlayerSpawnPoints[index];
	}


}
