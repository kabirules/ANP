using UnityEngine;
using System.Collections;

public class AN_SocialSharingProxy {

	private const string CLASS_NAME = "com.androidnative.features.social.common.SocialGate";
	
	private static void CallActivityFunction(string methodName, params object[] args) {
		AN_ProxyPool.CallStatic(CLASS_NAME, methodName, args);
	}

	// --------------------------------------
	// Social
	// --------------------------------------

	public static void GetLaunchDeepLinkId(){
		CallActivityFunction("GetLaunchDeepLinkId");
	}

	public static void GooglePlusShare(string message, string[] images) {
		CallActivityFunction("GooglePlusShare", message, images);
	}

    public static void ShareTwitterGif(string path, string message) {
        CallActivityFunction("ShareTwitterGif", path, message);
    }

	public static void StartGooglePlusShareIntent(string text, string media) {
		CallActivityFunction("StartGooglePlusShare", text, media);
	}

	public static void StartVideoShareIntent(string videoFilePath, string message, string subject, string caption) {
		CallActivityFunction("StartVideoShareIntent", videoFilePath, message, subject, caption);
	}
	
	public static void StartShareIntent(string caption, string message,  string subject, string filters) {
		CallActivityFunction("StartShareIntent", caption, message, subject, filters);
	}

	public static void StartShareCollectionIntent(string caption, string message, string subject, string collection, string filters, int format, bool saveImageToGallery = false) {
		CallActivityFunction("StartShareIntentMediaCollection", caption, message, subject, collection, filters, saveImageToGallery, format);
	}
	
	public static void StartShareIntent(string caption, string message, string subject, string media, string filters, int format, bool saveImageToGallery = false) {
		CallActivityFunction("StartShareIntentMedia", caption, message, subject, media, filters, saveImageToGallery, format);
	}

	public static void SendMailWithImages(string caption, string message, string subject, string email, string images, int format, bool saveImageToGallery = false) {
		CallActivityFunction("SendMailWithImages", caption, message, subject, email, images, saveImageToGallery, format);
	}

	public static void SendMailWithImage(string caption, string message,  string subject, string email, string media, int format, bool saveImageToGallery = false) {
		CallActivityFunction("SendMailWithImage", caption, message, subject, email, media, saveImageToGallery, format);
	}
	
	public static void SendMail(string caption, string message,  string subject, string email) {
		CallActivityFunction("SendMail", caption, message, subject, email);
	}

    public static void SendMailBcc(string caption, string message, string subject, string email)
    {
        CallActivityFunction("SendMailBcc", caption, message, subject, email);
    }

	public static void InstagramPostImage(string data, string cpation) {
		CallActivityFunction("InstagramPostImage", data, cpation);
	}

	public static void SendTextMessage(string message, string recepients){
		CallActivityFunction ("SendTextMessage", message, recepients);
	}




}
