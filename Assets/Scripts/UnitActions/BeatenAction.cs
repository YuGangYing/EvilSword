using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory (ActionCategory.ScriptControl)]
	public class BeatenAction : BaseUnitAction
	{

		public override void Awake ()
		{
			base.Awake ();
		}

		public override void OnEnter ()
		{
			Fsm.GameObject.GetComponent<EnemyCharacter> ().navAgent.isStopped = false;
			Animator animator = Fsm.GameObject.GetComponent<Animator> ();
			animator.PlayInFixedTime (animatorStateName);
			mExitTime = Time.time + Fsm.GameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length;
			base.OnEnter ();
		}

		public override void OnUpdate ()
		{
			if (mExitTime < Time.time) {
				Fsm.Event (onActionDoneEvent);
			}
			base.OnUpdate ();
		}

		public override void OnExit ()
		{
			base.OnExit ();
		}

	}
}
