using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory (ActionCategory.ScriptControl)]
	public class JumpAction : BaseUnitAction
	{

		public override void Awake ()
		{
			base.Awake ();
		}

		public override void OnEnter ()
		{
			Fsm.GameObject.GetComponent<EnemyCharacter> ().navAgent.isStopped = true;
			Animator animator = Fsm.GameObject.GetComponentInChildren<Animator> (true);
			animator.PlayInFixedTime (this.animatorStateName);
			mExitTime = Time.time + Fsm.GameObject.GetComponentInChildren<Animator> (true).GetCurrentAnimatorStateInfo (0).length;
			base.OnEnter ();
		}

		public override void OnUpdate ()
		{
			if (mExitTime < Time.time) {
				Fsm.Event (this.onActionDoneEvent);
			}
			base.OnUpdate ();
		}

		public override void OnExit ()
		{
			base.OnExit ();
		}

	}
}
