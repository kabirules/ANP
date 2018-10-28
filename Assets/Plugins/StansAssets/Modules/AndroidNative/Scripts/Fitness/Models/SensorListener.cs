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
	public class SensorListener {

		public event Action<int> OnRegisterSuccess = delegate {};
		public event Action<int> OnRegisterFail = delegate {};

		public event Action<int, DataPoint> OnDataPointReceived = delegate {};

		private int id;

		private DataType dataType;
		private long rateAmount;
		private TimeUnit rateTimeUnit;

		private SensorListener() {
			id = SA.Common.Util.IdFactory.NextId;
		}

		public void DispatchRegisterSuccess() {
			OnRegisterSuccess (id);
		}

		public void DispatchRegisterFail() {
			OnRegisterFail (id);
		}

		public void DispatchDataPointEvent(string[] bundle) {
			OnDataPointReceived (id, new DataPoint (dataType, bundle, Connection.SEPARATOR1));
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

		public long RateAmount {
			get {
				return rateAmount;
			}
		}

		public TimeUnit RateTimeUnit {
			get {
				return rateTimeUnit;
			}
		}

		public class Builder {
			private SensorListener listener = new SensorListener();

			public Builder() {}

			public Builder SetDataType(DataType dataType) {
				listener.dataType = dataType;

				return this;
			}

			public Builder SetSamplingRate (long amount, TimeUnit unit) {
				listener.rateAmount = amount;
				listener.rateTimeUnit = unit;

				return this;
			}

			public SensorListener Build() {
				return listener;
			}
		}

	}
}
