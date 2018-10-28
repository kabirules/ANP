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
	public class ReadDailyTotalResult : Result {
		
		private int id;

		private DataSet dataSet;

		public ReadDailyTotalResult(int id) : base() {
			this.id = id;
		}

		public ReadDailyTotalResult(int id, int resultCode, string message) : base(new Error (resultCode, message)) {
			this.id = id;
		}

		public void SetData(DataSet dataSet) {
			this.dataSet = dataSet;
		}

		public int Id {
			get {
				return id;
			}
		}

		public DataSet DataSet {
			get {
				return dataSet;
			}
		}
	}
}
