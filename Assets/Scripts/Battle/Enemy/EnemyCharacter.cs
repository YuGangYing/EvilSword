using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMaker;

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

	PlayMakerFSM mFsm;


	// Use this for initialization
	void Awake( ) {
		//mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		animator = GetComponent<Animator>( );
		mFsm = GetComponent<PlayMakerFSM> ();
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

	IEnumerator _Move(){
		Vector3 forward = transform.position -transform.forward * 0.3f;
		float t = 0;
		while(t<1){
			if (attribute.IsDeath)
				yield break;
			transform.position = Vector3.Slerp (transform.position,forward,t);
			t += Time.deltaTime * 3;
			yield return null;
		}
	}

	public Vector3 direct = new Vector3 (0, 100, 0);
	// Update is called once per frame
	void Update( ) {
		if ( attribute.IsDeath ) {
			if(mFsm.Fsm.ActiveStateName!="Death")
				mFsm.Fsm.Event ("OnDead");
			return;
		}
		if (Input.GetKeyDown (KeyCode.H)) {
			Debug.Log ("AddForce");
//			GetComponent<Rigidbody> ().AddForce (direct);
			StartCoroutine (_Move());
		}
//		if ( attackTimer > 0 )
//			attackTimer -= Time.deltaTime;
//
//		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
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
		mFsm.Fsm.Event ("OnBeaten");
		StartCoroutine (_Move());
		int damage = (int)Random.Range(100, 200);
		bool critical = damage > 150;
		attribute.TakeDamage(damage.ToString(), critical);
		navAgent.isStopped = true;
		if ( critical )
			player.Shake( );
	}

	IEnumerator _SlowDown(){
		Time.timeScale = 0.5f;
		yield return new WaitForSecondsRealtime(0.2f);
		Time.timeScale = 1;
		yield return new WaitForSecondsRealtime (5);
		GetComponentInChildren<Animator> ().speed = 1;
	}

	public void Attack( ) {
		if(player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Base Layer.SAMK")){
			GetComponentInChildren<Animator> ().speed = 0.1f;
			StartCoroutine (_SlowDown());
			return;
		}
		float distance = Vector3.Distance(player.transform.position, navAgent.nextPosition);

		Vector3 dir = ( player.transform.position - transform.position ).normalized;

		float direction = Vector3.Dot(transform.forward, dir);

		if ( direction > 0 && distance < attribute.attackDistance ) {
			player.BeAttacked( );
		} else {

		}

	}

//	private void Death( ) {
//		animator.SetBool("death", true);
//		Destroy(this.gameObject, 3);
//	}

	public void OnDeadDone(int param){
		Debug.Log ("OnDeadDone");
	}

}
