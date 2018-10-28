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
	public class ConnectionResult : Result {

		public ConnectionResult() : base() {}

		public ConnectionResult(int resultCode, string message) : base(new Error (resultCode, message)) {}
	}
}
