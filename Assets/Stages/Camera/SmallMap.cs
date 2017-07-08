using UnityEngine;
using System.Collections;

public class SmallMap : MonoBehaviour {

	private GameObject target;

	private float preserveY;
	// Use this for initialization
	void Start () {
		
		preserveY = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("Update");
		if(target == null)
			target = GameObject.FindGameObjectWithTag("Player");
		else
			this.transform.position = new Vector3(target.transform.position.x, preserveY, target.transform.position.z);
	}
}
