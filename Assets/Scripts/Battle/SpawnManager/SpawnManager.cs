using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : SingleMonoBehaviour<SpawnManager> {

	public List<Transform> spawnPoints;
	public Dictionary<Transform,GameObject> monsters;
	ResourceManager mResourceManager;

	protected override void Awake(){
		mResourceManager = GameObject.FindObjectOfType<ResourceManager> ();
		monsters = new Dictionary<Transform, GameObject> ();
		foreach(Transform trans in spawnPoints){
			monsters.Add (trans,Instantiate(mResourceManager.GetMonster(),trans.position,trans.rotation));
			StartCoroutine (_DelayActive(monsters [trans]));
		}
	}

	void Update(){
		for(int i=0;i<spawnPoints.Count;i++){
			if(monsters[spawnPoints[i]]==null){
				monsters [spawnPoints [i]] = Instantiate (mResourceManager.GetMonster (), spawnPoints [i].position, spawnPoints [i].rotation);
				StartCoroutine (_DelayActive(monsters [spawnPoints [i]]));
			}
		}
	}

	IEnumerator _DelayActive(GameObject monster){
		yield return new WaitForSeconds (5);
		monster.SetActive (true);
	}


}
