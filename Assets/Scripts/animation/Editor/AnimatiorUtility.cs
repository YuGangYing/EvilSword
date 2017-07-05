using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class AnimatiorUtility : MonoBehaviour {

	[MenuItem("Tools/InitAnimatorController")]
	public static void Init(){
		Object obj = Selection.activeObject;
		AnimatorController ac = obj as AnimatorController;
		InitAnimatorController (ac);
	}

	static void InitAnimatorController(AnimatorController animatorController){
		animatorController.AddParameter("Idol_a_to_b", AnimatorControllerParameterType.Trigger);
		animatorController.AddParameter("Idol_to_aha", AnimatorControllerParameterType.Trigger);
		animatorController.AddParameter("Idol_to_angry", AnimatorControllerParameterType.Trigger);
		animatorController.AddParameter("Idol_to_joy", AnimatorControllerParameterType.Trigger);
		var rootStateMachine = animatorController.layers[0].stateMachine;

		Dictionary<string,AnimatorState> stateDic = new Dictionary<string, AnimatorState> ();
		foreach(ChildAnimatorState state in rootStateMachine.states ){
			string stateName = state.state.name;
			stateName = stateName.Substring (stateName.LastIndexOf ('_') + 1);
			Debug.Log ("stateName:" + stateName);
			stateDic.Add (stateName,state.state);
		}
		AnimatorStateTransition trans =  stateDic ["a"].AddTransition (stateDic ["aha"]);
		trans.AddCondition (AnimatorConditionMode.If, 1, "Idol_to_aha");
		trans.hasExitTime = true;
		trans =  stateDic ["a"].AddTransition (stateDic ["angry"]);
		trans.AddCondition (AnimatorConditionMode.If, 1, "Idol_to_angry");
		trans.hasExitTime = true;
		trans =  stateDic ["a"].AddTransition (stateDic ["joy"]);
		trans.AddCondition (AnimatorConditionMode.If, 1, "Idol_to_joy");
		trans.hasExitTime = true;
		trans =  stateDic ["a"].AddTransition (stateDic ["b"]);
		trans.AddCondition (AnimatorConditionMode.If, 1, "Idol_a_to_b");
		trans.hasExitTime = true;

		trans =  stateDic ["b"].AddTransition (stateDic ["aha"]);
		trans.AddCondition (AnimatorConditionMode.If, 1, "Idol_to_aha");
		trans.hasExitTime = true;
		trans =  stateDic ["b"].AddTransition (stateDic ["angry"]);
		trans.AddCondition (AnimatorConditionMode.If, 1, "Idol_to_angry");
		trans.hasExitTime = true;
		trans =  stateDic ["b"].AddTransition (stateDic ["joy"]);
		trans.AddCondition (AnimatorConditionMode.If, 1, "Idol_to_joy");
		trans.hasExitTime = true;
	

		trans =  stateDic ["b"].AddTransition (stateDic ["a"]);
		trans.hasExitTime = true;
		trans =  stateDic ["aha"].AddTransition (stateDic ["a"]);
		trans.hasExitTime = true;
		trans =  stateDic ["angry"].AddTransition (stateDic ["a"]);
		trans.hasExitTime = true;
		trans =  stateDic ["joy"].AddTransition (stateDic ["a"]);
		trans.hasExitTime = true;

	}


}
