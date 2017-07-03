using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Animations;
#endif
[ExecuteInEditMode]
public class AnimatorManager : MonoBehaviour {
	#if UNITY_EDITOR
//	[MenuItem ("Animator/CreateTriggerAndTransition")]
//	public static void DoSomething1 () {
//		GameObject go = Selection.activeGameObject;
//		AnimatorController animator = go.GetComponent<Animator> ().runtimeAnimatorController;
//		AnimatorController controller = Selection.activeObject as AnimatorController;

//		animator.SetTrigger ("Idol_a");
//		animator.SetTrigger ("Idol_b");
//
//
//
//		AnimatorController acc = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/characters/character_1/ch_f_001/ch_f_003_001.controller");
//		Debug.Log (acc);
//		controller.AddParameter("Idol_a_to_b", AnimatorControllerParameterType.Trigger);
//		controller.AddParameter("Idol_to_aha", AnimatorControllerParameterType.Trigger);
//		controller.AddParameter("Idol_to_angry", AnimatorControllerParameterType.Trigger);
//		controller.AddParameter("Idol_to_joy", AnimatorControllerParameterType.Trigger);
//
//
//		Debug.Log(controller.animationClips.Length);
//
//		var rootStateMachine = controller.layers[0].stateMachine;
//		AnimatorState state = rootStateMachine.AddState ("a");
//		Debug.Log (go);
//	}

	public AnimatorController animatorController;
	public bool load;

	void Update(){
		if (load) {
			load = false;
//			animatorController.AddParameter("Idol_a_to_b", AnimatorControllerParameterType.Trigger);
//			animatorController.AddParameter("Idol_to_aha", AnimatorControllerParameterType.Trigger);
//			animatorController.AddParameter("Idol_to_angry", AnimatorControllerParameterType.Trigger);
//			animatorController.AddParameter("Idol_to_joy", AnimatorControllerParameterType.Trigger);

			foreach (ChildAnimatorState state in animatorController.layers[0].stateMachine.states) {
				state.state.AddStateMachineBehaviour<AnimStatusMachine> ();
//				Debug.logger.Log ();
			}
		}
	}
	#endif

}
