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
	public class SubscribeRequest {
		public event Action<Result> OnSubscribeFinished = delegate {};

		private int id = SA.Common.Util.IdFactory.NextId;
		private DataType dataType;

		private SubscribeRequest() {}

		public void DispatchResult(Result result) {
			OnSubscribeFinished (result);
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
			private SubscribeRequest request = new SubscribeRequest();

			public Builder() {}

			public Builder SetDataType(DataType dataType) {
				request.dataType = dataType;
				return this;
			}

			public SubscribeRequest Build() {
				return request;
			}
		}

	}
}
