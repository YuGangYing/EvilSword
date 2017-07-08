using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEffect : MonoBehaviour
{
	public string effectName;
	public Vector3 offset;
	public float speed;
	public GameObject prefab;
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.F)) {
//			GameObject prefab = Resources.Load<GameObject> (effectName);
			GameObject go = Instantiate (prefab, transform.position + new Vector3(transform.forward.x,0,transform.forward.z).normalized * 10 + new Vector3(0,10,0),Quaternion.identity) as GameObject;
//			StartCoroutine (_Move (go, transform.forward));
		}
	}

	IEnumerator _Move (GameObject go, Vector3 dir)
	{
		while (true && go!=null) {
			go.transform.Translate (dir * speed * Time.deltaTime);
			yield return null;
		}
	}
}
