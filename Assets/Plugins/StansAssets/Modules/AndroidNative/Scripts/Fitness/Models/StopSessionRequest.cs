////////////////////////////////////////////////////////////////////////////////
//  
// @module Stan's Assets Android Native Fitness
// @author Alexey Yaremenko (Stan's Assets) 
// @support support@stansassets.com
//
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA.Common.Models;

namespace SA.Fitness {
	public class StopSessionRequest {

		public event Action<StopSessionResult> OnSessionStopped = delegate {};

		private int id = SA.Common.Util.IdFactory.NextId;

		private string sessionId = string.Empty;

		private StopSessionRequest() {}

		public void DispatchResult(string[] bundle) {
			int resultCode = Int32.Parse(bundle[1]);
			StopSessionResult result = resultCode == 0 ? new StopSessionResult (id) : new StopSessionResult (id, resultCode, bundle [2]);
			if (result.IsSucceeded) {
				for (int i = 2; i < bundle.Length; i++) {
					string[] sessionData = bundle [i].Split (new string[] { Connection.SEPARATOR2 }, StringSplitOptions.None);

					Session session = new Session ();
					session.StartTime = long.Parse (sessionData [0]);
					session.EndTime = long.Parse (sessionData[1]);
					session.Name = sessionData [2];
					session.Id = sessionData [3];
					session.Description = sessionData [4];
					session.Activity = new Activity (sessionData [5]);
					session.AppPackageName = sessionData [6];

					result.AddSession (session);
				}
			}

			OnSessionStopped (result);
		}

		public int Id {
			get {
				return id;
			}
		}

		public string SessionId {
			get {
				return sessionId;
			}
		}

		public class Builder {
			private StopSessionRequest request = new StopSessionRequest();

			public Builder() {}

			public Builder SetIdentifier(string sessionId) {
				request.sessionId = sessionId;
				return this;
			}

			public StopSessionRequest Build() {
				return request;
			}
		}
	}
}
