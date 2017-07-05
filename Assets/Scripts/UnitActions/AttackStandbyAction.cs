using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker.Actions;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.ScriptControl)]
	public class AttackStandbyAction : FsmStateAction
	{
		public float mRoarInterval = 3;
		float mRoarEnergy ;
		float mNextRoarTime;

		public override void Awake ()
		{
			base.Awake ();
		}

		public override void OnEnter ()
		{
			Fsm.GameObject.GetComponent<EnemyCharacter> ().navAgent.isStopped = true;
			mNextRoarTime = Time.time + mRoarInterval;
			base.OnEnter ();
		}

		public override void OnUpdate ()
		{
			mRoarEnergy += Time.deltaTime;
			EnemyCharacter enemyCharacter = Fsm.GameObject.GetComponent<EnemyCharacter> ();
			if (enemyCharacter.IsInAttackRange ()) {
				Fsm.Event ("OnAttack");
			} else if (enemyCharacter.IsInSearchRange ()) {
				Fsm.Event ("OnRunToTarget");
			} else {
				if(mNextRoarTime < Time.time){
					mNextRoarTime = Time.time + mRoarInterval;
					if (Random.Range (0, 10) < 5) {
						Fsm.Event ("OnRoar");
					} else {
						Fsm.Event ("OnJump");
					}
				}
			}
			base.OnUpdate ();
		}

		public override void OnExit ()
		{
			base.OnExit ();
		}

	}
}