using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory (ActionCategory.ScriptControl)]
	public class RoarAction : FsmStateAction {

		public override void Awake ()
		{
			base.Awake ();
		}

		public override void OnEnter ()
		{
			Fsm.GameObject.GetComponent<EnemyCharacter> ().navAgent.isStopped = true;
			Animator animator = Fsm.GameObject.GetComponent<Animator> ();
			animator.SetBool ("roar", true);
			base.OnEnter ();
		}

		public override void OnUpdate ()
		{
			Animator animator = Fsm.GameObject.GetComponent<Animator> ();
			if ( !animator.GetBool("roar") && Fsm.GameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Base Layer.Attack_standby")) {
				Fsm.Event ("OnRoarDone");
			}
			base.OnUpdate ();
		}

		public override void OnExit ()
		{
			base.OnExit ();
		}
	}

}