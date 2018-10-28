////////////////////////////////////////////////////////////////////////////////
//  
// @module Stan's Assets Android Native Fitness
// @author Alexey Yaremenko (Stan's Assets) 
// @support support@stansassets.com
//
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Fitness {
	public sealed class History : SA.Common.Pattern.Singleton<History> {

		private Dictionary<int, ReadDailyTotalRequest> dailyTotalRequests = new Dictionary<int, ReadDailyTotalRequest> ();
		private Dictionary<int, ReadHistoryRequest> readRequests = new Dictionary<int, ReadHistoryRequest> ();

		void Awake () {
			DontDestroyOnLoad (gameObject);
		}

		//--------------------------------------
		// PUBLIC API CALL METHODS
		//--------------------------------------

		public void ReadData(ReadHistoryRequest request) {
			StringBuilder builder = new StringBuilder ();
			builder.Append (request.Id);
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.StartTime.ToString());
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.EndTime.ToString());
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.TimeUnit.ToString());
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.Limit);
			builder.Append (Connection.SEPARATOR1);
			builder.Append (System.Convert.ToInt32 (request.IsAggregated));
			builder.Append (Connection.SEPARATOR1);

			if (request.IsAggregated) {
				builder.Append (request.DataType.Value);
				builder.Append (Connection.SEPARATOR1);
				builder.Append (request.AggregateType.Value);
				builder.Append (Connection.SEPARATOR1);
				builder.Append ((int)request.BucketingType);
				builder.Append (Connection.SEPARATOR1);
				builder.Append (request.MinDuration);
				builder.Append (Connection.SEPARATOR1);
				builder.Append (request.BucketUnit.ToString());
			} else {
				builder.Append (request.DataType.Value);
			}

			readRequests.Add (request.Id, request);
			Proxy.ReadData (builder.ToString());
		}

		public void ReadDailyTotal(ReadDailyTotalRequest request) {
			StringBuilder builder = new StringBuilder ();
			builder.Append (request.Id);
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.DataType.Value);

			dailyTotalRequests.Add (request.Id, request);
			Proxy.ReadDailyTotal (builder.ToString());
		}

		public void ReadDailyTotalFromLocalDevice(ReadDailyTotalRequest request) {
			StringBuilder builder = new StringBuilder ();
			builder.Append (request.Id);
			builder.Append (Connection.SEPARATOR1);
			builder.Append (request.DataType.Value);

			dailyTotalRequests.Add (request.Id, request);
			Proxy.ReadDailyTotalFromLocalDevice (builder.ToString ());
		}

		public void InsertData(){
			StringBuilder builder = new StringBuilder ("hello insert data");

			Proxy.InsertData (builder.ToString());
		}

		public void UpdateData(){
			StringBuilder builder = new StringBuilder ("hello update data");

			Proxy.UpdateData (builder.ToString ());
		}

		public void DeleteData(DeleteDataRequest request) {
			StringBuilder builder = new StringBuilder ("hello delete data");

			Proxy.DeleteData (builder.ToString ());
		}

		//--------------------------------------
		// CALLBACKS - will be called from the Android native code
		//--------------------------------------

		private void DispatchReadDailyTotalRequest (string data) {
			string[] array = data.Split (new string[] {Connection.SEPARATOR1}, System.StringSplitOptions.None);
			int id = Int32.Parse (array [0]);

			dailyTotalRequests [id].DispatchResult (array);
			dailyTotalRequests.Remove (id);
		}

		private void DispatchReadHistoryRequest (string data){
			string[] bundle = data.Split (new string[] {Connection.SEPARATOR1}, StringSplitOptions.None);
			int id = Int32.Parse (bundle [0]);

			if (readRequests [id].IsAggregated) {
				readRequests [id].DispatchAggregatedResult (bundle);
			} else {
				readRequests [id].DispatchReadResult (bundle);
			}

			readRequests.Remove (id);
		}

	}
}
