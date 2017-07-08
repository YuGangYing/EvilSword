using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Slash : MonoBehaviour {

	public CanvasGroup slash01;
	public CanvasGroup slash02;
	public AnimationCurve curve;
	public float toggleDuration = 3;
	public Button btn_back;

	void Start () {
		slash01.alpha = 0;
		slash02.alpha = 0;
		slash01.DOFade (1,toggleDuration).SetEase(curve).OnComplete(()=>{
			slash01.DOFade (0,toggleDuration).SetEase(curve);
		});
		slash02.DOFade (1, toggleDuration).SetEase(curve).SetDelay (toggleDuration).OnComplete(()=>{
			slash02.DOFade (0, toggleDuration).SetEase(curve).OnComplete(()=>{
				Invoke("ChangeScene",2);
			});
		});
		btn_back.onClick.AddListener (()=>{
			SceneLoader.LoadLogin();
		});
	}

	void ChangeScene(){
		SceneLoader.LoadLogin();
	}



}
