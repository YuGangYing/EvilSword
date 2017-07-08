using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

namespace UIFramework
{
	public class UIManager : SingleMonoBehaviour<UIManager>
	{

		Dictionary<UILayerType,Transform> m_uiLayers;
		public Transform container;

		protected override void Awake ()
		{
			base.Awake ();
			mPages = new Dictionary<Type, BasePage> ();
			InitLayers ();
			StartCoroutine (_Load ());
		}

		IEnumerator _Load ()
		{
//			InitPage<common.mypage.mypage.MyPage> ("sprite_mypage", "prefab_mypage", "MyPage", m_uiLayers [UILayerType.Common], false, true);
			yield return null;
			InitPage<BattleScenePanel> ("BattleScenePanel".ToLower() + ".assetbundle", "BattleScenePanel",  m_uiLayers [UILayerType.Common], false, true);
			yield return null;
			ShowPage<BattleScenePanel> (false);
			Resources.UnloadUnusedAssets ();
		}

		public void DisableUIEvent ()
		{
			GetComponent<GraphicRaycaster> ().enabled = false;
		}

		public void EnableUIEvent ()
		{
			GetComponent<GraphicRaycaster> ().enabled = true;
		}

		void InitPage (System.Type t)
		{
//			if (t == typeof(Memorial)) {
//				InitPage<Memorial> ("sprite_memorial", "prefab_memorial", "Memorial", m_uiLayers [UILayerType.Common], false, true);
//			}
		}

		public Dictionary<string,T> ArrayToDic<T> (T[] list) where T:UnityEngine.Object
		{
			Dictionary<string,T> dic = new Dictionary<string, T> ();
			foreach (T t in list) {
				dic.Add (t.name, t);
			}
			return dic;
		}

		public void InitLayers ()
		{
			Transform tempLayer;
			m_uiLayers = new Dictionary<UILayerType, Transform> ();
			Debug.Log ("InitLayers");
			//底层
			tempLayer = AddALayer ("UILayer_Bottom", container);
			m_uiLayers.Add (UILayerType.Bottom, tempLayer);

			//场景辅助层
			tempLayer = AddALayer ("UILayer_MapSceneAbout", container, false);
			m_uiLayers.Add (UILayerType.MapSceneAbout, tempLayer);

			//固定面板层
			tempLayer = AddALayer ("UILayer_Fixed", container, false);
			m_uiLayers.Add (UILayerType.Fixed, tempLayer);

			//普通面板层
			tempLayer = AddALayer ("UILayer_Common", container);
			m_uiLayers.Add (UILayerType.Common, tempLayer);

			//普通面板层
			tempLayer = AddALayer ("UILayer_Movie", container);
			m_uiLayers.Add (UILayerType.Movie, tempLayer);

			//普通上层面板层
			tempLayer = AddALayer ("UILayer_FontCommon", container);
			m_uiLayers.Add (UILayerType.FontCommon, tempLayer);

			//UI特效层层
			tempLayer = AddALayer ("UIEffectLayer", container, false);
			m_uiLayers.Add (UILayerType.UIEffect, tempLayer);

			//顶层
			tempLayer = AddALayer ("UILayer_Top", container);
			m_uiLayers.Add (UILayerType.Top, tempLayer);

			//遮罩层
			tempLayer = AddALayer ("UILayer_Mask", container);
			m_uiLayers.Add (UILayerType.Mask, tempLayer);
		}

		private Transform AddALayer (string name, Transform parent, bool mouseEventable = true)
		{
			GameObject retLayer;
			retLayer = new GameObject (name);
			retLayer.layer = LayerMask.NameToLayer ("UI");
			RectTransform rect = retLayer.AddComponent<RectTransform> ();
			retLayer.transform.SetParent (parent);
			retLayer.transform.localPosition = Vector3.zero;
			retLayer.transform.localScale = Vector3.one;
			rect.SetInsetAndSizeFromParentEdge (RectTransform.Edge.Left, 0, 0);
			rect.SetInsetAndSizeFromParentEdge (RectTransform.Edge.Top, 0, 0);
			CanvasGroup gc = retLayer.AddComponent<CanvasGroup> ();
			gc.blocksRaycasts = mouseEventable;
			gc.interactable = mouseEventable;
			rect.anchorMin = new Vector2 (0f, 0f);
			rect.anchorMax = new Vector2 (1f, 1f);
			return retLayer.transform;
		}

		//UI层类型
		public enum UILayerType
		{
			/// <summary>
			/// //最顶层
			/// </summary>
			Top,
			/// <summary>
			/// //UI特效层
			/// </summary>
			UIEffect,
			/// <summary>
			/// //普通上一层
			/// </summary>
			FontCommon,
			/// <summary>
			/// //任务、剧情
			/// </summary>
			Movie,
			/// <summary>
			/// //普通面板层
			/// </summary>
			Common,
			/// <summary>
			/// //固定面板层
			/// </summary>
			Fixed,
			/// <summary>
			/// //遮罩层
			/// </summary>
			Mask,
			/// <summary>
			/// 场景辅助层
			/// </summary>
			MapSceneAbout,
			/// <summary>
			/// //最底层
			/// </summary>
			Bottom
		}

		public GameObject InitUI (string prefabAB, string prefabName, Transform parent, bool isActive)
		{
			GameObject prefab = RogerAssetBundle.AssetManager.GetAssetFromLocal<GameObject> (prefabAB, prefabName);
			if (prefab == null) {
				Debug.LogError ("prefabName:" + prefabName + " is not exiting.");
				return null;
			}
			prefab.SetActive (false);
			if (prefab == null) {
				Debug.LogWarning (prefabName + " prefab is not existing!");
				return null;
			}
			GameObject go = Instantiate (prefab) as GameObject;
			RectTransform rect = go.GetComponent<RectTransform> ();
			go.transform.SetParent (parent);
			go.transform.localPosition = Vector3.zero;
			go.transform.localScale = Vector3.one;
			rect.SetInsetAndSizeFromParentEdge (RectTransform.Edge.Left, 0, 0);
			rect.SetInsetAndSizeFromParentEdge (RectTransform.Edge.Top, 0, 0);
			rect.anchorMin = new Vector2 (0f, 0f);
			rect.anchorMax = new Vector2 (1f, 1f);
			go.SetActive (isActive);
			return go;
		}

		Dictionary<System.Type,BasePage> mPages;

		void InitPage<T> (string prefabAB, string prefabName, Transform parent, bool isActive, bool isBackPage) where T:BasePage
		{
//			Resources.UnloadUnusedAssets ();
			GameObject page = InitUI (prefabAB, prefabName, parent, isActive);
			RectTransform rectTrans = page.GetComponent<RectTransform> ();
			if (rectTrans != null) {
				rectTrans.anchorMin = new Vector2 (0.5f, 0.5f);
				rectTrans.anchorMax = new Vector2 (0.5f, 0.5f);
				rectTrans.sizeDelta = new Vector2 (1082, 1920);
				rectTrans.anchoredPosition3D = Vector3.zero;
			}
			if (page == null)
				return;
			T t = page.GetComponent<T> ();
			t.isClosePrepageOnOpen = isBackPage;
			if (t == null) {
				Debug.LogWarning (typeof(T).ToString () + " is not existing on " + prefabName);
				return;
			}
			AddPage<T> (t);
		}

		void AddPage<T> (T t) where T:BasePage
		{
			mPages.Add (typeof(T), t);
		}

		public T GetPage<T> () where T : BasePage
		{
			if (!mPages.ContainsKey (typeof(T))) {
				InitPage (typeof(T));
			}
			if (mPages.ContainsKey (typeof(T))) {
				T t = (T)mPages [typeof(T)];
				return t;
			}
			return null;
		}

		public List<BasePage> pageQueue = new List<BasePage> ();
		BasePage mBackPage;

		public void Back (BasePage p, bool whiteinout)
		{
//			if (mIsInWhiteOutIn)
//				return;
			if (whiteinout) {
				mBackPage = p;
//				GameInitialization.GetInstance ().whiteInOut.Play (_Back, WhiteOutInEnd);
//				mIsInWhiteOutIn = true;
			} else {
				mBackPage = p;
				_Back ();
				WhiteOutInEnd ();
			}
		}

		void WhiteOutInEnd ()
		{
//			mIsInWhiteOutIn = false;
		}

		void _Back ()
		{
			mBackPage.gameObject.SetActive (false);
			if (pageQueue != null && pageQueue.Count > 0) {
				while (pageQueue.Count > 0) {
					BasePage page = pageQueue [pageQueue.Count - 1];
					if (mBackPage == page) {
						page.gameObject.SetActive (false);
						pageQueue.Remove (page);
					} else {
						break;
					}
				}
			}
			if (pageQueue != null && pageQueue.Count > 0) {
				for (int i = pageQueue.Count - 1; i >= 0; i--) {
					BasePage page = pageQueue [i];
					page.gameObject.SetActive (true);
					if (page.isClosePrepageOnOpen) {
						break;
					}
				}
			} 
		}

		public T ShowPage<T> (bool whiteoutin = false) where T : BasePage
		{
			T t = GetPage<T> ();
			Debug.Log (t);
			if (t != null) {
				if (whiteoutin) {
					mBasePage = t;
//					GameInitialization.GetInstance ().whiteInOut.Play (HideOther, WhiteOutInEnd);
//					mIsInWhiteOutIn = true;
				} else {
					mBasePage = t;
					HideOther ();
				}
			}
			Resources.UnloadUnusedAssets ();
			return t;
		}

		BasePage mBasePage;

		void HideOther ()
		{
			if (mBasePage.isClosePrepageOnOpen) {
				foreach (BasePage page in pageQueue) {
					page.gameObject.SetActive (false);
				}
			}
			pageQueue.Add (mBasePage);
			mBasePage.gameObject.SetActive (true);
			mBasePage.transform.SetSiblingIndex (99);
			mBasePage.action_back = null;
		}

		public T HidePage<T> () where T : BasePage
		{
			T t = GetPage<T> ();
			if (t != null) {
				t.gameObject.SetActive (false);
			}
			return t;
		}

		public void ShowPageByName (string pageName, bool whiteoutin = false)
		{
			Type type = Type.GetType (pageName);
			if (!mPages.ContainsKey (type)) {
				InitPage (type);
			}
			if (mPages.ContainsKey (type)) {
				BasePage basePage = mPages [type];
				mBasePage = basePage;
				if (whiteoutin) {
//					GameInitialization.GetInstance ().whiteInOut.Play (HideOther, WhiteOutInEnd);
				} else {
					HideOther ();
					WhiteOutInEnd ();
				}
//				mIsInWhiteOutIn = true;
			}
		}

		public Dictionary<System.Type,BaseDialog> mDialogs;

		void InitDialogs ()
		{
			mDialogs = new Dictionary<System.Type, BaseDialog> ();
//			InitDialog<ItemUseDialog> ("sprite_common", "prefab_dialog", "ItemUseDlg", m_uiLayers [UILayerType.Mask], false);
		}

		void InitDialog<T> (string spriteAB, string prefabAB, string prefabName, Transform parent, bool isActive) where T:BaseDialog
		{
			GameObject dialog = InitUI (prefabAB, prefabName, parent, isActive);
			if (dialog == null)
				return;
			T t = dialog.GetComponent<T> ();
			if (t == null) {
				Debug.LogWarning (typeof(T).ToString () + " is not existing on " + prefabName);
				return;
			}
			AddDialog<T> (t);
		}

		void AddDialog<T> (T t) where T:BaseDialog
		{
			mDialogs.Add (typeof(T), t);
		}

		public T GetDialog<T> () where T : BaseDialog
		{
			if (mDialogs.ContainsKey (typeof(T))) {
				T t = (T)mDialogs [typeof(T)];
				return t;
			}
			return null;
		}

		public T ShowDialog<T> () where T : BaseDialog
		{
			T t = GetDialog<T> ();
			if (t != null) {
				t.gameObject.SetActive (true);
				t.transform.SetSiblingIndex (99);
			}
			return t;
		}
	}
}