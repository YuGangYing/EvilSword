using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunToTargetState : StateMachineBehaviour {

	override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.GetComponent<EnemyCharacter> ().navAgent.isStopped = false;
	}

	override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		
	}

	override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		EnemyCharacter enemyCharacter = animator.GetComponent<EnemyCharacter> ();
		enemyCharacter.MoveToTarget ();
		int attackNumber = 7;
		if ( enemyCharacter.IsInAttackRange()) {
			animator.SetBool("attack" + Random.Range(1, attackNumber + 1).ToString( ), true);
			animator.SetBool ("run_to_target",false);
		}
	}

	override public void OnStateMove (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}

	override public void OnStateIK (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}

}
