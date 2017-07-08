using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory (ActionCategory.ScriptControl)]
	public class RunToTargetAction : FsmStateAction
	{

		public override void Awake ()
		{
			base.Awake ();
		}

		public override void OnEnter ()
		{
			Fsm.GameObject.GetComponentInChildren<Animator> (true).SetBool ("run_to_target",true);
			base.OnEnter ();
		}

		public override void OnUpdate ()
		{
			EnemyCharacter enemyCharacter = Fsm.GameObject.GetComponent<EnemyCharacter> ();
			if (Fsm.GameObject.GetComponentInChildren<Animator> (true).GetCurrentAnimatorStateInfo (0).IsName ("Base Layer.RunToTarget")) {
				Fsm.GameObject.GetComponent<EnemyCharacter> ().MoveToTarget ();
				Fsm.GameObject.GetComponent<EnemyCharacter> ().navAgent.isStopped = false;
			} else {
				Fsm.GameObject.GetComponent<EnemyCharacter> ().navAgent.isStopped = true;
			}
			if (enemyCharacter.IsInAttackRange ()) {
				Fsm.Event ("OnAttack");
			}
			base.OnUpdate ();
		}

		public override void OnExit ()
		{
			base.OnExit ();
		}
	}
}