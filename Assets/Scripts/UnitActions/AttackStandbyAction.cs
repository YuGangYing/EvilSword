using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker.Actions;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.ScriptControl)]
	public class AttackStandbyAction : FsmStateAction
	{

		public override void Awake ()
		{
			base.Awake ();
		}

		public override void OnEnter ()
		{
			Fsm.GameObject.GetComponent<EnemyCharacter> ().navAgent.isStopped = true;
			base.OnEnter ();
		}

		public override void OnUpdate ()
		{
			EnemyCharacter enemyCharacter = Fsm.GameObject.GetComponent<EnemyCharacter> ();
			if (enemyCharacter.IsInAttackRange ()) {
				Fsm.Event ("OnAttack");
			} else if (enemyCharacter.IsInSearchRange ()) {
				Fsm.Event ("OnRunToTarget");
			} 
			base.OnUpdate ();
		}

		public override void OnExit ()
		{
			base.OnExit ();
		}

	}
}