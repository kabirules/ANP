using UnityEngine;
using System.Collections;

public class SavedGamesExample : MonoBehaviour {
	


	public GameObject avatar;
	private Texture defaulttexture;
	
	public DefaultPreviewButton connectButton;
	public SA_Label playerLabel;
	
	public DefaultPreviewButton[] ConnectionDependedntButtons;

	

	


	void Start() {
		
		playerLabel.text = "Player Disconnected";
		defaulttexture = avatar.GetComponent<Renderer>().material.mainTexture;
		
		//listen for GooglePlayConnection events
		GooglePlayConnection.ActionPlayerConnected +=  OnPlayerConnected;
		GooglePlayConnection.ActionPlayerDisconnected += OnPlayerDisconnected;
		
		GooglePlayConnection.ActionConnectionResultReceived += OnConnectionResult;
		
		GooglePlaySavedGamesManager.ActionNewGameSaveRequest += ActionNewGameSaveRequest;
		GooglePlaySavedGamesManager.ActionGameSaveLoaded += ActionGameSaveLoaded;
		GooglePlaySavedGamesManager.ActionConflict += ActionConflict;

		
		if(GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED) {
			//checking if player already connected
			OnPlayerConnected ();
		} 
		
	}

	void OnDestroy() {
		GooglePlayConnection.ActionPlayerConnected -=  OnPlayerConnected;
		GooglePlayConnection.ActionPlayerDisconnected -= OnPlayerDisconnected;
		
		GooglePlayConnection.ActionConnectionResultReceived -= OnConnectionResult;


		GooglePlaySavedGamesManager.ActionNewGameSaveRequest -= ActionNewGameSaveRequest;
		GooglePlaySavedGamesManager.ActionGameSaveLoaded -= ActionGameSaveLoaded;
		GooglePlaySavedGamesManager.ActionConflict -= ActionConflict;

		
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


	public void CreateNewSnapshot() {
		StartCoroutine(MakeScreenshotAndSaveGameData());
	}

	private void ShowSavedGamesUI() {
		int maxNumberOfSavedGamesToShow = 5;
		GooglePlaySavedGamesManager.Instance.ShowSavedGamesUI("See My Saves", maxNumberOfSavedGamesToShow);
	}


	public void LoadSavedGames() {
		GooglePlaySavedGamesManager.ActionAvailableGameSavesLoaded += ActionAvailableGameSavesLoaded;
		GooglePlaySavedGamesManager.Instance.LoadAvailableSavedGames();

		SA_StatusBar.text = "Loading saved games.. ";
	}

	private void ActionAvailableGameSavesLoaded (GooglePlayResult res) {

		GooglePlaySavedGamesManager.ActionAvailableGameSavesLoaded -= ActionAvailableGameSavesLoaded;
		if(res.IsSucceeded) {
			foreach(GP_SnapshotMeta meta in GooglePlaySavedGamesManager.Instance.AvailableGameSaves) {
				Debug.Log("Meta.Title: " 					+ meta.Title);
				Debug.Log("Meta.Description: " 				+ meta.Description);
				Debug.Log("Meta.CoverImageUrl): " 			+ meta.CoverImageUrl);
				Debug.Log("Meta.LastModifiedTimestamp: " 	+ meta.LastModifiedTimestamp);
				Debug.Log("Meta.TotalPlayedTime" 			+ meta.TotalPlayedTime);
			}

			if(GooglePlaySavedGamesManager.Instance.AvailableGameSaves.Count > 0) {
				GP_SnapshotMeta s =  GooglePlaySavedGamesManager.Instance.AvailableGameSaves[0];
				AndroidDialog dialog = AndroidDialog.Create("Load Snapshot?", "Would you like to load " + s.Title);
				dialog.ActionComplete += OnSpanshotLoadDialogComplete;
			}

		} else {
			AndroidMessage.Create("Fail", "Available Game Saves Load failed");
		}
	}

	void OnSpanshotLoadDialogComplete (AndroidDialogResult res) {
		if(res == AndroidDialogResult.YES) {
			GP_SnapshotMeta s =  GooglePlaySavedGamesManager.Instance.AvailableGameSaves[0];
			GooglePlaySavedGamesManager.Instance.LoadSpanshotByName(s.Title);
		}
	}

	//--------------------------------------
	// EVENTS
	//--------------------------------------

	private void ActionNewGameSaveRequest () {
		SA_StatusBar.text = "New  Game Save Requested, Creating new save..";
		Debug.Log("New  Game Save Requested, Creating new save..");
		StartCoroutine(MakeScreenshotAndSaveGameData());

		AndroidMessage.Create("Result", "New Game Save Request");
	}

	private void ActionGameSaveLoaded (GP_SpanshotLoadResult result) {

		Debug.Log("ActionGameSaveLoaded: " + result.Message);
		if(result.IsSucceeded) {

			Debug.Log("Snapshot.Title: " 					+ result.Snapshot.meta.Title);
			Debug.Log("Snapshot.Description: " 				+ result.Snapshot.meta.Description);
			Debug.Log("Snapshot.CoverImageUrl): " 			+ result.Snapshot.meta.CoverImageUrl);
			Debug.Log("Snapshot.LastModifiedTimestamp: " 	+ result.Snapshot.meta.LastModifiedTimestamp);

			Debug.Log("Snapshot.stringData: " 				+ result.Snapshot.stringData);
			Debug.Log("Snapshot.bytes.Length: " 			+ result.Snapshot.bytes.Length);

			AndroidMessage.Create("Snapshot Loaded", "Data: " + result.Snapshot.stringData);
		} 

		SA_StatusBar.text = "Games Loaded: " + result.Message;

	}

	private void ActionGameSaveResult (GP_SpanshotLoadResult result) {
		GooglePlaySavedGamesManager.ActionGameSaveResult -= ActionGameSaveResult;
		Debug.Log("ActionGameSaveResult: " + result.Message);

		if(result.IsSucceeded) {
			SA_StatusBar.text = "Games Saved: " + result.Snapshot.meta.Title;
		} else {
			SA_StatusBar.text = "Games Save Failed";
		}

		AndroidMessage.Create("Game Save Result", SA_StatusBar.text);
	}	

	private void ActionConflict (GP_SnapshotConflict result) {

		Debug.Log("Conflict Detected: ");

		GP_Snapshot snapshot = result.Snapshot;
		GP_Snapshot conflictSnapshot = result.ConflictingSnapshot;
		
		// Resolve between conflicts by selecting the newest of the conflicting snapshots.
		GP_Snapshot mResolvedSnapshot = snapshot;
		
		if (snapshot.meta.LastModifiedTimestamp < conflictSnapshot.meta.LastModifiedTimestamp) {
			mResolvedSnapshot = conflictSnapshot;
		}

		result.Resolve(mResolvedSnapshot);
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



	//--------------------------------------
	// PRIVATE METHODS
	//--------------------------------------
	
	private IEnumerator MakeScreenshotAndSaveGameData() {
		
		
		yield return new WaitForEndOfFrame();
		// Create a texture the size of the screen, RGB24 format
		int width = Screen.width;
		int height = Screen.height;
		Texture2D Screenshot = new Texture2D( width, height, TextureFormat.RGB24, false );
		// Read screen contents into the texture
		Screenshot.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
		Screenshot.Apply();
		

		long TotalPlayedTime = 20000;
		//string currentSaveName =  "snapshotTemp-" + Random.Range(1, 281).ToString();
		string currentSaveName =  "TestingSameName";
		string description  = "Modified data at: " + System.DateTime.Now.ToString("MM/dd/yyyy H:mm:ss");


		GooglePlaySavedGamesManager.ActionGameSaveResult += ActionGameSaveResult;
		GooglePlaySavedGamesManager.Instance.CreateNewSnapshot(currentSaveName,
		                                                       description,
		                                                       Screenshot,
		                                                       "some save data, for example you can use JSON or byte array " + Random.Range(1, 10000).ToString(),
		                                                       TotalPlayedTime);		
		Destroy(Screenshot);
	}


}
