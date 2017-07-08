using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory (ActionCategory.ScriptControl)]
	public class AttackAction : FsmStateAction
	{

		public override void Awake ()
		{
			base.Awake ();
		}

		int attackNumber = 7;

		public override void OnEnter ()
		{
			Fsm.GameObject.GetComponent<EnemyCharacter> ().navAgent.isStopped = true;
			Fsm.GameObject.GetComponent<EnemyCharacter> ().isAttacking = true;
			Animator animator = Fsm.GameObject.GetComponent<Animator> ();
			animator.SetBool ("run_to_target",false);
			animator.SetBool ("attack" + Random.Range (1, attackNumber + 1).ToString (), true);
			Fsm.GameObject.transform.LookAt (Fsm.GameObject.GetComponent<EnemyCharacter> ().player.transform);
			base.OnEnter ();
		}

		public override void OnUpdate ()
		{

			if (!Fsm.GameObject.GetComponent<EnemyCharacter> ().isAttacking && Fsm.GameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Base Layer.Attack_standby")) {
				Fsm.Event ("OnAttackDone");
			}
//			EnemyCharacter enemyCharacter = Fsm.GameObject.GetComponent<EnemyCharacter> ();
//			if (enemyCharacter.IsInAttackRange ()) {
//				Fsm.Event ("OnAttack");
//			} else if (enemyCharacter.IsInSearchRange ()) {
//				Fsm.Event ("OnRunToTarget");
//			} 
//			if (enemyCharacter.IsInAttackRange ()) {
//				Fsm.Event ("OnAttack");
//			}
			base.OnUpdate ();
		}

		public override void OnExit ()
		{
			base.OnExit ();
		}
	}
}
