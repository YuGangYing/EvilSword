using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

namespace UIFramework{
public class BasePage : MonoBehaviour {

	public bool isClosePrepageOnOpen;

	public Button btn_back;
	public Button btn_close;

	public UnityAction action_back;
	public UnityAction action_close;

	protected bool isFirstLoad = true;

	protected virtual void Awake(){
		if (btn_back != null)
			btn_back.onClick.AddListener (OnBack);
		if (btn_close != null)
			btn_close.onClick.AddListener (OnClose);
	}

	protected virtual void OnBack(){
		if (action_back != null)
			action_back ();
//		MyPage.GetInstance().PlaySEVoice(SESound.SE_003.ToString());
//		MyPage.GetInstance ().Back (this,false);
	}

	protected virtual void OnClose(){
		if (action_close != null)
			action_close ();
		gameObject.SetActive (false);
	}

	public virtual void OnActive(){
		
	}

	protected virtual void DisableButton(Button btn){
		btn.GetComponent<Image> ().color = Color.gray;
		btn.enabled = false;
	}

	protected virtual void EnableButton(Button btn){
		btn.GetComponent<Image> ().color = Color.white;
		btn.enabled = true;
	}
	}

}
