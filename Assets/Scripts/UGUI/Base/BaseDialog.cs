using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

namespace UIFramework{
	public class BaseDialog : MonoBehaviour
	{

		public Button btnClose;
		public Button btnYes;
		public Text txtTitle;
		public Text txtMsg;
		public UnityAction actionYes;
		public UnityAction actionClose;
		public Button btn_check;
		public Image img_check;
		public Text txt_check;
		protected bool mChecked = false;
		protected bool mHasOpenVoice = true;

		protected virtual void Awake ()
		{
			if(btnYes!=null)
				btnYes.onClick.AddListener (OnButtonYesClick);
			if (btnClose != null)
				btnClose.onClick.AddListener (OnButtonCloseClick);
//			if (mCheckableDic == null)
//				LoadChecked ();
			if (btn_check != null) {
				if(txt_check!=null)
//					txt_check.text = LanguageJP.NAME_DIALOG_NOT_SHOW2;
				btn_check.onClick.AddListener (OnCheck);
			}
		}

		protected bool isFirstLoad = true;
//		protected SESound soundYes = SESound.SE_002;
		protected virtual void OnEnable(){
//			if (btn_check != null) {
//				if(checkedType!=DialogCheckedType.None){
//					mChecked = GetChecked (checkedType.ToString());
//					if (img_check != null) {
//						img_check.gameObject.SetActive (mChecked);
//					}
//					if(mChecked){
//						gameObject.SetActive (false);
//						if (actionYes != null) {
//							actionYes ();
//						}
//					}
//				}
//			}
			mChecked = false;
			if (img_check != null)
				img_check.gameObject.SetActive (mChecked);
			if (isFirstLoad) {
				isFirstLoad = false;
			} else {
//				if(MyPage.GetInstance ()!=null && mHasOpenVoice)
//					MyPage.GetInstance ().PlaySEVoice (soundYes.ToString());
			}
		}

		public virtual void ShowBaseDialog (string title = "", string message = "", UnityAction action = null)
		{
			if (txtTitle != null)
				txtTitle.text = title;
			if (txtMsg != null)
				txtMsg.text = message;
			actionYes = action;
			gameObject.SetActive (true);
		}

		protected bool playYesSE = true;
		protected virtual void OnButtonYesClick ()
		{
			if (actionYes != null) {
				actionYes ();
				actionYes = null;
			}
			gameObject.SetActive (false);
			if (playYesSE) {
//				if (MyPage.GetInstance () != null)
//					MyPage.GetInstance ().PlaySEVoice (SESound.SE_002.ToString ());
//				else
//					GameInitialization.GetInstance ().PlayLocalSESound ();
			}
		}

		protected virtual void OnButtonCloseClick ()
		{
			if (actionClose != null) {
				actionClose ();
				actionClose = null;
			}
			gameObject.SetActive (false);
//			if (MyPage.GetInstance () != null)
//				MyPage.GetInstance ().PlaySEVoice (SESound.SE_003.ToString ());
//			else
//				GameInitialization.GetInstance ().PlayLocalSESound1 ();
		}

//		public static Dictionary<string,CheckableData> mCheckableDic;
//
		void OnCheck ()
		{
//			mChecked = GetChecked (checkedType.ToString());
			mChecked = !mChecked;
			if (img_check != null)
				img_check.gameObject.SetActive (mChecked);
//			SetChecked (checkedType.ToString(),mChecked);
		}
//
//		void LoadChecked ()
//		{
//			string cheackables = PlayerPrefs.GetString ("checkables");
//			Debug.Log (cheackables);
//			Checkable checkable = JsonUtility.FromJson<Checkable> (cheackables);
//			mCheckableDic = new Dictionary<string, CheckableData> ();
//			if(checkable!=null){
//				foreach (CheckableData cd in checkable.checkables) {
//					mCheckableDic.Add (cd.key, cd);
//				}
//			}
//			Debug.Log (mCheckableDic.Count);
//		}
//
//		bool GetChecked (string key)
//		{
//			CheckableData cd;
//			if (mCheckableDic.TryGetValue (key, out cd)) {
//				return cd.value;
//			}
//			return false;
//		}

//		void SetChecked (string key, bool isTrue)
//		{
//			CheckableData cd;
//			if (mCheckableDic.TryGetValue (key, out cd)) {
//				cd.value = isTrue;
//			} else {
//				cd = new CheckableData ();
//				cd.key = key;
//				cd.value = isTrue;
//				mCheckableDic.Add (key, cd);
//			}
//			SaveChecked ();
//		}
//
//		void SaveChecked ()
//		{
//			Checkable checkable = new YuRiYuRi.Checkable ();
//			foreach(CheckableData cd in mCheckableDic.Values){
//				checkable.checkables.Add (cd);
//			}
//			string checkableJson = JsonUtility.ToJson (checkable);
//			PlayerPrefs.SetString ("checkables", checkableJson);
//			PlayerPrefs.Save ();
//		}

	}
}
