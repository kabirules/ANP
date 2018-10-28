using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SA.Common.Models.UI.Core;

namespace SA.AN.GOOGLE_PLAY_FRIENDS {
	
	public class FriendsLoadingTab : FeatureTab {

		public Image avatar;
		private Sprite logo;
		
		public Text playerLabel;
		public Button connectButton;
		public Text connectButtonLabel;
		
		private Sprite defaulttexture;
		public Button[] ConnectionDependedntButtons;
		public FriendInfoPresenter[] rows;	
		
		void Awake() {		
			playerLabel.text = "Player Disconnected";
			defaulttexture = avatar.sprite;		
			
			//listen for GooglePlayConnection events
			
			GooglePlayConnection.ActionPlayerConnected +=  OnPlayerConnected;
			GooglePlayConnection.ActionPlayerDisconnected += OnPlayerDisconnected;
			
			GooglePlayConnection.ActionConnectionResultReceived += OnConnectionResult;			
			GooglePlayManager.ActionFriendsListLoaded +=  OnFriendListLoaded;	
			
			if(GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED) {
				//checking if player already connected
				OnPlayerConnected ();
			}		
		}
		
		public void ConncetButtonPress() {
			Debug.Log("GooglePlayManager State  -> " + GooglePlayConnection.State.ToString());
			if(GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED) {
				SA_StatusBar.text = "Disconnecting from Play Service...";
				GooglePlayConnection.Instance.Disconnect ();
			} else {
				SA_StatusBar.text = "Connecting to Play Service...";
				GooglePlayConnection.Instance.Connect ();
			}
		}
		
		void Update() {		
			if(GooglePlayConnection.State != GPConnectionState.STATE_CONNECTED) {
				return;
			} 
			
			int i = 0;
			foreach(string fId in GooglePlayManager.Instance.friendsList) {
				GooglePlayerTemplate p = GooglePlayManager.Instance.GetPlayerById(fId);
				if(p != null) {
					rows[i].SetInfo(p.playerId, p.name, p.hasIconImage && p.icon != null, p.hasHiResImage && p.image != null, p.icon);
				}
				
				i++;
				if(i > 7) {
					return;
				}
			}		
		}	
		
		void FixedUpdate() {
			if(GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED) {
				if(GooglePlayManager.Instance.player.icon != null) {
					Texture2D icon = GooglePlayManager.Instance.player.icon;
					if (logo == null) {
						logo = Sprite.Create(icon, new Rect (0.0f, 0.0f, icon.width, icon.height), new Vector2(0.5f, 0.5f));
					}
					avatar.sprite = logo;
				}
			}  else {
				avatar.sprite = defaulttexture;
			}
			
			
			string title = "Connect";
			if(GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED) {
				title = "Disconnect";
				
				foreach(Button btn in ConnectionDependedntButtons) {
					btn.interactable = true;
				}
				
				
			} else {
				foreach(Button btn in ConnectionDependedntButtons) {
					btn.interactable = false;
					
				}
				if(GooglePlayConnection.State == GPConnectionState.STATE_DISCONNECTED || GooglePlayConnection.State == GPConnectionState.STATE_UNCONFIGURED) {
					
					title = "Connect";
				} else {
					title = "Connecting..";
				}
			}
			
			connectButtonLabel.text = title;
		}
		
		
		
		public void LoadFriendsList() {
			GooglePlayManager.Instance.LoadFriends();
		}

		private void OnFriendListLoaded(GooglePlayResult result) {
			GooglePlayManager.ActionFriendsListLoaded -=  OnFriendListLoaded;
			SA_StatusBar.text = "Load Friends Result:  " + result.Response.ToString();
		}
			
		private void OnPlayerDisconnected() {
			SA_StatusBar.text = "Player Disconnected";
			playerLabel.text = "Player Disconnected";
		}
		
		private void OnPlayerConnected() {
			SA_StatusBar.text = "Player Connected";
			playerLabel.text = GooglePlayManager.Instance.player.name;
		}
		
		private void OnConnectionResult(GooglePlayConnectionResult result) {
			SA_StatusBar.text = "ConnectionResul:  " + result.code.ToString();
			Debug.Log(result.code.ToString());
		}
	}
}