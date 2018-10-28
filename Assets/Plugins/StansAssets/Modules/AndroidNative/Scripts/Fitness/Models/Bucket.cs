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
	public sealed class Bucket {

		private List<DataSet> dataSets = new List<DataSet> ();

		private Type type = Type.Time;

		private long startTime = 0L;
		private long endTime = 10L;

		public Bucket(Type type) {
			this.type = type;
		}

		public void SetTimeRange(long startTime, long endTime) {
			this.startTime = startTime;
			this.endTime = endTime;
		}

		public void AddDataSet(DataSet dataSet) {
			dataSets.Add (dataSet);
		}

		public Type Bucketing {
			get {
				return type;
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

		public List<DataSet> DataSets {
			get {
				return dataSets;
			}
		}

		/// <summary>
		/// Type denotes if the bucketing is by time, session or activity.
		/// </summary>
		public enum Type {
			/// <summary>
			/// Type constant denoting that bucketing by individual activity segment is requested.
			/// </summary>
			ActivitySegment = 4,

			/// <summary>
			/// Type constant denoting that bucketing by activity type is requested.
			/// </summary>
			ActivityType = 3,

			/// <summary>
			/// Type constant denoting that bucketing by session is requested.
			/// </summary>
			Session = 2,

			/// <summary>
			/// Type constant denoting that bucketing by time is requested.
			/// </summary>
			Time = 1
		}

	}
}
