using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory (ActionCategory.ScriptControl)]
	public class DeathAction : BaseUnitAction
	{
		public override void Awake ()
		{
			base.Awake ();
		}

		public override void OnEnter ()
		{
			Debug.Log ("DeathAction");
			Fsm.GameObject.GetComponent<NavMeshAgent> ().isStopped = true;
			if (!string.IsNullOrEmpty (animatorStateName)) {
				Animator animator = Fsm.GameObject.GetComponentInChildren<Animator> (true);
				animator.PlayInFixedTime (animatorStateName);
				mExitTime = Time.time + Fsm.GameObject.GetComponentInChildren<Animator> (true).GetCurrentAnimatorStateInfo (0).length;
				GameObject.Destroy (Fsm.GameObject,Fsm.GameObject.GetComponentInChildren<Animator> (true).GetCurrentAnimatorStateInfo (0).length + 2);
			}

			base.OnEnter ();
		}

		public override void OnUpdate ()
		{
			if (mExitTime < Time.time) {
				if(!string.IsNullOrEmpty(onActionDoneEvent))
					Fsm.Event (onActionDoneEvent);
				Fsm.GameObject.GetComponentInChildren<Animator> (true).PlayInFixedTime("Base Layer.Death2");
			}
			base.OnUpdate ();
		}

		public override void OnExit ()
		{
			base.OnExit ();
		}
	}
}
