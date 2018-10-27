using UnityEngine;
using System.Collections;

public class QuestAndEventsExample : MonoBehaviour {
	


	public GameObject avatar;
	private Texture defaulttexture;
	public Texture2D pieIcon;
	
	public DefaultPreviewButton connectButton;
	public SA_Label playerLabel;
	
	public DefaultPreviewButton[] ConnectionDependedntButtons;



	//example, replase with your ID
	private const string EVENT_ID = "CgkIipfs2qcGEAIQDQ";

	//example, replase with your ID
	private const string QUEST_ID = "CgkIipfs2qcGEAIQDg";


	


	void Start() {
		
		playerLabel.text = "Player Disconnected";
		defaulttexture = avatar.GetComponent<Renderer>().material.mainTexture;
		
		//listen for GooglePlayConnection events
		GooglePlayConnection.ActionPlayerConnected +=  OnPlayerConnected;
		GooglePlayConnection.ActionPlayerDisconnected += OnPlayerDisconnected;
		
		GooglePlayConnection.ActionConnectionResultReceived += OnConnectionResult;
		
		//listen for events, we will use action in this example
		GooglePlayEvents.Instance.OnEventsLoaded += OnEventsLoaded;

		GooglePlayQuests.Instance.OnQuestsAccepted += OnQuestsAccepted;
		GooglePlayQuests.Instance.OnQuestsCompleted += OnQuestsCompleted;
		GooglePlayQuests.Instance.OnQuestsLoaded += OnQuestsLoaded;
		
		if(GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED) {
			//checking if player already connected
			OnPlayerConnected ();
		} 
		
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
	



	

	
	void FixedUpdate() {
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


	//--------------------------------------
	// PUBLIC METHODS
	//--------------------------------------

	public void LoadEvents() {
		GooglePlayEvents.Instance.LoadEvents();
	}

	public void IncrementEvent() {
		GooglePlayEvents.Instance.SumbitEvent(EVENT_ID);
	}
	


	public void ShowAllQuests() {
		GooglePlayQuests.Instance.ShowQuests();
	}

	public void ShowAcceptedQuests() {
		GooglePlayQuests.Instance.ShowQuests(GP_QuestsSelect.SELECT_ACCEPTED);
	}

	public void ShowCompletedQuests() {
		GooglePlayQuests.Instance.ShowQuests(GP_QuestsSelect.SELECT_COMPLETED);
	}

	public void ShowOpenQuests() {
		GooglePlayQuests.Instance.ShowQuests(GP_QuestsSelect.SELECT_OPEN);
	}

	public void AcceptQuest() {
		GooglePlayQuests.Instance.AcceptQuest(QUEST_ID);
	}

	public void LoadQuests() {
		GooglePlayQuests.Instance.LoadQuests(GP_QuestSortOrder.SORT_ORDER_ENDING_SOON_FIRST);
	}





	//--------------------------------------
	// EVENTS
	//--------------------------------------

	private void OnEventsLoaded (GooglePlayResult result) {
		Debug.Log ("Total Events: " + GooglePlayEvents.Instance.Events.Count);
		AN_PoupsProxy.showMessage ("Events Loaded", "Total Events: " + GooglePlayEvents.Instance.Events.Count);
		SA_StatusBar.text = "OnEventsLoaded:  " + result.Response.ToString();

		foreach(GP_Event ev in GooglePlayEvents.Instance.Events) {
			Debug.Log(ev.Id);
			Debug.Log(ev.Description);
			Debug.Log(ev.FormattedValue);
			Debug.Log(ev.Value);
			Debug.Log(ev.IconImageUrl);
			Debug.Log(ev.icon);
		}
	}

	private void OnQuestsAccepted (GP_QuestResult result) {
		AN_PoupsProxy.showMessage ("On Quests Accepted", "Quests Accepted, ID: " + result.GetQuest().Id);

		SA_StatusBar.text = "OnQuestsAccepted:  " + result.Response.ToString();
	}

	private void OnQuestsCompleted (GP_QuestResult result) {
		Debug.Log ("Quests Completed, Reward: " + result.GetQuest().RewardData);

		AN_PoupsProxy.showMessage ("On Quests Completed", "Quests Completed, Reward: " + result.GetQuest().RewardData);

		SA_StatusBar.text = "OnQuestsCompleted:  " + result.Response.ToString();
	}

	private void OnQuestsLoaded (GooglePlayResult result) {
		Debug.Log ("Total Quests: " + GooglePlayQuests.Instance.GetQuests().Count);
		AN_PoupsProxy.showMessage ("Quests Loaded", "Total Quests: " + GooglePlayQuests.Instance.GetQuests().Count);

		SA_StatusBar.text = "OnQuestsLoaded:  " + result.Response.ToString();

		foreach(GP_Quest quest in GooglePlayQuests.Instance.GetQuests()) {
			Debug.Log(quest.Id);
		}
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
	

	
	void OnDestroy() {
		GooglePlayConnection.ActionPlayerConnected -=  OnPlayerConnected;
		GooglePlayConnection.ActionPlayerDisconnected -= OnPlayerDisconnected;
		
		GooglePlayConnection.ActionConnectionResultReceived -= OnConnectionResult;
		
	}

}
