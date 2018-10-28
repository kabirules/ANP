////////////////////////////////////////////////////////////////////////////////
//  
// @module Stan's Assets Android Native Fitness
// @author Alexey Yaremenko (Stan's Assets) 
// @support support@stansassets.com
//
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using SA.Common.Models;

namespace SA.Fitness {
	public sealed class Recording : SA.Common.Pattern.Singleton<Recording> {

		private Dictionary<int, SubscribeRequest> subscriptions = new Dictionary<int, SubscribeRequest>();
		private Dictionary<int, UnsubscribeRequest> unsubs = new Dictionary<int, UnsubscribeRequest>();
		private Dictionary<int, SubscriptionsRequest> subsRequests = new Dictionary<int, SubscriptionsRequest>();

		void Awake() {
			DontDestroyOnLoad (gameObject);
		}

		//--------------------------------------
		// PUBLIC API CALL METHODS
		//--------------------------------------

		public void Subscribe(SubscribeRequest request) {
			StringBuilder builder = new StringBuilder ();
			builder.Append (request.Id);
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.DataType.Value);

			subscriptions.Add (request.Id, request);
			Proxy.Subscribe (builder.ToString());
		}

		public void ListSubscriptions(SubscriptionsRequest request) {
			StringBuilder builder = new StringBuilder ();
			builder.Append (request.Id);
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.DataType == null ? string.Empty : request.DataType.Value);

			subsRequests.Add (request.Id, request);
			Proxy.ListSubscriptions (builder.ToString());
		}

		public void Unsubscribe(UnsubscribeRequest request) {
			StringBuilder builder = new StringBuilder ();
			builder.Append (request.Id);
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.DataType.Value);

			unsubs.Add (request.Id, request);
			Proxy.Unsubscribe (builder.ToString());
		}

		//--------------------------------------
		// PRIVATE
		//--------------------------------------
		
		//--------------------------------------
		// CALLBACKS - will be called from the Android native code
		//--------------------------------------

		private void SubscribeResultListener(string data) {
			string[] bundle = data.Split (new string[] {Connection.SEPARATOR1}, System.StringSplitOptions.None);
			int id = Int32.Parse (bundle[0]);
			int resultCode = Int32.Parse (bundle[1]);
			string message = bundle [2];

			Result result = resultCode == 0 ? new Result () : new Result (new Error (resultCode, message));
			subscriptions [id].DispatchResult (result);
			subscriptions.Remove (id);
		}

		private void ListSubsResultListener (string data) {
			string[] bundle = data.Split (new string[] {Connection.SEPARATOR2}, System.StringSplitOptions.None);
			int id = Int32.Parse (bundle[0]);

			subsRequests [id].DispatchRequestResult (bundle);
			subsRequests.Remove (id);
		}

		private void UnsubResultListener(string data) {
			string[] bundle = data.Split (new string[] {Connection.SEPARATOR1}, System.StringSplitOptions.None);
			int id = Int32.Parse (bundle[0]);
			int resultCode = Int32.Parse (bundle[1]);
			string message = bundle [2];

			Result result = resultCode == 0 ? new Result () : new Result (new Error (resultCode, message));
			unsubs [id].DispatchUnsubscribeResult (result);
			unsubs.Remove (id);
		}

		//--------------------------------------
		// GET / SET
		//--------------------------------------

	}
}
