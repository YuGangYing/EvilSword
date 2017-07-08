using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEnemyEvent : MonoBehaviour {

	public void Attack(){
		GetComponentInParent<EnemyCharacter> ().Attack ();
	}
}
