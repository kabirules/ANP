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
	public class DataSet {

		private DataType dataType;

		private List<DataPoint> dataPoints = new List<DataPoint>();

		public DataSet (DataType dataType) {
			this.dataType = dataType;
		}

		internal void AddDataPoint(DataPoint dataPoint) {
			dataPoints.Add (dataPoint);
		}

		public DataType DataType {
			get {
				return dataType;
			}
		}

		public List<DataPoint> DataPoints {
			get {
				return dataPoints;
			}
		}
	}
}
