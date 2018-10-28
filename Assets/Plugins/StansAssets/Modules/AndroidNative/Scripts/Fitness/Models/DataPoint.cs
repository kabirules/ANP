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
	public class DataPoint {

		private long startTime = 0L;
		private long endTime = 0L;

		private DataType dataType;
		private Dictionary<string, object> fields = new Dictionary<string, object>();

		public DataPoint (DataType type, string[] bundle, string key) {
			this.dataType = type;
			this.startTime = long.Parse(bundle[1]);
			this.endTime = long.Parse(bundle[2]);

			for (int i = 3; i < bundle.Length; i++) {
				if (!bundle[i].Equals(string.Empty)) {
					string[] array = bundle [i].Split (new string[] {key}, StringSplitOptions.None);
					fields.Add (array [0], array [1]);
				}
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

		public DataType DataType {
			get {
				return dataType;
			}
		}

		public Dictionary<string, object> Fields {
			get {
				return fields;
			}
		}
	}
}
