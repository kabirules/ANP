using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class AndroidSocialGate : MonoBehaviour {

	private static AndroidSocialGate _Instance = null;

	public static event Action<bool, string> OnShareIntentCallback = delegate {};

	public static void StartGooglePlusShare(string text, Texture2D texture = null) {
		CheckAndCreateInstance();
		AN_SocialSharingProxy.StartGooglePlusShareIntent(text, texture == null ? string.Empty : System.Convert.ToBase64String(texture.EncodeToPNG()));
	}

	public static void StartVideoPickerAndShareIntent(string message, string caption) {
		s_message = message;
		s_caption = caption;
		AndroidCamera.Instance.OnVideoPicked += OnVideoPickedHandler;
		AndroidCamera.Instance.GetVideoFromGallery();
	}

	private static string s_message = string.Empty;
	private static string s_caption = string.Empty;
	private static void OnVideoPickedHandler(AndroidVideoPickResult result) {
		AndroidCamera.Instance.OnVideoPicked -= OnVideoPickedHandler;
		if (result.IsSucceeded) {
			StartVideoShareIntent(result.VideoPath, s_message, s_caption);
		} else {
			Debug.Log("Failed to choose video file.Result code: " + result.code.ToString());
		}
	}

	public static void StartVideoShareIntent(string videoFilePath, string message, string caption) {
		CheckAndCreateInstance();
		AN_SocialSharingProxy.StartVideoShareIntent(videoFilePath, message, string.Empty, caption);
	}

	public static void StartShareIntent(string caption, string message, string packageNamePattern = "") {
		CheckAndCreateInstance();
		StartShareIntentWithSubject(caption, message, "", packageNamePattern);
	}

	public static void StartShareIntent(string caption, string message, Texture2D texture,  string packageNamePattern = "") {
		CheckAndCreateInstance();
		StartShareIntentWithSubject(caption, message, "", texture, packageNamePattern);
	}

	public static void StartShareIntent(string caption, string message, Texture2D[] textures, string packageNamePattern = "") {
		CheckAndCreateInstance();
		StartShareIntentWithSubject(caption, message, string.Empty, textures, packageNamePattern = "");
	}

	public static void StartShareIntentWithSubject(string caption, string message, string subject, Texture2D[] textures, string packageNamePattern = "") {
		CheckAndCreateInstance();
		if (textures == null) {
			StartShareIntentWithSubject(caption, message, subject, packageNamePattern);
		} else if (textures.Length == 0) {
			StartShareIntentWithSubject(caption, message, subject, packageNamePattern);
		} else if (textures.Length == 1) {
			StartShareIntent(caption, message, textures[0], packageNamePattern);
		} else {
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < textures.Length - 1; i++) {
				builder.Append(Convert.ToBase64String(textures[i].EncodeToPNG()));
				builder.Append(AndroidNative.DATA_SPLITTER);
			}
			builder.Append(Convert.ToBase64String(textures[textures.Length - 1].EncodeToPNG()));

			AN_SocialSharingProxy.StartShareCollectionIntent(caption,
												message,
												subject,
												builder.ToString(),
												packageNamePattern,
												(int)AndroidNativeSettings.Instance.ImageFormat,
												AndroidNativeSettings.Instance.SaveCameraImageToGallery);
		}
	}

	public static void StartShareIntentWithSubject(string caption, string message, string subject, string packageNamePattern = "") {
		CheckAndCreateInstance();
		AN_SocialSharingProxy.StartShareIntent(caption, message, subject, packageNamePattern);
	}

	public static void StartShareIntentWithSubject(string caption, string message, string subject, Texture2D texture,  string packageNamePattern = "") {
		CheckAndCreateInstance();

		byte[] val = texture.EncodeToPNG();
		string bytesString = System.Convert.ToBase64String (val);

		AN_SocialSharingProxy.StartShareIntent(caption,
												message,
												subject,
												bytesString,
												packageNamePattern,
												(int)AndroidNativeSettings.Instance.ImageFormat,
												AndroidNativeSettings.Instance.SaveCameraImageToGallery);
	}

	public static void SendMail(string caption, string message, string subject, string recipients, Texture2D[] textures) {
		if (textures == null) {
			AN_SocialSharingProxy.SendMail(caption, message, subject, recipients);
		} else if (textures.Length == 0) {
			AN_SocialSharingProxy.SendMail(caption, message, subject, recipients);
		} else if (textures.Length == 1) {
			SendMail(caption, message, subject, recipients, textures[0]);
		} else {
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < textures.Length - 1; i++) {
				builder.Append(Convert.ToBase64String(textures[i].EncodeToPNG()));
				builder.Append(AndroidNative.DATA_SPLITTER);
			}
			builder.Append(Convert.ToBase64String(textures[textures.Length - 1].EncodeToPNG()));

			AN_SocialSharingProxy.SendMailWithImages(caption,
													message,
													subject,
													recipients,
													builder.ToString(),
													(int)AndroidNativeSettings.Instance.ImageFormat,
													AndroidNativeSettings.Instance.SaveCameraImageToGallery);
		}
	}

	public static void SendMail(string caption, string message,  string subject, string recipients, Texture2D texture = null) {
		CheckAndCreateInstance();
		if(texture != null) {
			byte[] val = texture.EncodeToPNG();
			string mdeia = System.Convert.ToBase64String (val);

			AN_SocialSharingProxy.SendMailWithImage(caption,
													message,
													subject,
													recipients,
													mdeia,
													(int)AndroidNativeSettings.Instance.ImageFormat,
													AndroidNativeSettings.Instance.SaveCameraImageToGallery);
		} else {
			AN_SocialSharingProxy.SendMail(caption, message, subject, recipients);
		}
	}

    public static void SendMailBcc(string caption, string message, string subject, string recipients) {
        AN_SocialSharingProxy.SendMailBcc(caption, message, subject, recipients);
    }


    public static void ShareTwitterGif(string gifPath, string message) {
        AN_SocialSharingProxy.ShareTwitterGif(gifPath, message);
    }

	public static void SendTextMessage (string body, string recepient){
		List<string> recepients = new List<string> ();
		recepients.Add (recepient);
		SendTextMessage (body, recepients);
	}

	public static void SendTextMessage (string body, List<string> recipients){
		StringBuilder builder = new StringBuilder ();
		foreach (string recipient in recipients) {
			builder.Append (recipient);
			builder.Append ("|");
		}

		AN_SocialSharingProxy.SendTextMessage (body, builder.ToString());
	}


	private static void CheckAndCreateInstance() {
		if (_Instance == null) {
			_Instance = GameObject.FindObjectOfType(typeof(AndroidSocialGate)) as AndroidSocialGate;
			if (_Instance == null) {
				_Instance = new GameObject ().AddComponent<AndroidSocialGate> ();
				_Instance.gameObject.name = _Instance.GetType ().Name;
			}
		}
	}

	private void ShareCallback(string data) {
		string[] rawData = data.Split(new string[] {"|"}, StringSplitOptions.None);
		bool posted = Int32.Parse(rawData[1]) == -1;
		OnShareIntentCallback(posted, rawData[0]);

		Debug.Log("[AndroidSocialGate]ShareCallback Posted:" + posted + " Package:" + rawData[0]);
	}
}

