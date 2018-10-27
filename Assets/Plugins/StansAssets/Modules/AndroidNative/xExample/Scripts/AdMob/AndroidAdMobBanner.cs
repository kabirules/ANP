﻿////////////////////////////////////////////////////////////////////////////////
//  
// @module Android Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
#else
using UnityEngine.SceneManagement;
#endif



//Attach the script to the empty gameobject on your sceneS
public class AndroidAdMobBanner : MonoBehaviour {


	public string BannersUnityId;
	public GADBannerSize size = GADBannerSize.SMART_BANNER;
	public TextAnchor anchor = TextAnchor.LowerCenter;



	private static Dictionary<string, GoogleMobileAdBanner> _refisterdBanners = null;


	// --------------------------------------
	// Unity Events
	// --------------------------------------
	
	void Awake() {
		if(AndroidAdMobController.Instance.IsInited) {
			if(!AndroidAdMobController.Instance.BannersUunitId.Equals(BannersUnityId)) {
				AndroidAdMobController.Instance.SetBannersUnitID(BannersUnityId);
			} 
		} else {
			AndroidAdMobController.Instance.Init(BannersUnityId);
		}
	}

	void Start() {
		ShowBanner();
	}

	void OnDestroy() {
		HideBanner();
	}


	// --------------------------------------
	// PUBLIC METHODS
	// --------------------------------------

	public void ShowBanner() {
		GoogleMobileAdBanner banner;
		if(registerdBanners.ContainsKey(sceneBannerId)) {
			banner = registerdBanners[sceneBannerId];
		}  else {
			banner = AndroidAdMobController.Instance.CreateAdBanner(anchor, size);
			registerdBanners.Add(sceneBannerId, banner);
		}

		if(banner.IsLoaded && !banner.IsOnScreen) {
			banner.Show();
		}
	}

	public void HideBanner() {
		if(registerdBanners.ContainsKey(sceneBannerId)) {
			GoogleMobileAdBanner banner = registerdBanners[sceneBannerId];
			if(banner.IsLoaded) {
				if(banner.IsOnScreen) {
					banner.Hide();
				}
			} else {
				banner.ShowOnLoad = false;
			}
		}
	}

	// --------------------------------------
	// GET / SET
	// --------------------------------------


	public static Dictionary<string, GoogleMobileAdBanner> registerdBanners {
		get {
			if(_refisterdBanners == null) {
				_refisterdBanners = new Dictionary<string, GoogleMobileAdBanner>();
			}

			return _refisterdBanners;
		}
	}

	public string sceneBannerId {
		get {
			#if UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
			return Application.loadedLevelName + "_" + this.gameObject.name;
			#else
			return SceneManager.GetActiveScene().name + "_" + this.gameObject.name;
			#endif
		}
	}

	
}
