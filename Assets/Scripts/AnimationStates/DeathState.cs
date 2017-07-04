using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : StateMachineBehaviour {

	override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.speed = 0.3f;
	}

	override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.speed = 0;
	}

	override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.speed = Mathf.Min (1,animator.speed + Time.deltaTime / 10);
	}

	override public void OnStateMove (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}

	override public void OnStateIK (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}
}
