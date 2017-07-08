using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour {

	public void Attack(int v){
		Debug.Log ("Attack01");
		GetComponent<PlayerCharacter> ().OnRealHit ();
	}
}
