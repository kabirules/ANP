using UnityEngine;
using System.Collections.Generic;
using SA.Common.Models.UI.Core;

namespace SA.AN.NOTIFICATIONS {
	
	public sealed class NotificationsTab : FeatureTab {

		[SerializeField]
		private Texture2D bigPicture;
		
		private int LastNotificationId = 0;

		void Awake() {
			GoogleCloudMessageService.ActionCMDRegistrationResult += HandleActionCMDRegistrationResult;
			GoogleCloudMessageService.ActionCouldMessageLoaded += OnMessageLoaded;
			GoogleCloudMessageService.ActionGCMPushLaunched += HandleActionGCMPushLaunched;
			GoogleCloudMessageService.ActionGCMPushReceived += HandleActionGCMPushReceived;
			GoogleCloudMessageService.Instance.Init();
		}

		//--------------------------------------
		//  PUBLIC METHODS
		//--------------------------------------
		
		public void OnShowToastButton() {
			AndroidToast.ShowToastNotification ("Hello Toast", AndroidToast.LENGTH_LONG);
		}
		
		public void OnScheduleLocal() {
			AndroidNotificationBuilder builder = new AndroidNotificationBuilder(SA.Common.Util.IdFactory.NextId,
			                                                                    "Local Notification Title",
			                                                                    "This is local notification",
			                                                                    10);
			builder.SetBigPicture (bigPicture);
			AndroidNotificationManager.Instance.ScheduleLocalNotification(builder);
		}
		
		public void OnCancelLastLocal() {
			AndroidNotificationManager.Instance.CancelLocalNotification(LastNotificationId);
		}
		
		public void OnCancelAll() {
			AndroidNotificationManager.Instance.CancelAllLocalNotifications();
		}	
		
		public void OnRegisterDevice() {
			GoogleCloudMessageService.Instance.RgisterDevice();
		}
		
		public void OnLoadLastGcmMessage() {
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
}
