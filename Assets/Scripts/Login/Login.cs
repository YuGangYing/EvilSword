using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {

	public Button btn_login;

	void Awake(){
		btn_login.onClick.AddListener (()=>{
			SceneLoader.LoadMain();
		});
	}

}
