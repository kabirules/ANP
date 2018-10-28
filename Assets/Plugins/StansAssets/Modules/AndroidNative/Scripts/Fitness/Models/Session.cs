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

namespace SA.Fitness {
	public class Session {

		public string Id = string.Empty;
		public long StartTime = 0L;
		public long EndTime = 0L;
		public string Name = string.Empty;
		public string Description = string.Empty;
		public string AppPackageName = string.Empty;
		public Activity Activity = Activity.UNKNOWN;

		private List<DataSet> dataSets = new List<DataSet>();

		public Session() {}

		public void AddDataSet(DataSet data) {
			dataSets.Add (data);
		}

		public List<DataSet> DataSets {
			get {
				return dataSets;
			}
		}
	}
}
