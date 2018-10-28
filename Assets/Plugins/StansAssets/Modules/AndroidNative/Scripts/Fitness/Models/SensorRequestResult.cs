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
	public class SensorRequestResult : Result {

		private int id;

		private List<DataSource> dataSources = new List<DataSource>();

		public SensorRequestResult(int id) : base() {
			this.id = id;
		}

		public SensorRequestResult(int id, int resultCode, string message) : base(new Error (resultCode, message)) {
			this.id = id;
		}

		public void AddDataSource(DataSource source) {
			dataSources.Add (source);
		}

		public int Id {
			get {
				return id;
			}
		}

		public List<DataSource> DataSources {
			get {
				return dataSources;
			}
		}
	}
}
