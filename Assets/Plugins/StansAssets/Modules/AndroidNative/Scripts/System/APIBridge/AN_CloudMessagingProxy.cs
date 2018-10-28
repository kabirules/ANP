using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AN_CloudMessagingProxy {

	private const string CLASS_NAME = "com.androidnative.gcm.CloudMessagingManager";

	private static void CallActivityFunction(string methodName, params object[] args) {
		AN_ProxyPool.CallStatic(CLASS_NAME, methodName, args);
	}

	// --------------------------------------
	// Google Cloud Message
	// --------------------------------------

	public static void InitPushNotifications(string smallIcon, string largeIcon, string sound, bool vibration, bool showWhenAppForeground, bool replaceOldNotificationWithNew, string color) {
		CallActivityFunction ("InitPushNotifications", smallIcon, largeIcon, sound, vibration.ToString(), showWhenAppForeground.ToString(), replaceOldNotificationWithNew.ToString(), color);
	}

	public static void GCMRgisterDevice(string senderId) {
		CallActivityFunction("GCMRgisterDevice", senderId);
	}

	public static void GCMLoadLastMessage() {
		CallActivityFunction("GCMLoadLastMessage");
	}

	public static void GCMRemoveLastMessageInfo() {
		CallActivityFunction("GCMRemoveLastMessageInfo");
	}

	public static void HideAllNotifications() {
		CallActivityFunction ("HideAllNotifications");
	}
}
