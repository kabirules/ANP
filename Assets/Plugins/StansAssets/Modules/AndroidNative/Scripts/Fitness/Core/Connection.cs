////////////////////////////////////////////////////////////////////////////////
//  
// @module Stan's Assets Android Native Fitness
// @author Alexey Yaremenko (Stan's Assets) 
// @support support@stansassets.com
//
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Fitness {
	public sealed class Connection : SA.Common.Pattern.Singleton<Connection> {

		public const string SEPARATOR1 = "|";
		public const string SEPARATOR2 = "~";
		public const string SEPARATOR3 = "$";
		public const string SEPARATOR4 = "%";
		public const string SEPARATOR5 = "^";

		private const int RESULT_OK = -1;

		public event Action<ConnectionResult> OnConnectionFinished = delegate {};

		private List<LoginApi> apis = new List<LoginApi> ();
		private List<LoginScope> scopes = new List<LoginScope> ();

		private ConnectionState connectionState = ConnectionState.DISCONNECTED;
		private bool shouldManageReconnection = false;

		void Awake () {
			DontDestroyOnLoad (gameObject);
		}
		
		void OnApplicationPause(bool pauseStatus) { // pauseStatus = True if the application is paused, else False.
			//Disconnect from Google Fit when activity paused and reconnect when resumed
			if (shouldManageReconnection) {
				if (pauseStatus) {
					Disconnect ();
				} else {
					Connect ();
				}
			}
		}

		//--------------------------------------
		// PUBLIC API CALL METHODS
		//--------------------------------------

		public void AddApi (LoginApi api) {
			if (!apis.Contains (api)) {
				apis.Add (api);
			}
		}

		public void AddScope (LoginScope scope) {
			if (!scopes.Contains (scope)) {
				scopes.Add (scope);
			}
		}

		public void Connect() {
			if (apis.Count == 0 || scopes.Count == 0) {
				Debug.LogWarning ("[SA.Fitness] Please, define login APIs & Scopes");
				return;
			}

			StringBuilder builder = new StringBuilder ();
			builder.Append (apis [0].Value);
			for (int i = 1; i < apis.Count; i++) {
				builder.Append (SEPARATOR1);
				builder.Append (apis[i].Value);
			}
			builder.Append (SEPARATOR2);

			builder.Append (scopes [0].Value);
			for (int i = 1; i < scopes.Count; i++) {
				builder.Append (SEPARATOR1);
				builder.Append (scopes [i].Value);
			}

			connectionState = ConnectionState.CONNECTING;
			Proxy.Connect (builder.ToString());
		}

		//--------------------------------------
		// PRIVATE
		//--------------------------------------

		private void Disconnect () {
			Proxy.Disconnect ();
			connectionState = ConnectionState.DISCONNECTED;
		}

		//--------------------------------------
		// CALLBACKS - will be called from the Android native code
		//--------------------------------------

		private void ConnectionResultHandler (string data) {
			string[] array = data.Split(new string[] {SEPARATOR1}, StringSplitOptions.None);

			int code = Int32.Parse (array[0]);
			ConnectionResult result = code == RESULT_OK ? new ConnectionResult () : new ConnectionResult (code, array[1]);
			connectionState = result.IsSucceeded ? ConnectionState.CONNECTED : ConnectionState.DISCONNECTED;
			if (!shouldManageReconnection) {
				OnConnectionFinished (result);
			}
			shouldManageReconnection = result.IsSucceeded;
		}

		//--------------------------------------
		// GET / SET
		//--------------------------------------

		public ConnectionState ConnectionState {
			get {
				return connectionState;
			}
		}
	}
}
