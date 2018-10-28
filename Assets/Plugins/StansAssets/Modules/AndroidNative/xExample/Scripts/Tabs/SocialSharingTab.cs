using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SA.Common.Models.UI.Core;

namespace SA.AN.SOCIAL_API {
	
	public class SocialSharingTab : FeatureTab {

		public Texture2D shareTexture;
		
		void Awake() {

		}
		
		public void ShareText() {
			AndroidSocialGate.OnShareIntentCallback += HandleOnShareIntentCallback;
			AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share https://d45nf.app.goo.gl/QcRv");
			//AndroidSocialGate.StartVideoPickerAndShareIntent("message for video", "share the video");
		}

		public void RequestDynamicLink() {
			SA.AndroidNative.DynamicLinks.Manager.Instance.OnShortLinkReceived += (result) => {
				if (result.IsSucceeded) {
					Debug.Log ("[Short Link] " + result.ShortLink);
				}
			};

			SA.AndroidNative.DynamicLinks.Link.Builder builder = new SA.AndroidNative.DynamicLinks.Link.Builder ();
			builder.SetLink ("https://game_promo")
				.SetDynamicLinkDomain ("d45nf.app.goo.gl")
				.SetAndroidParameters (new SA.AndroidNative.DynamicLinks.Link.AndroidParameters.Builder ("com.unionassets.android.plugin.preview")
					.SetMinimumVersion (1)
					.Build ())
				.SetIosParameters (new SA.AndroidNative.DynamicLinks.Link.IosParameters.Builder ("com.example.ios")
					.SetAppStoreId ("123456789")
					.SetMinimumVersion ("1.0.1")
					.Build ())
				.SetGoogleAnalyticsParameters (new SA.AndroidNative.DynamicLinks.Link.GoogleAnalyticsParameters.Builder ()
					.SetSource ("preview")
					.SetMedium ("social")
					.SetCampaign ("example-promo")
					.Build ())
				.SetItunesConnectAnalyticsParameters (new SA.AndroidNative.DynamicLinks.Link.ItunesConnectAnalyticsParameters.Builder ()
					.SetProviderToken ("123456")
					.SetCampaignToken ("example-promo")
					.Build ())
				.SetSocialMetaTagParameters (new SA.AndroidNative.DynamicLinks.Link.SocialMetaTagParameters.Builder ()
					.SetTitle ("Example of a Dynamic Link")
					.SetDescription ("This link works whether the app is installed or not!")
					.Build ());

			SA.AndroidNative.DynamicLinks.Manager.Instance.RequestShortDynamicLink (builder.Build());
		}
		
		void HandleOnShareIntentCallback (bool status, string package)
		{
			AndroidSocialGate.OnShareIntentCallback -= HandleOnShareIntentCallback;
			Debug.Log("[HandleOnShareIntentCallback] " + status.ToString() + " " + package);
		}
		
		public void ShareScreehshot() {
			StartCoroutine(PostScreenshot());
		}
		
		public void ShareImage() {
			AndroidSocialGate.StartShareIntent("Hello Share Intent", "Sharing Hello wolrd image", shareTexture);
		}	
		
		public void TwitterShare() {
			AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", shareTexture, "twi");
		}
		
		public void ShareMail() {
			AndroidSocialGate.SendMail("Hello Share Intent", "This is my text to share <br> <strong> html text </strong> ", "My E-mail Subject", "mail1@gmail.com, mail2@gmail.com", shareTexture);
		}	
		
		public void InstaShare() {
			AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", shareTexture, "insta");
		}
		
		public void GoogleShare() {
			AndroidSocialGate.StartGooglePlusShare("This is my text to share", shareTexture);
		}	
		
		public void ShareFB() {
			StartCoroutine(PostFBScreenshot());		
		}
		
		public void ShareWhatsapp() {
			StartCoroutine (PostWhatsappScreenshot());
		}
		
		private IEnumerator PostScreenshot() {		
			yield return new WaitForEndOfFrame();
			// Create a texture the size of the screen, RGB24 format
			int width = Screen.width;
			int height = Screen.height;
			Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
			// Read screen contents into the texture
			tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
			tex.Apply();
			
			AndroidSocialGate.StartShareIntent("Hello Share Intent", "This is my text to share", tex);
			
			Destroy(tex);		
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
			
			AndroidSocialGate.StartShareIntent ("Hello Share Intent", "This is my text to share", tex, "facebook.katana");
			
			Destroy(tex);		
		}
		
		private IEnumerator PostWhatsappScreenshot() {
			yield return new WaitForEndOfFrame();
			// Create a texture the size of the screen, RGB24 format
			int width = Screen.width;
			int height = Screen.height;
			Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
			// Read screen contents into the texture
			tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
			tex.Apply();
			
			AndroidSocialGate.StartShareIntent ("Hello Share Intent", "This is my text to share", tex, "whatsapp");
			
			Destroy(tex);		
		}
	}
}