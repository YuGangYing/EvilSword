using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : SingleMonoBehaviour<ResourceManager> {

	public GameObject GetMonster(){
		GameObject prefab = Resources.Load<GameObject> ("Prefabs/TrollPrefab");
		prefab.SetActive (false);
		return prefab;
	}

}
