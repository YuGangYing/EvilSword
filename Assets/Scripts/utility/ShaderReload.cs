using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderReload : MonoBehaviour {

	void Awake(){
		#if UNITY_EDITOR
		Renderer[] rs = GetComponentsInChildren<Renderer> (true);
		for(int i=0;i< rs.Length;i++){
			Material[] mats = rs [i].sharedMaterials;
			for(int j=0;j<mats.Length;j++){
				mats [j].shader = Shader.Find (mats [j].shader.name);
			}
		}
		#endif
	}
}
