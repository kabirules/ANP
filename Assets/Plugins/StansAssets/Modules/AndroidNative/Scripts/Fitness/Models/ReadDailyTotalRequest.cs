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
	public class ReadDailyTotalRequest {

		public event Action<ReadDailyTotalResult> OnRequestFinished = delegate {};

		private int id = SA.Common.Util.IdFactory.NextId;

		private DataType dataType;

		private ReadDailyTotalRequest() {
		}

		public void DispatchResult(string[] bundle) {
			int resultCode = Int32.Parse (bundle [1]);
			ReadDailyTotalResult result = resultCode == 0 ? new ReadDailyTotalResult (id) : new ReadDailyTotalResult (id, resultCode, bundle [2]);
			if (result.IsSucceeded) {
				DataSet dataSet = new DataSet (dataType);
				if (bundle.Length > 3) {
					//Parse DataSet here
					for (int i = 4; i < bundle.Length; i++) {
						string[] pointsData = bundle[i].Split(new string[] {Connection.SEPARATOR2}, StringSplitOptions.None);
						List<string> dp = new List<string> ();
						dp.Add (pointsData[0]);
						dp.Add (pointsData[1]);
						dp.Add (pointsData[2]);
						dp.Add (pointsData[3]);

						DataPoint p = new DataPoint (new DataType (pointsData [0]), dp.ToArray (), Connection.SEPARATOR3);
						dataSet.AddDataPoint (p);
					}
				}
				result.SetData (dataSet);
			}
			OnRequestFinished (result);
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
			private ReadDailyTotalRequest request = new ReadDailyTotalRequest();

			public Builder() {}

			public Builder SetDataType(DataType dataType) {
				request.dataType = dataType;
				return this;
			}

			public ReadDailyTotalRequest Build() {
				return request;
			}
		}
	}
}
