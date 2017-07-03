using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStandbyState : StateMachineBehaviour
{
	
	override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.GetComponent<EnemyCharacter> ().navAgent.isStopped = true;
	}

	override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}

	override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		EnemyCharacter enemyCharacter = animator.GetComponent<EnemyCharacter> ();
		int attackNumber = 7;
		for (int i = 1; i < attackNumber + 1; i++) {
			if (animator.GetBool ("attack" + i)) {
				return;
			}
		}
		if (animator.GetBool ("run_to_target")) {
			return;
		}

		if (enemyCharacter.IsInAttackRange ()) {
//			animator.SetBool ("attack" + Random.Range (1, attackNumber + 1).ToString (), true);
		} else if (enemyCharacter.IsInSearchRange ()) {
			animator.SetBool ("run_to_target", true);
		} 
	}

	override public void OnStateMove (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}

	override public void OnStateIK (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}

}
