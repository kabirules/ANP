using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SA.Common.Models.UI.Core;

namespace SA.AN.TWITTER {
	
	public class TwitterApiTab : FeatureTab {

		private static bool IsUserInfoLoaded = false;
		private static bool IsAuthenticated = false;
		
		public Texture2D ImageToShare;

		public Text ConnectButtonText;
		public Button ConnectButton;

		public Image Avatar;
		public Text Location;
		public Text Language;
		public Text Status;
		public Text Name;
		public Button[] AuthDependedButtons;

		private Sprite AvatarSprite = null;
		
		void Awake() {
			AndroidTwitterManager.Instance.OnTwitterInitedAction += OnTwitterInitedAction;
			AndroidTwitterManager.Instance.OnPostingCompleteAction += OnPostingCompleteAction;
			AndroidTwitterManager.Instance.OnUserDataRequestCompleteAction += OnUserDataRequestCompleteAction;
			AndroidTwitterManager.Instance.OnAuthCompleteAction += OnAuthCompleteAction;
			
			//You can use:
			//AndroidTwitterManager.instance.Init();
			//if TWITTER_CONSUMER_KEY and TWITTER_CONSUMER_SECRET was alredy set in 
			//Window -> Mobile Social Plugin -> Edit Settings menu.
			
			AndroidTwitterManager.Instance.Init();
		}
		
		void FixedUpdate() {
			if(IsAuthenticated) {
				ConnectButtonText.text = "Disconnect";
				Name.text = "Player Connected";
				foreach(Button button in AuthDependedButtons) {
					button.interactable = true;
				}
			} else {
				foreach(Button button in AuthDependedButtons) {
					button.interactable = false;
				}
				ConnectButtonText.text = "Connect";
				Name.text = "Player Disconnected";
				
				return;
			}		
			
			if(IsUserInfoLoaded) {			
				
				if(AndroidTwitterManager.Instance.userInfo.profile_image != null) {
					if (AvatarSprite == null) {
						AvatarSprite = Sprite.Create(AndroidTwitterManager.Instance.userInfo.profile_image,
							new Rect(
								0.0f,
								0.0f,
								AndroidTwitterManager.Instance.userInfo.profile_image.width,
								AndroidTwitterManager.Instance.userInfo.profile_image.height),
						    new Vector2(0.5f, 0.5f));
					}
					Avatar.sprite = AvatarSprite;
				}
				
				Name.text = AndroidTwitterManager.Instance.userInfo.name + " aka " + AndroidTwitterManager.Instance.userInfo.screen_name;
				Location.text = AndroidTwitterManager.Instance.userInfo.location;
				Language.text = AndroidTwitterManager.Instance.userInfo.lang;
				Status.text = AndroidTwitterManager.Instance.userInfo.status.text;			
			}		
		}
		
		public void Connect() {
			if(!IsAuthenticated) {
				AndroidTwitterManager.Instance.AuthenticateUser();
			} else {
				LogOut();
			}
		}
		
		public void PostWithAuthCheck() {
			AndroidTwitterManager.Instance.PostWithAuthCheck("Hello, I'am posting this from my app");
		}
		
		public void PostNativeScreenshot() {
			StartCoroutine(PostTWScreenshot());
		}
		
		public void PostMSG() {
			AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share",  "twi");
		}	
		
		public void PostImage() {
			AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", ImageToShare,  "twi");
		}	
		
		private IEnumerator PostTWScreenshot() {		
			yield return new WaitForEndOfFrame();
			// Create a texture the size of the screen, RGB24 format
			int width = Screen.width;
			int height = Screen.height;
			Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
			// Read screen contents into the texture
			tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
			tex.Apply();
			
			AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", tex,  "twi");
			
			Destroy(tex);		
		}
		
		
		public void LoadUserData() {
			AndroidTwitterManager.Instance.LoadUserData();		
		}
		
		
		void Test() {
			TW_OAuthAPIRequest request =  TW_OAuthAPIRequest.Create();
			
			//request.AddParam("count", 1);
			request.Send("https://api.twitter.com/1.1/statuses/home_timeline.json");
			
			request.OnResult += OnResult;
		}
		
		
		void OnResult (TW_APIRequstResult result) {
			Debug.Log("Is Request Succeeded: " + result.IsSucceeded);
			Debug.Log("Responce data:");
			Debug.Log(result.responce);
		}
		
		public void PostMessage() {
			AndroidTwitterManager.Instance.Post("Hello, I'am posting this from my app");
		}
		
		public void PostScreehShot() {
			StartCoroutine(PostScreenshot());
		}	
		
		// --------------------------------------
		// EVENTS
		// --------------------------------------
		
		void OnUserDataRequestCompleteAction (TWResult result) {
			if(result.IsSucceeded) {
				IsUserInfoLoaded = true;
				AndroidTwitterManager.Instance.userInfo.LoadProfileImage();
				AndroidTwitterManager.Instance.userInfo.LoadBackgroundImage();
			} else {
				Debug.Log("Opps, user data load failed, something was wrong");
			}
		}	
		
		void OnPostingCompleteAction (TWResult result) {
			if(result.IsSucceeded) {
				Debug.Log("Congrats. You just posted something to Twitter!");
			} else {
				Debug.Log("Oops! Posting failed. Something went wrong.");
			}
		}
		
		void OnAuthCompleteAction (TWResult result) {
			if(result.IsSucceeded) {
				//user authed
				OnAuth();
			}
		}
		
		void OnTwitterInitedAction (TWResult result) {
			
			if(AndroidTwitterManager.Instance.IsAuthed) {
				//user authed
				OnAuth();
			}
		}
		
		private void OnAuth() {
			IsAuthenticated = true;
		}
		
		// --------------------------------------
		// Aplication Only API Maehtods
		// --------------------------------------	
		
		public void RetrieveTimeLine() {
			TW_UserTimeLineRequest r =  TW_UserTimeLineRequest.Create();
			r.ActionComplete += OnTimeLineRequestComplete;
			r.AddParam("screen_name", "unity3d");
			r.AddParam("count", "1");
			r.Send();
		}	
		
		public void UserLookUpRequest() {
			TW_UsersLookUpRequest r =  TW_UsersLookUpRequest.Create();
			r.ActionComplete += OnLookUpRequestComplete;
			r.AddParam("screen_name", "unity3d");
			r.Send();
		}
		
		public void FriedsidsRequest() {
			TW_FriendsIdsRequest r =  TW_FriendsIdsRequest.Create();
			r.ActionComplete += OnIdsLoaded;
			r.AddParam("screen_name", "unity3d");
			r.Send();
		}
		
		public void FollowersidsRequest() {
			TW_FollowersIdsRequest r =  TW_FollowersIdsRequest.Create();
			r.ActionComplete += OnIdsLoaded;
			r.AddParam("screen_name", "unity3d");
			r.Send();
		}
		
		public void TweetSearch() {
			TW_SearchTweetsRequest r =  TW_SearchTweetsRequest.Create();
			r.ActionComplete += OnSearchRequestComplete;
			r.AddParam("q", "@noradio");
			r.AddParam("count", "1");
			r.Send();
		}
		
		// --------------------------------------
		// Events
		// --------------------------------------
		
		private void OnIdsLoaded(TW_APIRequstResult result) {		
			if(result.IsSucceeded) {
				AN_PoupsProxy.showMessage("Ids Request Succeeded", "Totals ids loaded: " + result.ids.Count);
				Debug.Log(result.ids.Count);
			} else {
				Debug.Log(result.responce);
				AN_PoupsProxy.showMessage("Ids Request Failed", result.responce);
			}
		}
		
		
		private void OnLookUpRequestComplete(TW_APIRequstResult result) {
			if(result.IsSucceeded) {
				string msg = "User Id: ";
				msg+= result.users[0].id;
				msg+= "\n";
				msg+= "User Name:" + result.users[0].name;
				
				AN_PoupsProxy.showMessage("User Info Loaded", msg);
				Debug.Log(msg);
			} else {
				Debug.Log(result.responce);
				AN_PoupsProxy.showMessage("User Info Failed", result.responce);
			}
		}	
		
		private void OnSearchRequestComplete(TW_APIRequstResult result) {		
			if(result.IsSucceeded) {
				string msg = "Tweet text:" + "\n";
				msg+= result.tweets[0].text;
				
				AN_PoupsProxy.showMessage("Tweet Search Request Succeeded", msg);
				Debug.Log(msg);
			} else {
				Debug.Log(result.responce);
				AN_PoupsProxy.showMessage("Tweet Search Request Failed", result.responce);
			}		
		}	
		
		private void OnTimeLineRequestComplete(TW_APIRequstResult result) {
			if(result.IsSucceeded) {
				string msg = "Last Tweet text:" + "\n";
				msg+= result.tweets[0].text;
				
				AN_PoupsProxy.showMessage("Time Line Request Succeeded", msg);
				Debug.Log(msg);
			} else {
				Debug.Log(result.responce);
				AN_PoupsProxy.showMessage("Time Line Request Failed", result.responce);
			}		
		}	
		
		// --------------------------------------
		// PRIVATE METHODS
		// --------------------------------------
		
		private IEnumerator PostScreenshot() {		
			yield return new WaitForEndOfFrame();
			// Create a texture the size of the screen, RGB24 format
			int width = Screen.width;
			int height = Screen.height;
			Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
			// Read screen contents into the texture
			tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
			tex.Apply();
			
			AndroidTwitterManager.Instance.Post("My app ScreehShot", tex);
			
			Destroy(tex);		
		}
		
		private void LogOut() {
			IsUserInfoLoaded = false;
			
			IsAuthenticated = false;
			
			AndroidTwitterManager.Instance.LogOut();
		}
	}
}
