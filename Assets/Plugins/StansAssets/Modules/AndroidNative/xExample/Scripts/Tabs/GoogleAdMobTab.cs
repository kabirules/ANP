using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using SA.Common.Models.UI.Core;

namespace SA.AN.ADVERTISING {
	
	public class GoogleAdMobTab : FeatureTab {

		//replace with your ids
		private const string MY_BANNERS_AD_UNIT_ID		 = "ca-app-pub-6101605888755494/1824764765";
		private const string MY_INTERSTISIALS_AD_UNIT_ID =  "ca-app-pub-6101605888755494/3301497967";
		private const string MY_REWARDED_VIDEO_AD_UNIT_ID =  "ca-app-pub-6101605888755494/5378283960";
		
		private GoogleMobileAdBanner Banner;
		private GoogleMobileAdBanner SmartBanner;
		
		private bool IsInterstisialsAdReady = false;

		[SerializeField]
		private Button ShowInterstitialButton;

		public Toggle CustomPosTgl;
		public Toggle UpperLeftTgl;
		public Toggle UpperCenterTgl;
		public Toggle BottomLeftTgl;
		public Toggle BottomCenterTgl;
		public Toggle BottomRightTgl;

		public Button BannerHideBtn;
		public Button BannerCreateBtn;
		public Button BannerRefreshBtn;
		public Button BannerChangePosToMiddleBtn;
		public Button BannerChangePosRandomBtn;
		public Button BannerDestroyBtn;

		public Toggle SmartBotPosTgl;
		public Toggle SmartTopPosTgl;

		public Button SmartHide;
		public Button SmartCreate;
		public Button SmartRefresh;
		public Button SmartDestroy;

		private Nullable<TextAnchor> BannerPosition = null;
		private TextAnchor SmartBannerPosition = TextAnchor.UpperCenter;

		//--------------------------------------
		// INITIALIZE
		//--------------------------------------	
		
		void Start() {
			
			AndroidAdMob.Client.Init(MY_BANNERS_AD_UNIT_ID);
			
			//If yoi whant to use Interstisial ad also, you need to set additional ad unin id for Interstisial as well
			AndroidAdMob.Client.SetInterstisialsUnitID(MY_INTERSTISIALS_AD_UNIT_ID);
			//If yoi whant to use Rewarded Video Ads also, you need to set additional ad unin id for Rewarded Video as well
			AndroidAdMobController.Instance.SetRewardedVideoAdUnitID(MY_REWARDED_VIDEO_AD_UNIT_ID);
			
			//Optional, add data for better ad targeting
			AndroidAdMob.Client.SetGender(GoogleGender.Male);
			AndroidAdMob.Client.AddKeyword("game");
			AndroidAdMob.Client.SetBirthday(1989, AndroidMonth.MARCH, 18);
			AndroidAdMob.Client.TagForChildDirectedTreatment(false);
			
			//Causes a device to receive test ads. The deviceId can be obtained by viewing the logcat output after creating a new ad
			//AndroidAdMobController.instance.AddTestDevice("6B9FA8031AEFDC4758B7D8987F77A5A6");
			
			AndroidAdMob.Client.OnInterstitialLoaded += OnInterstisialsLoaded; 
			AndroidAdMob.Client.OnInterstitialOpened += OnInterstisialsOpen;
			
			AndroidAdMobController.Instance.OnRewardedVideoLoaded += HandleOnRewardedVideoLoaded;
			AndroidAdMobController.Instance.OnRewardedVideoAdClosed += HandleOnRewardedVideoAdClosed;
			
			//listening for InApp Event
			//You will only receive in-app purchase (IAP) ads if you specifically configure an IAP ad campaign in the AdMob front end.
			AndroidAdMob.Client.OnAdInAppRequest += OnInAppRequest;

			///Toggle switches logic implementation
			CustomPosTgl.onValueChanged.AddListener ((b) => { if (b) BannerPosition = null;});
			UpperLeftTgl.onValueChanged.AddListener ((b) => { if (b) BannerPosition = TextAnchor.UpperLeft;});
			UpperCenterTgl.onValueChanged.AddListener ((b) => { if (b) BannerPosition = TextAnchor.UpperCenter;});
			BottomLeftTgl.onValueChanged.AddListener ((b) => { if (b) BannerPosition = TextAnchor.LowerLeft;});
			BottomCenterTgl.onValueChanged.AddListener ((b) => { if (b) BannerPosition = TextAnchor.LowerCenter;});
			BottomRightTgl.onValueChanged.AddListener ((b) => { if (b) BannerPosition = TextAnchor.LowerRight;});

			SmartTopPosTgl.onValueChanged.AddListener ((b) => { if (b) SmartBannerPosition = TextAnchor.UpperCenter;});
			SmartBotPosTgl.onValueChanged.AddListener ((b) => { if (b) SmartBannerPosition = TextAnchor.LowerCenter;});
		}
		
		void HandleOnRewardedVideoAdClosed () { }
		
		void HandleOnRewardedVideoLoaded () { }
		
		public void StartInterstitialAd() {
			AndroidAdMob.Client.StartInterstitialAd ();
		}
		
		public void LoadInterstitialAd() {
			AndroidAdMob.Client.LoadInterstitialAd ();
		}
		
		public void ShowInterstitialAd() {
			AndroidAdMob.Client.ShowInterstitialAd ();
		}
		
		public void LoadRewardedVideoAd () {
			AndroidAdMobController.Instance.LoadRewardedVideo();
		}
		
		public void ShowRewardedVideoAd () {
			AndroidAdMobController.Instance.ShowRewardedVideo();
		}
		
		public void BannerHide() {
			Banner.Hide();
		}
		
		public void BannerShow() {
			Banner = BannerPosition != null ? AndroidAdMob.Client.CreateAdBanner(BannerPosition.Value, GADBannerSize.BANNER)
				: AndroidAdMob.Client.CreateAdBanner(300, 100, GADBannerSize.BANNER);
		}
		
		public void BannerRefresh() {
			Banner.Refresh();
		}
		
		public void BannerDestroy() {
			AndroidAdMob.Client.DestroyBanner(Banner.id);
			Banner = null;
		}
		
		public void SmartBannerHide() {
			SmartBanner.Hide();
		}	
		
		public void SmartBannerShow() {
			SmartBanner = AndroidAdMob.Client.CreateAdBanner(SmartBannerPosition, GADBannerSize.SMART_BANNER);
		}
		
		public void SmartBannerRefresh() {
			SmartBanner.Refresh();
		}
		
		public void SmartBannerDestroy() {
			AndroidAdMob.Client.DestroyBanner(SmartBanner.id);
			SmartBanner = null;
		}
		
		public void ChnagePostToMiddle() {
			Banner.SetBannerPosition(TextAnchor.MiddleCenter);
		}
		
		public void ChangePostRandom() {
			Banner.SetBannerPosition(UnityEngine.Random.Range(0, Screen.width), UnityEngine.Random.Range(0, Screen.height));
		}	
		
		//--------------------------------------
		//  PUBLIC METHODS
		//--------------------------------------	
		
		void FixedUpdate() {
			if(IsInterstisialsAdReady) {
				ShowInterstitialButton.interactable = true;
			} else {
				ShowInterstitialButton.interactable = false;
			}
			
			if (Banner != null) {			
				BannerDestroyBtn.interactable = true;
				
				if (Banner.IsLoaded) {
					BannerRefreshBtn.interactable = true;
					BannerChangePosToMiddleBtn.interactable = true;
					BannerChangePosRandomBtn.interactable = true;
					if (Banner.IsOnScreen) {
						BannerHideBtn.interactable = true;
						BannerCreateBtn.interactable = false;
					} else {
						BannerHideBtn.interactable = false;
						BannerCreateBtn.interactable = true;
					}
				} else { 
					BannerRefreshBtn.interactable = false;
					BannerChangePosToMiddleBtn.interactable = false;
					BannerChangePosRandomBtn.interactable = false;
					BannerHideBtn.interactable = false;
					BannerCreateBtn.interactable = false;
				}
			} else {			
				BannerHideBtn.interactable = false;
				BannerCreateBtn.interactable = true;
				BannerRefreshBtn.interactable = false;
				BannerDestroyBtn.interactable = false;
				BannerChangePosToMiddleBtn.interactable = false;
				BannerChangePosRandomBtn.interactable = false;
			}
			
			if(SmartBanner != null) {			
				SmartDestroy.interactable = true;
				
				if(SmartBanner.IsLoaded) {
					SmartRefresh.interactable = true;
					if(SmartBanner.IsOnScreen) {
						SmartHide.interactable = true;
						SmartCreate.interactable = false;
					} else {
						SmartHide.interactable = false;
						SmartCreate.interactable = true;
					}
				} else { 
					SmartRefresh.interactable = false;
					SmartHide.interactable = false;
					SmartCreate.interactable = false;
				}			
			} else {
				SmartHide.interactable = false;
				SmartCreate.interactable = true;
				SmartRefresh.interactable = false;
				SmartDestroy.interactable = false;
			}		
		}
		
		//--------------------------------------
		//  GET/SET
		//--------------------------------------
		
		//--------------------------------------
		//  EVENTS
		//--------------------------------------
		
		private void OnInterstisialsLoaded() {
			IsInterstisialsAdReady = true;
		}
		
		private void OnInterstisialsOpen() {
			IsInterstisialsAdReady = false;
		}
		
		private void OnInAppRequest(string productId) {
			AN_PoupsProxy.showMessage ("In App Request", "In App Request for product Id: " + productId + " received");
			
			//Then you should perfrom purchase  for this product id, using this or another game billing plugin
			//Once the purchase is complete, you should call RecordInAppResolution with one of the constants defined in GADInAppResolution:
			
			AndroidAdMob.Client.RecordInAppResolution(GADInAppResolution.RESOLUTION_SUCCESS);
			
		}
		
		
		
		//--------------------------------------
		//  PRIVATE METHODS
		//--------------------------------------
		
		//--------------------------------------
		//  DESTROY
		//--------------------------------------

	}
}