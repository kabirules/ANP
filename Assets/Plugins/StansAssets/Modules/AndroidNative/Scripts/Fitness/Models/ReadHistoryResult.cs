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
	public class ReadHistoryResult : Result {

		private List<Bucket> buckets = new List<Bucket> ();
		private List<DataSet> dataSets = new List<DataSet>();

		private int id;

		private bool isAggregated = false;

		public ReadHistoryResult (int id, bool isAggregated) : base() {
			this.id = id;
			this.isAggregated = isAggregated;
		}

		public ReadHistoryResult (int id, int resultCode, string message) : base(new Error(resultCode, message)) {
			this.id = id;
		}

		public void AddDataSet (DataSet dataSet) {
			dataSets.Add (dataSet);
		}

		public void AddBucket (Bucket bucket) {
			buckets.Add (bucket);
		}

		public int Id {
			get {
				return id;
			}
		}

		public bool IsAggregated {
			get {
				return isAggregated;
			}
		}

		public List<Bucket> Buckets {
			get {
				return buckets;
			}
		}

		public List<DataSet> DataSets {
			get {
				return dataSets;
			}
		}
	}
}
