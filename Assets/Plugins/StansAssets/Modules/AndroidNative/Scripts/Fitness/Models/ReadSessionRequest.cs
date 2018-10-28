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
	public class ReadSessionRequest {

		public event Action<ReadSessionResult> OnSessionReadFinished = delegate{};

		private long startTime = 0L;
		private long endTime = 0L;
		private TimeUnit timeUnit = TimeUnit.Milliseconds;
		private string sessionId = string.Empty;
		private DataType dataType;

		private int id = SA.Common.Util.IdFactory.NextId;

		private ReadSessionRequest () {}

		public void DispatchResult(string[] bundle) {
			int resultCode = Int32.Parse(bundle[1]);
			ReadSessionResult result = resultCode == 0 ? new ReadSessionResult (id) : new ReadSessionResult (id, resultCode, bundle [2]);
			if (result.IsSucceeded) {
				for (int i = 3; i < bundle.Length; i++) {
					//Parse each Google Fit session here
					string[] sessionData = bundle[i].Split(new string[] {Connection.SEPARATOR2}, StringSplitOptions.None);
					Session session = new Session ();
					session.StartTime = long.Parse (sessionData [0]);
					session.EndTime = long.Parse (sessionData [1]);
					session.Name = sessionData [2];
					session.Id = sessionData [3];
					session.Description = sessionData [4];
					session.Activity = new Activity (sessionData [5]);
					session.AppPackageName = sessionData [6];

					if (sessionData.Length >= 8) {
						for (int j = 7; j < sessionData.Length; j++) {
							string[] data = sessionData [j].Split (new string[] {Connection.SEPARATOR3}, StringSplitOptions.None);
							DataSet dataSet = new DataSet (new DataType (data[0]));
							if (data.Length > 1) {
								for (int k = 1; k < data.Length; k++) {
									string[] point = data[k].Split(new string[] {Connection.SEPARATOR4}, StringSplitOptions.None);
									List<string> dp = new List<string> ();
									dp.Add (point[0]);
									dp.Add (point[1]);
									dp.Add (point[2]);
									dp.Add (point[3]);

									DataPoint p = new DataPoint (new DataType (point [0]), dp.ToArray (), Connection.SEPARATOR5);
									dataSet.AddDataPoint (p);
								}
							}
							session.AddDataSet (dataSet);
						}
					}
					result.AddSession (session);
				}
			}

			OnSessionReadFinished(result);
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

		public string SessionId {
			get {
				return sessionId;
			}
		}

		public DataType DataType {
			get {
				return dataType;
			}
		}

		public class Builder {
			private ReadSessionRequest request = new ReadSessionRequest();

			public Builder() {}

			public Builder SetTimeinterval(long startTime, long endTime, TimeUnit timeUnit) {
				request.startTime = startTime;
				request.endTime = endTime;
				request.timeUnit = timeUnit;

				return this;
			}

			public Builder SetIdentifier(string sessionId) {
				request.sessionId = sessionId;
				return this;
			}

			public Builder SetDataTypeToRead(DataType dataType) {
				request.dataType = dataType;
				return this;
			}

			public ReadSessionRequest Build() {
				return request;
			}
		}
	}
}
