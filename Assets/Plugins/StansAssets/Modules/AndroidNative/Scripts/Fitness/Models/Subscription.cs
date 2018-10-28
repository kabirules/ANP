////////////////////////////////////////////////////////////////////////////////
//  
// @module Stan's Assets Android Native Fitness
// @author Alexey Yaremenko (Stan's Assets) 
// @support support@stansassets.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

namespace SA.Fitness {
	public class Subscription {

		private DataType dataType;

		public Subscription(DataType dataType) {
			this.dataType = dataType;
		}

		public DataType DataType {
			get {
				return dataType;
			}
		}
	}
}
