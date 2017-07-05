using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	public class BaseUnitAction : FsmStateAction
	{

		public string animatorStateName = "";
		public string onActionDoneEvent = "";
		protected float mExitTime;

	}
}