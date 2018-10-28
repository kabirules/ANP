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
	public class ReadHistoryRequest {

		public event Action<ReadHistoryResult> OnReadFinished = delegate {};

		private int id = SA.Common.Util.IdFactory.NextId;

		private long startTime = 0L;
		private long endTime = 1L;
		private TimeUnit timeUnit = TimeUnit.Milliseconds;

		private DataType dataType;
		private DataType aggregateType;
		private int limit = 0;

		private bool isAggregated = false;
		private int minDuration = 0;
		private TimeUnit bucketUnit = TimeUnit.Milliseconds;
		private Bucket.Type bucketingType = Bucket.Type.Time;

		private ReadHistoryRequest () {}

		public void DispatchReadResult (string[] bundle) {
			int resultCode = Int32.Parse (bundle [1]);
			string message = bundle[2];

			ReadHistoryResult result = resultCode == 0 ? new ReadHistoryResult (id, isAggregated) : new ReadHistoryResult (id, resultCode, message);
			if (result.IsSucceeded) {
				for (int i = 3; i < bundle.Length; i++) {
					string[] setData = bundle [i].Split (new string[] {Connection.SEPARATOR2}, StringSplitOptions.None);
					DataSet set = new DataSet (new DataType(setData[0]));
					if (setData.Length > 1) {
						for (int k = 1; k < setData.Length; k++) {
							string[] point = setData[k].Split(new string[] {Connection.SEPARATOR3}, StringSplitOptions.None);
							List<string> dp = new List<string> ();
							dp.Add (point[0]);
							dp.Add (point[1]);
							dp.Add (point[2]);
							dp.Add (point[3]);

							DataPoint p = new DataPoint (new DataType (point [0]), dp.ToArray (), Connection.SEPARATOR4);
							set.AddDataPoint (p);
						}
					}
					result.AddDataSet (set);
				}
			}

			OnReadFinished (result);
		}

		public void DispatchAggregatedResult (string[] bundle) {
			int resultCode = Int32.Parse (bundle [1]);
			string message = bundle[2];

			ReadHistoryResult result = resultCode == 0 ? new ReadHistoryResult (id, isAggregated) : new ReadHistoryResult (id, resultCode, message);
			if (result.IsSucceeded) {
				for (int i = 3; i < bundle.Length; i++) {
					string[] bucketData = bundle [i].Split (new string[] {Connection.SEPARATOR2}, StringSplitOptions.None);
					Bucket bucket = new Bucket ((Bucket.Type)Int32.Parse(bucketData[0]));
					bucket.SetTimeRange (long.Parse(bucketData[1]), long.Parse(bucketData[2]));
					if (bucketData.Length > 3) {
						for (int j = 3; j < bucketData.Length; j++) {
							string[] setData = bucketData [j].Split (new string[] {Connection.SEPARATOR3}, StringSplitOptions.None);
							DataSet set = new DataSet (new DataType(setData[0]));
							if (setData.Length > 1) {
								for (int k = 1; k < setData.Length; k++) {
									string[] point = setData[k].Split(new string[] {Connection.SEPARATOR4}, StringSplitOptions.None);
									List<string> dp = new List<string> ();
									dp.Add (point[0]);
									dp.Add (point[1]);
									dp.Add (point[2]);
									dp.Add (point[3]);

									DataPoint p = new DataPoint (new DataType (point [0]), dp.ToArray (), Connection.SEPARATOR5);
									set.AddDataPoint (p);
								}
							}
							bucket.AddDataSet (set);
						}
					}
					result.AddBucket (bucket);
				}
			}

			OnReadFinished (result);
		}

		public bool IsAggregated {
			get {
				return isAggregated;
			}
		}

		public Bucket.Type BucketingType {
			get {
				return bucketingType;
			}
		}

		public TimeUnit BucketUnit {
			get {
				return bucketUnit;
			}
		}

		public int MinDuration {
			get {
				return minDuration;
			}
		}

		public int Limit {
			get {
				return limit;
			}
		}

		public DataType AggregateType {
			get {
				return aggregateType;
			}
		}

		public DataType DataType {
			get {
				return dataType;
			}
		}

		public TimeUnit TimeUnit {
			get {
				return timeUnit;
			}
		}

		public long EndTime {
			get {
				return endTime;
			}
		}

		public long StartTime {
			get {
				return startTime;
			}
		}

		public int Id {
			get {
				return id;
			}
		}

		public class Builder {
			private ReadHistoryRequest request = new ReadHistoryRequest();

			public Builder() {}

			public Builder Aggregate(DataType inputDataType, DataType outputDataType) {
				request.isAggregated = true;
				request.dataType = inputDataType;
				request.aggregateType = outputDataType;
				return this;
			}

			public Builder Read(DataType dataType) {
				request.isAggregated = false;
				request.dataType = dataType;
				return this;
			}

			public Builder BucketByActivitySegment(int minDuration, TimeUnit timeUnit) {
				request.minDuration = minDuration;
				request.bucketUnit = timeUnit;
				request.bucketingType = Bucket.Type.ActivitySegment;
				return this;
			}

			public Builder BucketByActivityType(int minDuration, TimeUnit timeUnit) {
				request.minDuration = minDuration;
				request.bucketUnit = timeUnit;
				request.bucketingType = Bucket.Type.ActivityType;
				return this;
			}

			public Builder BucketBySession(int minDuration, TimeUnit timeUnit) {				
				request.minDuration = minDuration;
				request.bucketUnit = timeUnit;
				request.bucketingType = Bucket.Type.Session;
				return this;
			}

			public Builder BucketByTime(int minDuration, TimeUnit timeUnit) {
				request.minDuration = minDuration;
				request.bucketUnit = timeUnit;
				request.bucketingType = Bucket.Type.Time;
				return this;
			}

			public Builder SetLimit(int limit) {
				request.limit = limit;
				return this;
			}

			public Builder SetTimeRange(long startTime, long endTime, TimeUnit unit) {
				request.startTime = startTime;
				request.endTime = endTime;
				request.timeUnit = unit;

				return this;
			}

			public ReadHistoryRequest Build() {
				return request;
			}
		}

	}
}
