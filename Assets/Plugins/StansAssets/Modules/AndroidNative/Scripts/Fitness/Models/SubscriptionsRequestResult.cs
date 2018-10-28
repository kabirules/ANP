////////////////////////////////////////////////////////////////////////////////
//  
// @module Stan's Assets Android Native Fitness
// @author Alexey Yaremenko (Stan's Assets) 
// @support support@stansassets.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections.Generic;
using SA.Common.Models;

namespace SA.Fitness {
	public class SubscriptionsRequestResult : Result {

		private List<Subscription> subscriptions = new List<Subscription>();

		private int id;

		public SubscriptionsRequestResult (int id) : base() {
			this.id = id;
		}

		public SubscriptionsRequestResult (int id, int resultCode, string message) : base(new Error(resultCode, message)) {
			this.id = id;
		}

		public void AddSubscription(Subscription subscription) {
			subscriptions.Add (subscription);
		}

		public int Id {
			get {
				return id;
			}
		}

		public List<Subscription> Subscriptions {
			get {
				return subscriptions;
			}
		}
	}
}
