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
	public class SensorRequest {

		public event Action<SensorRequestResult> OnRequestFinished = delegate{};

		private int id;

		private List<DataType> dataTypes = new List<DataType>();
		private List<DataSourceType> dataSourceTypes = new List<DataSourceType> ();

		private SensorRequest() {
			id = SA.Common.Util.IdFactory.NextId;
		}

		public void DispatchResult(string[] bundle) {
			int statusCode = Int32.Parse(bundle [1]);
			string message = bundle [2];
			SensorRequestResult result = statusCode == 0 ? new SensorRequestResult (id) : new SensorRequestResult (id, statusCode, message);
			for (int i = 3; i < bundle.Length; i++) {
				string[] dataSource = bundle [i].Split (new string[] {Connection.SEPARATOR1}, StringSplitOptions.None);
				DataSource source = new DataSource (dataSource);
				result.AddDataSource (source);
			}

			OnRequestFinished (result);
		}

		public int Id {
			get {
				return id;
			}
		}

		public List<DataType> DataTypes {
			get {
				return dataTypes;
			}
		}

		public List<DataSourceType> DataSourceTypes {
			get {
				return dataSourceTypes;
			}
		}

		public class Builder {

			private SensorRequest request = new SensorRequest ();

			public Builder AddDataType(DataType dataType) {
				if (!request.dataTypes.Contains (dataType)) {
					request.dataTypes.Add (dataType);
				}
				return this;
			}

			public Builder AddDataSourceType(DataSourceType dataSourceType) {
				if (!request.dataSourceTypes.Contains(dataSourceType)) {
					request.DataSourceTypes.Add (dataSourceType);
				}
				return this;
			}

			public SensorRequest Build() {
				return request;
			}

		}

	}
}
