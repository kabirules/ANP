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

namespace SA.Fitness {
	public class DeleteDataRequest {

		public event Action OnRequestFinished = delegate{};

		private int id = SA.Common.Util.IdFactory.NextId;

		private long startTime = 0L;
		private long endTime = 1L;
		private TimeUnit timeUnit;

		private List<DataType> dataTypes = new List<DataType>();
		private List<string> sessions = new List<string>();

		public DeleteDataRequest() {
		}

		public void DispatchRequestResult() {
			OnRequestFinished ();
		}

		public int Id {
			get {
				return id;
			}
		}

		public long StartTime {
			get {
				return startTime;
			}
		}

		public long EndTime {
			get {
				return endTime;
			}
		}

		public TimeUnit TimeUnit {
			get {
				return timeUnit;
			}
		}

		public List<DataType> DataTypes {
			get {
				return dataTypes;
			}
		}

		public List<string> Sessions {
			get {
				return sessions;
			}
		}

		public class Builder {
			private DeleteDataRequest request = new DeleteDataRequest();

			public Builder() {				
			}

			public Builder SetTimeInterval(long startTime, long endTime, TimeUnit unit) {
				request.startTime = startTime;
				request.endTime = endTime;
				request.timeUnit = unit;

				return this;
			}

			public Builder AddDataType(DataType dataType) {
				if (!request.dataTypes.Contains(dataType)) {
					request.dataTypes.Add (dataType);
				}

				return this;
			}

			public Builder AddSession(string sessionId) {
				if (!request.sessions.Contains(sessionId)) {
					request.sessions.Add (sessionId);
				}

				return this;
			}

			public Builder DeleteAllSessions() {
				request.sessions.Clear ();

				return this;
			}

			public DeleteDataRequest Build() {
				return request;
			}
		}
	}
}
