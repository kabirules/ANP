////////////////////////////////////////////////////////////////////////////////
//  
// @module Stan's Assets Android Native Fitness
// @author Alexey Yaremenko (Stan's Assets) 
// @support support@stansassets.com
//
////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA.Common.Models;

namespace SA.Fitness {
	public class StopSessionResult : Result {

		private List<Session> sessions = new List<Session>();

		private int id;

		public StopSessionResult (int id) : base() {
			this.id = id;
		}

		public StopSessionResult (int id, int resultCode, string message) : base(new Error(resultCode, message)) {
			this.id = id;
		}

		public void AddSession(Session session) {
			sessions.Add (session);
		}

		public int Id {
			get {
				return id;
			}
		}

		public List<Session> Sessions {
			get {
				return sessions;
			}
		}
	}
}
