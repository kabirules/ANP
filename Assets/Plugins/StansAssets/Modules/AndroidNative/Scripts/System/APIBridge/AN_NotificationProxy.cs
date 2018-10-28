using UnityEngine;
using System.Collections;

public class AN_NotificationProxy {

	private const string CLASS_NAME = "com.androidnative.features.notifications.LocalNotificationManager";
	
	private static void CallActivityFunction(string methodName, params object[] args) {
		AN_ProxyPool.CallStatic(CLASS_NAME, methodName, args);
	}

	// --------------------------------------
	// Toast Notification
	// --------------------------------------

	public static void ShowToastNotification(string text, int duration) {
		CallActivityFunction("ShowToastNotification", text, duration.ToString());
	}

	// --------------------------------------
	// Local Notifications
	// --------------------------------------

	public static void HideAllNotifications() {
		CallActivityFunction ("HideAllNotifications");
	}
	
	public static void requestCurrentAppLaunchNotificationId() { 
		CallActivityFunction("requestCurrentAppLaunchNotificationId");
	}

	public static void ScheduleLocalNotification(AndroidNotificationBuilder builder) {
		CallActivityFunction("ScheduleLocalNotification",
		                     builder.Title,
		                     builder.Message,
		                     builder.Time.ToString(),
		                     builder.Id.ToString(),
		                     builder.Icon,
		                     builder.Sound,
		                     builder.Vibration.ToString(),
		                     builder.ShowIfAppForeground.ToString(),
							 builder.Repeating,
							 builder.RepeatDelay,
		                     builder.LargeIcon,
		                     builder.BigPicture == null ? string.Empty : System.Convert.ToBase64String(builder.BigPicture.EncodeToPNG()),
							 builder.Color == null ? string.Empty : string.Format("{0}|{1}|{2}|{3}", 255 * builder.Color.Value.a,
		                                                     										 255 * builder.Color.Value.r,
		                                                     										 255 * builder.Color.Value.g,
		                                                     										 255 * builder.Color.Value.b),
		                     builder.WakeLockTime);
	}
	
	public static void CanselLocalNotification(int id) {
		CallActivityFunction("cancelLocalNotification", id.ToString());
	}
}
