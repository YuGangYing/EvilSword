using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetMeshCollider : MonoBehaviour {

	void Awake(){
		MeshCollider col = GetComponent<MeshCollider> ();
		if (col == null)
			col = gameObject.AddComponent<MeshCollider> ();
		col.enabled = true;
		col.isTrigger = false;
	}
}
