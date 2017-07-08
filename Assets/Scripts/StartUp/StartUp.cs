using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUp : MonoBehaviour {

	public GameObject coroutine;

	void Awake () {
		DontDestroyOnLoad (coroutine);
		SceneLoader.LoadSlash ();
	}

}
