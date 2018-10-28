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
	public class DataSource {

		private DataType dataType;
		private DataSourceType dataSourceType;
		private string appPackageName = string.Empty;
		private string device = string.Empty;
		private string name = string.Empty;
		private string streamId = string.Empty;
		private string streamName = string.Empty;

		public DataSource (string[] bundle) {
			appPackageName = bundle [0];
			dataType = new DataType (bundle [1]);
			device = bundle [2];
			name = bundle [3];
			streamId = bundle [4];
			streamName = bundle [5];
			dataSourceType = (DataSourceType) Int32.Parse (bundle[6]);
		}

		public DataType DataType {
			get {
				return dataType;
			}
		}

		public DataSourceType DataSourceType {
			get {
				return dataSourceType;
			}
		}

		public string AppPackageName {
			get {
				return appPackageName;
			}
		}

		public string Device {
			get {
				return device;
			}
		}

		public string Name {
			get {
				return name;
			}
		}

		public string StreamId {
			get {
				return streamId;
			}
		}

		public string StreamName {
			get {
				return streamName;
			}
		}
	}
}
