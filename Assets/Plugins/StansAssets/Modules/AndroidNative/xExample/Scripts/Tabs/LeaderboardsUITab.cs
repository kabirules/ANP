using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using SA.Common.Models.UI.Core;

namespace SA.AN.GOOGLE_LEADERBOARD_UI {
	
	public class LeaderboardsUITab : FeatureTab {

		//example
		private const string LEADERBOARD_ID = "CgkIipfs2qcGEAIQAA";
		
		public Image avatar;
		private Sprite defaulttexture;
		private Sprite logo;	
		
		public Button connectButton;
		public Text connectButtonLabel;
		public Text playerLabel;
		
		public Toggle GlobalButton;
		public Toggle FriendsButton;	
		
		public Toggle AllTimeButton;
		public Toggle WeekButton;
		public Toggle TodayButton;	
		
		public Button SubmitScoreButton;	
		public Text SubmitScoreLabel;
		
		public Selectable[] ConnectionDependedntButtons;
		public LeaderboardInfoPresenter[] lines;
		
		private GPLeaderBoard loadedLeaderBoard = null;
		private GPCollectionType displayCollection = GPCollectionType.GLOBAL;
		private GPBoardTimeSpan displayTime = GPBoardTimeSpan.ALL_TIME;
		
		private int score = 100;
		
		
		//--------------------------------------
		// INITIALIZATION
		//--------------------------------------
		
		void Start() {	
			playerLabel.text = "Player Disconnected";
			defaulttexture = avatar.sprite;

			SA_StatusBar.text = "Custom Leader-board example scene loaded";
			
			foreach(LeaderboardInfoPresenter line in lines) {
				line.Disable();
			}		
			
			//listen for GooglePlayConnection events	
			GooglePlayConnection.ActionPlayerConnected +=  OnPlayerConnected;
			GooglePlayConnection.ActionPlayerDisconnected += OnPlayerDisconnected;		
			GooglePlayConnection.ActionConnectionResultReceived += OnConnectionResult;	
			
			GooglePlayManager.ActionScoreSubmited += OnScoreSbumitted;		
			
			//Same events, one with C# actions, one with FLE
			GooglePlayManager.ActionScoresListLoaded += ActionScoreRequestReceived;		
			
			if(GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED) {
				//checking if player already connected
				OnPlayerConnected ();
			}

			GlobalButton.onValueChanged.AddListener ((b) => {
				if (b) {
					displayCollection = GPCollectionType.GLOBAL;
					UpdateScoresDisaplay();
				}
			});
			FriendsButton.onValueChanged.AddListener ((b) => {
				if (b) {
					displayCollection = GPCollectionType.FRIENDS;
					UpdateScoresDisaplay();
				}
			});

			AllTimeButton.onValueChanged.AddListener ((b) => {
				if (b) {
					displayTime = GPBoardTimeSpan.ALL_TIME;
					UpdateScoresDisaplay();
				}
			});
			WeekButton.onValueChanged.AddListener ((b) => {
				if (b) {
					displayTime = GPBoardTimeSpan.WEEK;
					UpdateScoresDisaplay();
				}
			});
			TodayButton.onValueChanged.AddListener ((b) => {
				if (b) {
					displayTime = GPBoardTimeSpan.TODAY;
					UpdateScoresDisaplay();
				}
			});
		}	
		
		//--------------------------------------
		// METHODS
		//--------------------------------------	
		
		public void LoadScore() {
			
			GooglePlayManager.Instance.LoadPlayerCenteredScores(LEADERBOARD_ID, displayTime, displayCollection, 10);
		}
		
		public void OpenUI() {
			GooglePlayManager.Instance.ShowLeaderBoardById(LEADERBOARD_ID);
		}
		
		
		
		public void ShowGlobal() {
			displayCollection = GPCollectionType.GLOBAL;
			UpdateScoresDisaplay();
		}
		
		public void ShowLocal() {
			displayCollection = GPCollectionType.FRIENDS;
			UpdateScoresDisaplay();
		}
		
		
		public void ShowAllTime() {
			displayTime = GPBoardTimeSpan.ALL_TIME;
			UpdateScoresDisaplay();
		}
		
		public void ShowWeek() {
			displayTime = GPBoardTimeSpan.WEEK;
			UpdateScoresDisaplay();
		}
		
		public void ShowDay() {
			displayTime = GPBoardTimeSpan.TODAY;
			UpdateScoresDisaplay();
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
		
		
		//--------------------------------------
		// UNITY
		//--------------------------------------
		
		void UpdateScoresDisaplay() {
			
			
			
			if(loadedLeaderBoard != null) {
				
				
				//Getting current player score
				int displayRank;
				
				GPScore currentPlayerScore = loadedLeaderBoard.GetCurrentPlayerScore(displayTime, displayCollection);
				if(currentPlayerScore == null) {
					//Player does not have rank at this collection / time
					//so let's show the top score
					//since we used loadPlayerCenteredScores function. we should have top scores loaded if player have no scores at this collection / time
					//https://developer.android.com/reference/com/google/android/gms/games/leaderboard/Leaderboards.html#loadPlayerCenteredScores(com.google.android.gms.common.api.GoogleApiClient, java.lang.String, int, int, int)
					//Asynchronously load the player-centered page of scores for a given leaderboard. If the player does not have a score on this leaderboard, this call will return the top page instead.
					displayRank = 1;
				} else {
					//Let's show 5 results before curent player Rank
					displayRank = Mathf.Clamp(currentPlayerScore.Rank - 5, 1, currentPlayerScore.Rank);
					
					//let's check if displayRank we what to display before player score is exists
					while(loadedLeaderBoard.GetScore(displayRank, displayTime, displayCollection) == null) {
						displayRank++;
					}
				}			
				
				Debug.Log("Start Display at rank: " + displayRank);			
				
				int i = displayRank;
				foreach(LeaderboardInfoPresenter line in lines) {
					
					GPScore score = loadedLeaderBoard.GetScore(i, displayTime, displayCollection);
					if(score != null) {
						GooglePlayerTemplate player = GooglePlayManager.Instance.GetPlayerById(score.PlayerId);
						line.SetInfo(i.ToString(),
						             score.LongScore.ToString(),
						             score.PlayerId,
						             player != null ? player.name : "[Empty]",
						             player != null && player.hasIconImage ? player.icon : defaulttexture.texture);
					} else {
						line.Disable();
					}
					
					i++;
				}
			} else {
				foreach(LeaderboardInfoPresenter line in lines) {
					line.Disable();
				}
			}
		}	
		
		void FixedUpdate() {		
			
			SubmitScoreLabel.text = "Submit Score: " + score;
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
				
				foreach(Selectable btn in ConnectionDependedntButtons) {
					btn.interactable = true;
				}
				
				switch(displayTime) {
				case GPBoardTimeSpan.ALL_TIME:
					AllTimeButton.Select();
					break;
				case GPBoardTimeSpan.WEEK:
					WeekButton.Select();
					break;
				case GPBoardTimeSpan.TODAY:
					TodayButton.Select();
					break;
				}

				switch(displayCollection) {
				case GPCollectionType.GLOBAL:
					GlobalButton.Select();
					break;
				case GPCollectionType.FRIENDS:
					FriendsButton.Select();
					break;
				}
				
				
			} else {
				foreach(Selectable btn in ConnectionDependedntButtons) {
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
		
		
		//--------------------------------------
		// EVENTS
		//--------------------------------------
		
		
		
		
		public void SubmitScore() {
			GooglePlayManager.Instance.SubmitScoreById(LEADERBOARD_ID, score);
			SA_StatusBar.text = "Submitiong score: " + (score +1).ToString();
			score ++;
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
			
			SA_StatusBar.text = "Connection Resul:  " + result.code.ToString();
			Debug.Log(result.code.ToString());
		}
		
		
		
		private void ActionScoreRequestReceived (GooglePlayResult obj) {
			
			SA_StatusBar.text = "Scores Load Finished";
			
			loadedLeaderBoard = GooglePlayManager.Instance.GetLeaderBoard(LEADERBOARD_ID);
			
			
			if(loadedLeaderBoard == null) {
				Debug.Log("No Leaderboard found");
				return;
			}
			
			List<GPScore> scoresLB =  loadedLeaderBoard.GetScoresList(GPBoardTimeSpan.ALL_TIME, GPCollectionType.GLOBAL);
			
			foreach(GPScore score in scoresLB) {
				Debug.Log("OnScoreUpdated " + score.Rank + " " + score.PlayerId + " " + score.LongScore);
			}
			
			GPScore currentPlayerScore = loadedLeaderBoard.GetCurrentPlayerScore(displayTime, displayCollection);
			
			Debug.Log("currentPlayerScore: " + currentPlayerScore.LongScore + " rank:" + currentPlayerScore.Rank);
			
			
			UpdateScoresDisaplay();
			
		}
		
		void OnScoreSbumitted (GP_LeaderboardResult result) {
			SA_StatusBar.text = "Score Submit Resul:  " + result.Message;
			LoadScore();
		}
		
		void OnDestroy() {
			
			GooglePlayConnection.ActionPlayerConnected +=  OnPlayerConnected;
			GooglePlayConnection.ActionPlayerDisconnected += OnPlayerDisconnected;
			
			GooglePlayConnection.ActionConnectionResultReceived += OnConnectionResult;
			
			
			GooglePlayManager.ActionScoreSubmited -= OnScoreSbumitted;
			GooglePlayManager.ActionScoresListLoaded -= ActionScoreRequestReceived;
			
		}

	}
}
