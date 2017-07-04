﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyCharacter : MonoBehaviour {

	public float speed;
	//public float minDis, maxDis;
	//public float range;

	public int searchRange = 10;

	public int attackNumber;

	public CharacterController controller;

	public PlayerCharacter player;

	private Animator animator;

	//private GameObject mainCamera;

	private CharacterAttribute attribute;

	private bool isFoundPlayer;
	public bool isAttacking;
	private float attackTimer;

	public float AttackInterval = 3f;

	public UnityEngine.AI.NavMeshAgent navAgent = null;

	private GameObject[ ] mobPoints;
	private int mobPointIndex = -1;

	// Use this for initialization
	void Awake( ) {
		//mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		animator = GetComponent<Animator>( );

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
		player.allEnemies.Add(this.gameObject);
		attackTimer = AttackInterval;
		attribute = GetComponent<CharacterAttribute>( );
		navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>( );
		mobPoints = GameObject.FindGameObjectsWithTag("MobPoint");
		if ( mobPoints.Length > 0 ) {
			mobPointIndex = 0;
			navAgent.SetDestination(mobPoints[mobPointIndex].transform.position);
		}
	}

	// Update is called once per frame
	void Update( ) {
		if ( attribute.IsDeath ) {
			Death( );
			return;
		}

		if ( attackTimer > 0 )
			attackTimer -= Time.deltaTime;

		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
//		isAttacking = false;
//		for(int i=1; i<attackNumber; i++ ) {
//			if( state.IsName("Attack" + i) ) {
//				isAttacking = true;
//				break;
//			}
//		}
////		Debug.Log(animator.GetBool("run"));
//		if ( isAttacking ) {
////			animator.SetBool("run", false);
//			return;
//		}

//		if ( IsInAttackRange() && attackTimer < 0 ) {       // attack state
//			attackTimer = AttackInterval;
////			animator.SetBool("run", false);
//			animator.SetBool("attack" + Random.Range(1, attackNumber + 1).ToString( ), true);
//			navAgent.Stop( );
//		} else if ( IsInSearchRange() ) {                                    // Found
//			navAgent.Resume( );
//			navAgent.SetDestination(player.gameObject.transform.position);
////			animator.SetBool("run", true);
//		} 

//		else if ( navAgent.remainingDistance < 1 ) {       // arrive at one patrol point
//			navAgent.Resume( );
//			mobPointIndex = ( mobPointIndex + 1 ) % mobPoints.Length;
//			navAgent.SetDestination(mobPoints[mobPointIndex].transform.position);
////			animator.SetBool("run", true);
//		}

//		if (isRunable) {
////			animator.SetBool("run", true);
//			navAgent.Resume ();
//		} else {
////			animator.SetBool("run", false);
//			navAgent.Stop();
//		}
		//Debug.Log(InRange( ));
	}

	public bool IsInSearchRange(){
		return InRange (searchRange);
	}

	public bool IsInAttackRange(){
		return InRange (attribute.attackDistance);
	}

	Transform mTarget;
	public void MoveToTarget(){
		mTarget = player.gameObject.transform;
		navAgent.SetDestination(mTarget.position);
	}

	bool InRange(float range) {
		var dis = Vector3.Distance(this.transform.position, player.transform.position);
		if ( dis < range )
			return true;
		return false;
	}
	
	public void BeAttacked() {

		if ( attribute.IsDeath )
			return;

		int damage = (int)Random.Range(100, 200);
		bool critical = damage > 150;

		animator.SetBool("hurt", true);

		attribute.TakeDamage(damage.ToString(), critical);

		if ( critical )
			player.Shake( );
	}

	public void Attack( ) {

		float distance = Vector3.Distance(player.transform.position, navAgent.nextPosition);

		Vector3 dir = ( player.transform.position - transform.position ).normalized;

		float direction = Vector3.Dot(transform.forward, dir);

		if ( direction > 0 && distance < attribute.attackDistance ) {
			player.BeAttacked( );
		} else {

		}

	}

	private void Death( ) {
		animator.SetBool("death", true);
		Destroy(this.gameObject, 3);
	}

	public void OnDeadDone(int param){
		Debug.Log ("OnDeadDone");
	}

}
