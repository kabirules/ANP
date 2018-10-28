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
	public sealed class LoginApi {

		public static LoginApi SENSORS_API = 	new LoginApi ("SENSORS_API");
		public static LoginApi RECORDING_API = 	new LoginApi ("RECORDING_API");
		public static LoginApi SESSIONS_API = 	new LoginApi ("SESSIONS_API");
		public static LoginApi HISTORY_API = 	new LoginApi ("HISTORY_API");
		public static LoginApi GOALS_API = 		new LoginApi ("GOALS_API");
		public static LoginApi CONFIG_API = 	new LoginApi ("CONFIG_API");
		public static LoginApi BLE_API = 		new LoginApi ("BLE_API");

		private string value = string.Empty;

		private LoginApi() {
		}

		private LoginApi (string api) {
			value = api;
		}

		public override bool Equals (object obj)
		{
			if (GetType() != obj.GetType()) {
				return false;
			}

			LoginApi other = obj as LoginApi;
			return value.Equals (other.Value);
		}

		public override int GetHashCode() {
			return base.GetHashCode ();
		}

		public string Value {
			get {
				return value;
			}
		}
	}
}
