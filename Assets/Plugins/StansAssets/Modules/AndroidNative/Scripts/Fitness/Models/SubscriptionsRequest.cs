////////////////////////////////////////////////////////////////////////////////
//  
// @module Stan's Assets Android Native Fitness
// @author Alexey Yaremenko (Stan's Assets) 
// @support support@stansassets.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System;
using System.Collections;

namespace SA.Fitness {
	public class SubscriptionsRequest {

		public event Action<SubscriptionsRequestResult> OnRequestFinished = delegate {};

		private int id = SA.Common.Util.IdFactory.NextId;
		private DataType dataType;

		private SubscriptionsRequest() {}

		public void DispatchRequestResult(string[] bundle) {
			int resultCode = Int32.Parse (bundle[1]);
			string message = bundle [2];

			SubscriptionsRequestResult result;
			if (resultCode == 0) {
				result = new SubscriptionsRequestResult (this.id);
				for (int i = 3; i < bundle.Length; i++) {
					if (!bundle[i].Equals(string.Empty)) {
						Subscription subscription = new Subscription (new DataType (bundle[i]));
						result.AddSubscription (subscription);
					}
				}
			} else {
				result = new SubscriptionsRequestResult (this.id, resultCode, message);
			}

			OnRequestFinished (new SubscriptionsRequestResult(this.id));
		}

		public int Id {
			get {
				return id;
			}
		}

		public DataType DataType {
			get {
				return dataType;
			}
		}

		public class Builder {
			private SubscriptionsRequest request = new SubscriptionsRequest();

			public Builder() {}

			public Builder SetDataType(DataType dataType) {
				request.dataType = dataType;
				return this;
			}

			public SubscriptionsRequest Build() {
				return request;
			}
		}
	}
}
