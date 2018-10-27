////////////////////////////////////////////////////////////////////////////////
//  
// @module Android Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////


 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotificationsExample : MonoBehaviour {

	public Texture2D bigPicture;

	private int LastNotificationId = 0;

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	void Awake() {
		GoogleCloudMessageService.ActionCMDRegistrationResult += HandleActionCMDRegistrationResult;
		GoogleCloudMessageService.ActionCouldMessageLoaded += OnMessageLoaded;
		GoogleCloudMessageService.ActionGCMPushLaunched += HandleActionGCMPushLaunched;
		GoogleCloudMessageService.ActionGCMPushReceived += HandleActionGCMPushReceived;
		GoogleCloudMessageService.Instance.Init();
	}

	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------

	private void Toast() {
		AndroidToast.ShowToastNotification ("Hello Toast", AndroidToast.LENGTH_LONG);
	}

	private void Local() {
		AndroidNotificationBuilder builder = new AndroidNotificationBuilder(SA.Common.Util.IdFactory.NextId,
		                                                                    "Local Notification Title",
																			"This is local notification",
		                                                                    10);
		builder.SetBigPicture (bigPicture);
		AndroidNotificationManager.Instance.ScheduleLocalNotification(builder);
	}

	private void LoadLaunchNotification (){
		AndroidNotificationManager.Instance.OnNotificationIdLoaded += OnNotificationIdLoaded;
		AndroidNotificationManager.Instance.LocadAppLaunchNotificationId();
	}

	private void CanselLocal() {
		AndroidNotificationManager.Instance.CancelLocalNotification(LastNotificationId);
	}

	private void CancelAll() {
		AndroidNotificationManager.Instance.CancelAllLocalNotifications();
	}


	private void Reg() {
		GoogleCloudMessageService.Instance.RgisterDevice();
	}

	private void LoadLastMessage() {
		GoogleCloudMessageService.Instance.LoadLastMessage();
	}
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------

	private void HandleActionGCMPushReceived (string message, Dictionary<string, object> data)
	{
		Debug.Log("[HandleActionGCMPushReceived]");
		Debug.Log("Message: " + message);
		foreach (KeyValuePair<string, object> pair in data) {
			Debug.Log("Data Entity: " + pair.Key + " " + pair.Value.ToString());
		}

		AN_PoupsProxy.showMessage (message, ANMiniJSON.Json.Serialize(data));
	}

	private void HandleActionGCMPushLaunched (string message, Dictionary<string, object> data)
	{
		Debug.Log("[HandleActionGCMPushLaunched]");
		Debug.Log("Message: " + message);
		foreach (KeyValuePair<string, object> pair in data) {
			Debug.Log("Data Entity: " + pair.Key + " " + pair.Value.ToString());
		}

		AN_PoupsProxy.showMessage (message, ANMiniJSON.Json.Serialize(data));
	}

	private void HandleActionCMDRegistrationResult (GP_GCM_RegistrationResult res) {
		if(res.IsSucceeded) {
			AN_PoupsProxy.showMessage ("Regstred", "GCM REG ID: " + GoogleCloudMessageService.Instance.registrationId);
		} else {
			AN_PoupsProxy.showMessage ("Reg Failed", "GCM Registration failed :(");
		}
	}
		
	private void OnNotificationIdLoaded (int notificationid){
		AN_PoupsProxy.showMessage ("Loaded", "App was laucnhed with notification id: " + notificationid);
	}
	
	private void OnMessageLoaded(string msg) {
		AN_PoupsProxy.showMessage ("Message Loaded", "Last GCM Message: " + GoogleCloudMessageService.Instance.lastMessage);
	}
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------

}
