//#define SA_DEBUG_MODE
using UnityEngine;
using System.Collections;

public class RTM_Game_Example : AndroidNativeExampleBase {


	public GameObject avatar;
	public GameObject hi;
	public SA_Label playerLabel;
	public SA_Label gameState;
	public SA_Label parisipants;

	public DefaultPreviewButton connectButton;


	public DefaultPreviewButton helloButton;
	public DefaultPreviewButton leaveRoomButton;
	public DefaultPreviewButton showRoomButton;


	public DefaultPreviewButton[] ConnectionDependedntButtons;
	public SA_PartisipantUI[] patricipants;
	public SA_FriendUI[] friends;


	private Texture defaulttexture;


	void Start() {
		
		playerLabel.text = "Player Disconnected";
		defaulttexture = avatar.GetComponent<Renderer>().material.mainTexture;
		
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

	private void ConncetButtonPress() {
		Debug.Log("GooglePlayManager State  -> " + GooglePlayConnection.State.ToString());
		if(GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED) {
			SA_StatusBar.text = "Disconnecting from Play Service...";
			GooglePlayConnection.Instance.Disconnect ();
		} else {
			SA_StatusBar.text = "Connecting to Play Service...";
			GooglePlayConnection.Instance.Connect ();
		}
	}

	
	private void ShowWatingRoom() {
		GooglePlayRTM.Instance.ShowWaitingRoomIntent();
	}


	/*
	private static int ROLE_FARMER = 0x1; // 001 in binary
	private static int ROLE_ARCHER = 0x2; // 010 in binary
	private static int ROLE_WIZARD = 0x4; // 100 in binary


	private static int TRACK_1 = 1; // 100 in binary
	private static int TRACK_2 = 2; // 100 in binary
*/

	private void findMatch() {
/*
		GooglePlayRTM.instance.SetExclusiveBitMask (ROLE_WIZARD);
		GooglePlayRTM.instance.SetVariant (TRACK_1);
*/
		int minPlayers = 1;
		int maxPlayers = 2;

		GooglePlayRTM.Instance.FindMatch(minPlayers, maxPlayers);
	}

	private void InviteFriends() {
		int minPlayers = 1;
		int maxPlayers = 2;
		GooglePlayRTM.Instance.OpenInvitationBoxUI(minPlayers, maxPlayers);
	}


	private void SendHello() {
		#if (UNITY_ANDROID && !UNITY_EDITOR) || SA_DEBUG_MODE
		string msg = "hello world";
		System.Text.UTF8Encoding  encoding = new System.Text.UTF8Encoding();
		byte[] data = encoding.GetBytes(msg);

		GooglePlayRTM.Instance.SendDataToAll(data, GP_RTM_PackageType.RELIABLE);
		#endif

	}

	private void LeaveRoomInstance() {
		GooglePlayRTM.Instance.LeaveRoom();
	}



	private void DrawParticipants() {

		UpdateGameState("Room State: " + GooglePlayRTM.Instance.currentRoom.status.ToString());
		parisipants.text = "Total Room Participants: " + GooglePlayRTM.Instance.currentRoom.participants.Count;
			


		foreach(SA_PartisipantUI p in patricipants) {
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
			showRoomButton.EnabledButton();
		} else {
			showRoomButton.DisabledButton();
		}



		if(GooglePlayRTM.Instance.currentRoom.status == GP_RTM_RoomStatus.ROOM_VARIANT_DEFAULT) {
			leaveRoomButton.DisabledButton();
		} else {
			leaveRoomButton.EnabledButton();
		}

		if(GooglePlayRTM.Instance.currentRoom.status == GP_RTM_RoomStatus.ROOM_STATUS_ACTIVE) {
			helloButton.EnabledButton();
			hi.SetActive(true);
		} else {
			helloButton.DisabledButton();
			hi.SetActive(false);
		}

		if(GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED) {
			if(GooglePlayManager.Instance.player.icon != null) {
				avatar.GetComponent<Renderer>().material.mainTexture = GooglePlayManager.Instance.player.icon;
			}
		}  else {
			avatar.GetComponent<Renderer>().material.mainTexture = defaulttexture;
		}
		
		
		string title = "Connect";
		if(GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED) {
			title = "Disconnect";

			foreach(DefaultPreviewButton btn in ConnectionDependedntButtons) {
				btn.EnabledButton();
			}
			
			
		} else {
			foreach(DefaultPreviewButton btn in ConnectionDependedntButtons) {
				btn.DisabledButton();
				
			}
			if(GooglePlayConnection.State == GPConnectionState.STATE_DISCONNECTED || GooglePlayConnection.State == GPConnectionState.STATE_UNCONFIGURED) {
				
				title = "Connect";
			} else {
				title = "Connecting..";
			}
		}
		
		connectButton.text = title;
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
