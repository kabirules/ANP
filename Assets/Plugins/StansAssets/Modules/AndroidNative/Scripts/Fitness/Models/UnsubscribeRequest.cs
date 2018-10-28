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
using SA.Common.Models;

namespace SA.Fitness {
	public class UnsubscribeRequest {
		public event Action<Result> OnUnsubscribeFinished = delegate {};

		private int id = SA.Common.Util.IdFactory.NextId;
		private DataType dataType;

		private UnsubscribeRequest() {}

		public void DispatchUnsubscribeResult(Result result) {
			OnUnsubscribeFinished (result);
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
			private UnsubscribeRequest request = new UnsubscribeRequest();

			public Builder() {}

			public Builder SetDataType(DataType dataType) {
				request.dataType = dataType;
				return this;
			}

			public UnsubscribeRequest Build() {
				return request;
			}
		}
	}
}
