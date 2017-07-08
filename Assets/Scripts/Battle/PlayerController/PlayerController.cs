using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingleMonoBehaviour<PlayerController>
{

	public Transform joystick;
	public Canvas canvas;

	void Update ()
	{
//		if (Input.touchCount > 0) {
//			for (int i = 0; i < Input.touchCount; i++) {
//				if (Input.GetTouch (i).position.x < Screen.width / 2 && Input.GetTouch (i).position.y < Screen.height / 2) {
//					if (Input.GetTouch (i).phase == TouchPhase.Began) {
//						joystick.gameObject.SetActive (true);
//						joystick.GetComponent<RectTransform> ().anchoredPosition = RectTransformUtility.PixelAdjustPoint ((Vector2)Input.GetTouch (i).position, joystick, canvas);
//					} else if (Input.GetTouch (i).phase == TouchPhase.Ended) {
//						joystick.gameObject.SetActive (false);
//					}
//				}
//			}
//		}
	}

}
