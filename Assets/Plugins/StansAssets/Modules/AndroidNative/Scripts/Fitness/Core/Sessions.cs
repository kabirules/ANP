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
using SA.Common.Models;

namespace SA.Fitness {
	public sealed class Sessions : SA.Common.Pattern.Singleton<Sessions> {

		private Dictionary<int, StartSessionRequest> startSessionRequests = new Dictionary<int, StartSessionRequest> ();
		private Dictionary<int, StopSessionRequest> stopSessionRequests = new Dictionary<int, StopSessionRequest>();
		private Dictionary<int, ReadSessionRequest> readSessionRequests = new Dictionary<int, ReadSessionRequest>();

		void Awake () {
			DontDestroyOnLoad(gameObject);
		}

		//--------------------------------------
		// PUBLIC API CALL METHODS
		//--------------------------------------

		public void StartSession(StartSessionRequest request) {
			StringBuilder builder = new StringBuilder ();
			builder.Append (request.Id);
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.Name);
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.SessionId);
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.Description);
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.StartTime);
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.TimeUnit.ToString());
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.Activity.Value);

			startSessionRequests.Add (request.Id, request);
			Proxy.StartSession (builder.ToString());
		}

		public void StopSession(StopSessionRequest request) {
			StringBuilder builder = new StringBuilder ();
			builder.Append (request.Id);
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.SessionId);

			stopSessionRequests.Add (request.Id, request);
			Proxy.StopSession (builder.ToString ());
		}

		public void InsertSession() {
			//TODO: Insert session implementation will be added soon
			/*StringBuilder builder = new StringBuilder ("hello insert session");
			Proxy.InsertSesison (builder.ToString());*/
		}

		public void ReadSession(ReadSessionRequest request) {
			//TODO: Read session implementation will be added soon
			StringBuilder builder = new StringBuilder ();
			builder.Append (request.Id);
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.SessionId);
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.StartTime);
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.EndTime);
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.TimeUnit.ToString());
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.DataType.Value);

			readSessionRequests.Add (request.Id, request);
			Proxy.ReadSession (builder.ToString());
		}

		//--------------------------------------
		// CALLBACKS - will be called from the Android native code
		//--------------------------------------

		private void SessionStartCallbackHandler(string data) {
			string[] bundle = data.Split (new string[] {Connection.SEPARATOR1}, System.StringSplitOptions.None);
			int id = Int32.Parse (bundle[0]);
			int resultCode = Int32.Parse (bundle[1]);
			string message = bundle [2];

			Result result = resultCode == 0 ? new Result () : new Result (new Error (resultCode, message));
			startSessionRequests [id].DispatchSessionStartResult (result);
			startSessionRequests.Remove (id);
		}

		private void SessionStopCallbackHandler(string data) {
			string[] bundle = data.Split (new string[] {Connection.SEPARATOR1}, System.StringSplitOptions.None);
			int id = Int32.Parse (bundle[0]);

			stopSessionRequests [id].DispatchResult (bundle);
			stopSessionRequests.Remove (id);
		}

		private void SessionReadCallbackHandler(string data) {
			string[] bundle = data.Split (new String[] {Connection.SEPARATOR1}, System.StringSplitOptions.None);
			int id = Int32.Parse (bundle[0]);

			readSessionRequests [id].DispatchResult (bundle);
			readSessionRequests.Remove (id);
		}
	}
}
