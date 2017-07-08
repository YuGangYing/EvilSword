using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader  {

	public static void LoadSlash(){
		SceneManager.LoadScene("Slash");
	}

	public static void LoadLogin(){
		SceneManager.LoadScene("Login");
	}

	public static void LoadMain(){
		SceneManager.LoadScene("Main");
	}

	public static void LoadDownload(){
		SceneManager.LoadScene("Download");
	}

	public static void LoadBattle(){
		SceneManager.LoadScene("Battle");
	}

}
