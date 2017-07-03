using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Battle
{
	public class Trigger : MonoBehaviour
	{

		void OnCollisionEnter(Collision collision){
			Debug.Log (collision);
		}
		public Vector3 targetAngle = new Vector3 (20,-90,0);
		public float duration = 3;
		void OnTriggerEnter(Collider other){
			if(other.GetComponent<PlayerUserControl>()!=null){
				SimpleMouseRotator rotator = GameObject.FindObjectOfType<SimpleMouseRotator> ();
				if(rotator!=null){
					rotator.transform.DOLocalRotate (targetAngle,duration);
				}
			}
		}



	}
}
