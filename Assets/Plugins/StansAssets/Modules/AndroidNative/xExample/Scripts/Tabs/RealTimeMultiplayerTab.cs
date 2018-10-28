using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SA.Common.Models.UI.Core;

namespace SA.AN.GOOGLE_RTM {
	
	public class RealTimeMultiplayerTab : FeatureTab {

		public Image avatar;
		private Sprite defaulttexture;
		private Sprite logo;

		public GameObject hi;
		public Text playerLabel;
		public Text gameState;
		public Text parisipants;
		
		public Button connectButton;
		public Text connectButtonLabel;

		public Button helloButton;
		public Button leaveRoomButton;
		public Button showRoomButton;

		public Button[] ConnectionDependedntButtons;
		public ParticipantPresenter[] patricipants;
		public FriendPresenter[] friends;
		
		
		void Start() {
			
			playerLabel.text = "Player Disconnected";
			defaulttexture = avatar.sprite;
			
			//listen for GooglePlayConnection events
			GooglePlayInvitationManager.ActionInvitationReceived += OnInvite;
			GooglePlayInvitationManager.ActionInvitationAccepted += ActionInvitationAccepted;
			GooglePlayRTM.ActionRoomCreated += OnRoomCreated;
			GooglePlayRTM.ActionWatingRoomIntentClosed += WaitingUIClosed;
			
			GooglePlayConnection.ActionPlayerConnected +=  OnPlayerConnected;
			GooglePlayConnection.ActionPlayerDisconnected += OnPlayerDisconnected;
			
			GooglePlayConnection.ActionConnectionResultReceived += OnConnectionResult;
			
			
			if(GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED) {
				//checking if player already connected
				OnPlayerConnected ();
			} 
			
			//networking event
			GooglePlayRTM.ActionDataRecieved += OnGCDataReceived;
			
			
		}
		
		private void WaitingUIClosed(AndroidActivityResult result) {
			Debug.Log ("[WaitingUIClosed] result "  + result.code + " status " + result.IsSucceeded.ToString());
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
		
		
		public void ShowWatingRoom() {
			GooglePlayRTM.Instance.ShowWaitingRoomIntent();
		}
		
		
		/*
		private static int ROLE_FARMER = 0x1; // 001 in binary
		private static int ROLE_ARCHER = 0x2; // 010 in binary
		private static int ROLE_WIZARD = 0x4; // 100 in binary


		private static int TRACK_1 = 1; // 100 in binary
		private static int TRACK_2 = 2; // 100 in binary
	*/
		
		public void findMatch() {
			/*
			GooglePlayRTM.instance.SetExclusiveBitMask (ROLE_WIZARD);
			GooglePlayRTM.instance.SetVariant (TRACK_1);
	*/
			int minPlayers = 1;
			int maxPlayers = 2;
			
			GooglePlayRTM.Instance.FindMatch(minPlayers, maxPlayers);
		}
		
		public void InviteFriends() {
			int minPlayers = 1;
			int maxPlayers = 2;
			GooglePlayRTM.Instance.OpenInvitationBoxUI(minPlayers, maxPlayers);
		}
		
		
		public void SendHello() {
			#if (UNITY_ANDROID && !UNITY_EDITOR) || SA_DEBUG_MODE
			string msg = "hello world";
			System.Text.UTF8Encoding  encoding = new System.Text.UTF8Encoding();
			byte[] data = encoding.GetBytes(msg);
			
			GooglePlayRTM.Instance.SendDataToAll(data, GP_RTM_PackageType.RELIABLE);
			#endif
			
		}
		
		public void LeaveRoomInstance() {
			GooglePlayRTM.Instance.LeaveRoom();
		}
		
		
		
		private void DrawParticipants() {
			
			UpdateGameState("Room State: " + GooglePlayRTM.Instance.currentRoom.status.ToString());
			parisipants.text = "Total Room Participants: " + GooglePlayRTM.Instance.currentRoom.participants.Count;
			
			
			
			foreach(ParticipantPresenter p in patricipants) {
				p.gameObject.SetActive(false);
			}
			
			int i = 0;
			foreach(GP_Participant p in GooglePlayRTM.Instance.currentRoom.participants) {
				patricipants[i].gameObject.SetActive(true);
				patricipants[i].SetParticipant(p);
				i++;
			}
			
			
		}
		
		private void UpdateGameState(string msg) {
			gameState.text = msg;
		}
		
		void FixedUpdate() {
			DrawParticipants();
			
			if(GooglePlayRTM.Instance.currentRoom.status!= GP_RTM_RoomStatus.ROOM_VARIANT_DEFAULT && GooglePlayRTM.Instance.currentRoom.status!= GP_RTM_RoomStatus.ROOM_STATUS_ACTIVE) {
				showRoomButton.interactable = true;
			} else {
				showRoomButton.interactable = false;
			}
			
			
			
			if(GooglePlayRTM.Instance.currentRoom.status == GP_RTM_RoomStatus.ROOM_VARIANT_DEFAULT) {
				leaveRoomButton.interactable = false;
			} else {
				leaveRoomButton.interactable = true;
			}
			
			if(GooglePlayRTM.Instance.currentRoom.status == GP_RTM_RoomStatus.ROOM_STATUS_ACTIVE) {
				helloButton.interactable = true;
				hi.SetActive(true);
			} else {
				helloButton.interactable = false;
				hi.SetActive(false);
			}
			
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
		
		private void OnPlayerDisconnected() {
			SA_StatusBar.text = "Player Disconnected";
			playerLabel.text = "Player Disconnected";
		}
		
		private void OnPlayerConnected() {
			SA_StatusBar.text = "Player Connected";
			playerLabel.text = GooglePlayManager.Instance.player.name;
			
			GooglePlayInvitationManager.Instance.RegisterInvitationListener();
			
			
			GooglePlayManager.ActionFriendsListLoaded +=  OnFriendListLoaded;
			GooglePlayManager.Instance.LoadFriends();
		}
		
		void OnFriendListLoaded (GooglePlayResult result) {
			Debug.Log("OnFriendListLoaded: " + result.Message);
			GooglePlayManager.ActionFriendsListLoaded -=  OnFriendListLoaded;
			
			if(result.IsSucceeded) {
				Debug.Log("Friends Load Success");
				
				int i = 0;
				foreach(string fId in GooglePlayManager.Instance.friendsList) {
					if(i < 3) {
						friends[i].SetFriendId(fId);
					}
					i++;
				}
			}
		}
		
		private void OnConnectionResult(GooglePlayConnectionResult result) {
			SA_StatusBar.text = "ConnectionResul:  " + result.code.ToString();
			Debug.Log(result.code.ToString());
		}
		
		private string inviteId;
		private void OnInvite(GP_Invite invitation) {
			
			if (invitation.InvitationType != GP_InvitationType.INVITATION_TYPE_REAL_TIME) {
				return;
			}
			
			inviteId = invitation.Id;
			
			AndroidDialog dialog =  AndroidDialog.Create("Invite", "You have new invite from: " + invitation.Participant.DisplayName, "Manage Manually", "Open Google Inbox");
			dialog.ActionComplete += OnInvDialogComplete;
		}
		
		void ActionInvitationAccepted (GP_Invite invitation) {
			
			Debug.Log("ActionInvitationAccepted called");
			
			if (invitation.InvitationType != GP_InvitationType.INVITATION_TYPE_REAL_TIME) {
				return;
			}
			
			
			Debug.Log("Starting The Game");
			//make sure you have prepared your scene to start the game before you accepting the invite. Room join even will be triggered
			GooglePlayRTM.Instance.AcceptInvitation(invitation.Id);
		}
		
		private void OnRoomCreated(GP_GamesStatusCodes code) {
			SA_StatusBar.text = "Room Create Result:  " + code.ToString();
		}
		
		
		
		private void OnInvDialogComplete(AndroidDialogResult result) {
			
			
			
			//parsing result
			switch(result) {
			case AndroidDialogResult.YES:
				AndroidDialog dialog =  AndroidDialog.Create("Manage Invite", "Would you like to accept this invite?", "Accept", "Decline");
				dialog.ActionComplete += OnInvManageDialogComplete;
				break;
			case AndroidDialogResult.NO:
				GooglePlayRTM.Instance.OpenInvitationInBoxUI();
				break;
				
			}
		}
		
		private void OnInvManageDialogComplete(AndroidDialogResult result) {
			switch(result) {
			case AndroidDialogResult.YES:
				GooglePlayRTM.Instance.AcceptInvitation(inviteId);
				break;
			case AndroidDialogResult.NO:
				GooglePlayRTM.Instance.DeclineInvitation(inviteId);
				break;
			}
		}
		
		
		
		
		
		private void OnGCDataReceived(GP_RTM_Network_Package package) {
			#if (UNITY_ANDROID && !UNITY_EDITOR ) || SA_DEBUG_MODE
			
			
			System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
			string str = enc.GetString(package.buffer);
			
			
			string name = package.participantId;
			
			
			GP_Participant p =  GooglePlayRTM.Instance.currentRoom.GetParticipantById(package.participantId);
			if(p != null) {
				GooglePlayerTemplate player = GooglePlayManager.Instance.GetPlayerById(p.playerId);
				if(player != null) {
					name = player.name;
				}
			}
			
			AndroidMessage.Create("Data Eeceived", "player " + name + " \n " + "data: " + str);
			
			#endif
			
		}

	}
}
