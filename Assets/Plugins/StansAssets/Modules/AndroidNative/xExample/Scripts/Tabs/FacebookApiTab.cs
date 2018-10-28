using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SA.Common.Models.UI.Core;

namespace SA.AN.FACEBOOK {
	
	public class FacebookApiTab : FeatureTab {

		private static bool IsUserInfoLoaded = false;
		private static bool IsFrindsInfoLoaded = false;
		private static bool IsAuntificated = false;
		
		public Button[] ConnectionDependedntButtons;

		public Text ConnectButtonText;
		public Button ConnectButton;

		public Image Avatar;
		private Sprite AvatarSprite = null;

		public Text Location;
		public Text Language;
		public Text Mail;
		public Text Name;
		
		
		public Text f1Name;
		public Text f2Name;
		
		public Image f1ProfileLogo;
		public Image f2ProfileLogo;
		private Sprite f1Avatar = null;
		private Sprite f2Avatar = null;
		
		public Texture2D ImageToShare;
		
		public GameObject friends;
		
		private int startScore = 555;
		
		void Awake() {
			SPFacebook.OnInitCompleteAction += OnInit;
			SPFacebook.OnFocusChangedAction += OnFocusChanged;
			
			SPFacebook.OnAuthCompleteAction += OnAuth;
			
			SPFacebook.OnPostingCompleteAction += OnPost;
			
			//scores Api events
			SPFacebook.OnPlayerScoresRequestCompleteAction += OnPlayerScoreRequestComplete; 
			SPFacebook.OnAppScoresRequestCompleteAction += OnAppScoreRequestComplete; 
			SPFacebook.OnSubmitScoreRequestCompleteAction += OnSubmitScoreRequestComplete; 
			SPFacebook.OnDeleteScoresRequestCompleteAction += OnDeleteScoreRequestComplete; 
			
			SPFacebook.Instance.Init();
			
			SA_StatusBar.text = "initializing Facebook";
		}
		
		void HandleOnRevokePermission (FB_Result result)
		{
			Debug.Log("[HandleOnRevokePermission] result.IsSucceeded: " + result.IsSucceeded + " Responce: " + result.RawData);
		}

		
		void FixedUpdate() {
			if(IsAuntificated) {
				ConnectButtonText.text = "Disconnect";
				Name.text = "Player Connected";
				foreach(Button btn in ConnectionDependedntButtons) {
					btn.interactable = true;
				}
			} else {
				foreach(Button btn in ConnectionDependedntButtons) {
					btn.interactable = false;
				}
				ConnectButtonText.text = "Connect";
				Name.text = "Player Disconnected";
				
				friends.SetActive(false);
				return;
			}
			
			if(IsUserInfoLoaded) {
				if(SPFacebook.Instance.userInfo.GetProfileImage(FB_ProfileImageSize.square) != null) {
					if (AvatarSprite == null) {
						Texture2D avatar = SPFacebook.Instance.userInfo.GetProfileImage(FB_ProfileImageSize.square);
						AvatarSprite = Sprite.Create(avatar, new Rect(0.0f, 0.0f, avatar.width, avatar.height), new Vector2(0.5f, 0.5f));
						avatar = null;
					}

					Avatar.sprite = AvatarSprite;
					Name.text = SPFacebook.Instance.userInfo.Name + " aka " + SPFacebook.Instance.userInfo.UserName;
					Location.text = SPFacebook.Instance.userInfo.Location;
					Language.text = SPFacebook.Instance.userInfo.Locale;
				}
			}
			
			
			if(IsFrindsInfoLoaded) {
				friends.SetActive(true);
				int i = 0;
				if (SPFacebook.Instance.friendsList != null) {
					foreach(FB_UserInfo friend in SPFacebook.Instance.friendsList) {					
						if(i == 0) {
							f1Name.text = friend.Name;
							if(friend.GetProfileImage(FB_ProfileImageSize.square) != null) {
								if (f1Avatar == null) {
									Texture2D avatar = friend.GetProfileImage(FB_ProfileImageSize.square);
									f1Avatar = Sprite.Create(avatar, new Rect(0.0f, 0.0f, avatar.width, avatar.height), new Vector2(0.5f, 0.5f));
									avatar = null;
								}
								f1ProfileLogo.sprite = f1Avatar;
							} 
						} else {
							f2Name.text = friend.Name;
							if(friend.GetProfileImage(FB_ProfileImageSize.square) != null) {
								if (f1Avatar == null) {
									Texture2D avatar = friend.GetProfileImage(FB_ProfileImageSize.square);
									f2Avatar = Sprite.Create(avatar, new Rect(0.0f, 0.0f, avatar.width, avatar.height), new Vector2(0.5f, 0.5f));
									avatar = null;
								}
								f2ProfileLogo.sprite = f2Avatar;
							}
						}					
						i ++;
					}
				}
			} else {
				friends.SetActive(false);
			}
		}
		
		public void PostWithAuthCheck() {
			SPFacebook.Instance.FeedShare (
				link: "https://example.com/myapp/?storyID=thelarch",
				linkName: "The Larch",
				linkCaption: "I thought up a witty tagline about larches",
				linkDescription: "There are a lot of larch trees around here, aren't there?",
				picture: "https://example.com/myapp/assets/1/larch.jpg"
				);
		}	
		
		public void PostNativeScreenshot() {
			StartCoroutine(PostFBScreenshot());
		}
		
		public void PostImage() {
			AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", ImageToShare,  "facebook.katana");
		}
		
		private IEnumerator PostFBScreenshot() {	
			
			yield return new WaitForEndOfFrame();
			// Create a texture the size of the screen, RGB24 format
			int width = Screen.width;
			int height = Screen.height;
			Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
			// Read screen contents into the texture
			tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
			tex.Apply();
			
			AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", tex,  "facebook.katana");
			
			Destroy(tex);
			
		}
		
		
		public void Connect() {
			if(!IsAuntificated) {
				SPFacebook.Instance.Login();//"email","publish_actions","user_friends");
				SA_StatusBar.text = "Log in...";
			} else {
				LogOut();
				SA_StatusBar.text = "Logged out";
			}
		}
		
		public void LoadUserData() {
			SPFacebook.OnUserDataRequestCompleteAction += OnUserDataLoaded;
			SPFacebook.Instance.LoadUserData();
			SA_StatusBar.text = "Loadin user data..";
		}
		
		public void PostMessage() {
			SPFacebook.Instance.FeedShare (
				link: "https://example.com/myapp/?storyID=thelarch",
				linkName: "The Larch",
				linkCaption: "I thought up a witty tagline about larches",
				linkDescription: "There are a lot of larch trees around here, aren't there?",
				picture: "https://example.com/myapp/assets/1/larch.jpg"
				);
			
			SA_StatusBar.text = "Positng..";
		}
		
		public void PostScreehShot() {
			StartCoroutine(PostScreenshot());
			SA_StatusBar.text = "Positng..";
		}
		
		public void LoadFriends() {
			
			SPFacebook.OnFriendsDataRequestCompleteAction += OnFriendsDataLoaded;
			
			int limit = 5;
			SPFacebook.Instance.LoadFrientdsInfo(limit);
			SA_StatusBar.text = "Loading friends..";
		}
		
		public void AppRequest() {
			SPFacebook.Instance.AppRequest("Come play this great game!");
		}
		
		public void LoadScore() {
			SPFacebook.Instance.LoadPlayerScores();
		}
		
		public void LoadAppScores() {
			SPFacebook.Instance.LoadAppScores();
		}
		
		public void SubmitScore() {
			startScore++;
			SPFacebook.Instance.SubmitScore(startScore);
		}
		
		
		public void DeletePlayerScores() {
			SPFacebook.Instance.DeletePlayerScores();
		}
		
		public void LikePage() {
			Application.OpenURL("https://www.facebook.com/unionassets");
		}
		
		
		private string UNION_ASSETS_PAGE_ID = "1435528379999137";
		
		
		public void CheckLike() {
			
			//checking if current user likes the page
			Debug.Log("[CheckLike]");
			
			bool IsLikes = SPFacebook.Instance.IsUserLikesPage(SPFacebook.Instance.UserId, UNION_ASSETS_PAGE_ID);
			if(IsLikes) {
				SA_StatusBar.text ="Current user Likes union assets";
			} else {
				//user do not like the page or we han't yet downloaded likes data
				//downloading likes for this page
				SPFacebook.OnLikesListLoadedAction += OnLikesLoaded;
				SPFacebook.Instance.LoadLikes(SPFacebook.Instance.UserId, UNION_ASSETS_PAGE_ID);
				
			}
			
		}

		public void ActivateApp() {
			
			//An app is being activated, typically in the AppDelegate's applicationDidBecomeActive.
			SPFacebookAnalytics.ActivateApp ();
		}
		
		
		public void AchievedLevel() {
			//The user has achieved a level in the app.
			SPFacebookAnalytics.AchievedLevel (1);
		}
		
		
		public void AddedPaymentInfo() {
			//The user has entered their payment info.
			SPFacebookAnalytics.AddedPaymentInfo (true);
		}
		
		
		public void AddedToCart() {
			//The user has added an item to their cart. 
			SPFacebookAnalytics.AddedToCart(54.23f, "HDFU-8452", "shoes", "USD");
		}
		
		
		public void AddedToWishlist() {
			//The user has added an item to their wishlist. 
			SPFacebookAnalytics.AddedToWishlist(54.23f, "HDFU-8452", "shoes", "USD");
		}
		
		public void CompletedRegistration() {
			//A user has completed registration with the app.
			//Facebook, Email, Twitter, etc.
			
			SPFacebookAnalytics.CompletedRegistration("Email");
		}
		
		public void InitiatedCheckout() {
			//The user has entered the checkout process. The valueToSum passed to logEvent should be the total price in the cart.
			SPFacebookAnalytics.InitiatedCheckout(54.23f, 3, "HDFU-8452", "shoes", true, "USD");
		}
		
		
		public void Purchased() {
			//The user has completed a purchase
			SPFacebookAnalytics.Purchased (54.23f, 3, "shoes", "HDFU-8452", "USD");
		}
		
		public void Rated() {
			//The user has rated an item in the app.
			SPFacebookAnalytics.Rated (4, "HDFU-8452", "shoes", 5);
		}
		
		
		public void Searched() {
			//A user has performed a search within the app.
			SPFacebookAnalytics.Searched ("shoes", "HD", true);
		}
		
		public void SpentCredits() {
			//The user has spent app credits
			SPFacebookAnalytics.SpentCredits (120f, "shoes", "HDFU-8452");
		}
		
		public void UnlockedAchievement() {
			//The user has unlocked an achievement in the app.
			SPFacebookAnalytics.UnlockedAchievement ("ShoeMan");
		}
		
		
		public void ViewedContent() {
			//A user has viewed a form of content in the app.
			SPFacebookAnalytics.ViewedContent (54.23f, "shoes", "HDFU-8452", "USD");
		}
		
		
		// --------------------------------------
		// EVENTS
		// --------------------------------------
		
		private void OnLikesLoaded(FB_Result result) {
			Debug.Log("[OnLikesLoaded] result " + result.RawData);
			//The likes is loaded so now we can find out for sure if user is like our page
			bool IsLikes = SPFacebook.Instance.IsUserLikesPage(SPFacebook.Instance.UserId, UNION_ASSETS_PAGE_ID);
			if(IsLikes) {
				SA_StatusBar.text ="Current user Likes union assets";
			} else {
				SA_StatusBar.text ="Current user does not like union assets";
			}
		}
		
		
		private void OnFocusChanged(bool focus) {
			
			Debug.Log("FB OnFocusChanged: " + focus);
			
			if (!focus)  {                                                                                        
				// pause the game - we will need to hide                                             
				Time.timeScale = 0;                                                                  
			} else  {                                                                                        
				// start the game back up - we're getting focus again                                
				Time.timeScale = 1;                                                                  
			}   
		}
		
		
		
		private void OnUserDataLoaded(FB_Result result) {
			
			SPFacebook.OnUserDataRequestCompleteAction -= OnUserDataLoaded;
			
			if (result.IsSucceeded)  { 
				SA_StatusBar.text = "User data loaded";
				IsUserInfoLoaded = true;
				
				//user data available, we can get info using
				//SPFacebook.instance.userInfo getter
				//and we can also use userInfo methods, for exmple download user avatar image
				SPFacebook.Instance.userInfo.LoadProfileImage(FB_ProfileImageSize.square);
				
				
			} else {
				SA_StatusBar.text ="Opps, user data load failed, something was wrong";
				Debug.Log("Opps, user data load failed, something was wrong");
			}
			
		}
		
		
		private void OnFriendsDataLoaded(FB_Result res) {
			SPFacebook.OnFriendsDataRequestCompleteAction -= OnFriendsDataLoaded;
			
			if(res.Error == null) {
				//friednds data available, we can get it using
				//SPFacebook.instance.friendsList getter
				//and we can also use FacebookUserInfo methods, for exmple download user avatar image
				
				foreach(FB_UserInfo friend in SPFacebook.Instance.friendsList) {
					friend.LoadProfileImage(FB_ProfileImageSize.square);
				}
				
				IsFrindsInfoLoaded = true;
			} else {
				Debug.Log("Opps, friends data load failed, something was wrong");
			}
		}
		
		
		
		
		private void OnInit() {
			if(SPFacebook.Instance.IsLoggedIn) {
				IsAuntificated = true;
			} else {
				SA_StatusBar.text = "user Login -> fale";
			}
		}
		
		
		private void OnAuth(FB_Result result) {
			if(SPFacebook.Instance.IsLoggedIn) {
				IsAuntificated = true;
				SA_StatusBar.text = "user Login -> true";
			} else {
				Debug.Log("Failed to log in");
			}
			
		}
		
		private void OnPost(FB_PostResult res) {
			
			if(res.IsSucceeded) {
				Debug.Log("Posting complete");
				Debug.Log("Posy id: " + res.PostId);
				SA_StatusBar.text = "Posting complete";
			} else {
				SA_StatusBar.text = "Oops, post failed, something was wrong " + res.Error;
				Debug.Log("Oops, post failed, something was wrong " + res.Error);
			}
		}
		
		
		//scores Api events
		private void OnPlayerScoreRequestComplete(FB_Result result) {
			
			if(result.IsSucceeded) {
				string msg = "Player has scores in " + SPFacebook.Instance.userScores.Count + " apps" + "\n";
				msg += "Current Player Score = " + SPFacebook.Instance.GetCurrentPlayerIntScoreByAppId(SPFacebook.Instance.AppId);
				SA_StatusBar.text = msg;
				
			} else {
				SA_StatusBar.text = result.RawData;
			}
			
			
		}
		
		private void OnAppScoreRequestComplete(FB_Result result) {
			
			if(result.IsSucceeded) {
				string msg = "Loaded " + SPFacebook.Instance.appScores.Count + " scores results" + "\n";
				msg += "Current Player Score = " + SPFacebook.Instance.GetScoreByUserId(SPFacebook.Instance.UserId);
				SA_StatusBar.text = msg;
				
			} else {
				SA_StatusBar.text = result.RawData;
			}
			
		}
		
		private void OnSubmitScoreRequestComplete(FB_Result result) {
			
			
			if(result.IsSucceeded) {
				string msg = "Score successfully submited" + "\n";
				msg += "Current Player Score = " + SPFacebook.Instance.GetScoreByUserId(SPFacebook.Instance.UserId);
				SA_StatusBar.text = msg;
				
			} else {
				SA_StatusBar.text = result.RawData;
			}	
		}
		
		private void OnDeleteScoreRequestComplete(FB_Result result) {
			if(result.IsSucceeded) {
				string msg = "Score successfully deleted" + "\n";
				msg += "Current Player Score = " + SPFacebook.Instance.GetScoreByUserId(SPFacebook.Instance.UserId);
				SA_StatusBar.text = msg;
				
			} else {
				SA_StatusBar.text = result.RawData;
			}	
		}
		
		
		// --------------------------------------
		// PRIVATE METHODS
		// --------------------------------------
		
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
			
			SPFacebook.Instance.PostImage("My app ScreehShot", tex);
			
			Destroy(tex);
			
		}
		
		private void LogOut() {
			IsUserInfoLoaded = false;
			
			IsAuntificated = false;
			
			SPFacebook.Instance.Logout();
		}

	}
}
