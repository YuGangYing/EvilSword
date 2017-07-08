using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIFramework;

public class BattleScenePanel : BasePage
{

	public GameObject itemPrefab;
	public VerticalLayoutGroup layout;

	protected override void Awake ()
	{
		base.Awake ();
	}

	void InitItems ()
	{
		for (int i = 0; i < CSVManager.GetInstance ().sceneList.Count; i++) {
			Instantiate (itemPrefab);
		}
	}

}
