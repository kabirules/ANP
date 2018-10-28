////////////////////////////////////////////////////////////////////////////////
//  
// @module Stan's Assets Android Native Fitness
// @author Alexey Yaremenko (Stan's Assets) 
// @support support@stansassets.com
//
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Fitness {
	public sealed class Sensors : SA.Common.Pattern.Singleton<Sensors> {

		private Dictionary<int, SensorRequest> requests = new Dictionary<int, SensorRequest> ();
		private Dictionary<int, SensorListener> listeners = new Dictionary<int, SensorListener> ();

		void Awake() {
			DontDestroyOnLoad (gameObject);
		}

		//--------------------------------------
		// PUBLIC API CALL METHODS
		//--------------------------------------

		public void RequestSensors(SensorRequest request) {
			if (request.DataTypes.Count == 0 || request.DataSourceTypes.Count == 0) {
				Debug.LogWarning ("[SA.Fitness] Sensore Request should be setup correctly!");
				return;
			}

			StringBuilder builder = new StringBuilder ();
			builder.Append (request.Id);
			builder.Append (Connection.SEPARATOR2);

			builder.Append (request.DataTypes[0].Value);
			for (int i = 1; i < request.DataTypes.Count; i++) {
				builder.Append (Connection.SEPARATOR1);
				builder.Append (request.DataTypes[i].Value);
			}
			builder.Append (Connection.SEPARATOR2);

			builder.Append ((int)request.DataSourceTypes[0]);
			for (int i = 1; i < request.DataSourceTypes.Count; i++) {
				builder.Append (Connection.SEPARATOR1);
				builder.Append ((int)request.DataSourceTypes[i]);
			}

			requests.Add (request.Id, request);
			Proxy.RequestDataSources (builder.ToString());
		}

		public void RegisterSensorListener(SensorListener listener) {
			StringBuilder builder = new StringBuilder ();
			builder.Append (listener.Id);
			builder.Append (Connection.SEPARATOR2);
			builder.Append (listener.DataType.Value);
			builder.Append (Connection.SEPARATOR2);
			builder.Append (listener.RateAmount.ToString());
			builder.Append (Connection.SEPARATOR2);
			builder.Append (listener.RateTimeUnit.ToString());

			listeners.Add (listener.Id, listener);
			Proxy.RegisterSensorListener (builder.ToString());
		}

		//--------------------------------------
		// PRIVATE
		//--------------------------------------

		//--------------------------------------
		// CALLBACKS - will be called from the Android native code
		//--------------------------------------

		private void SensorsRequestResultHandler(string data) {
			string[] array = data.Split (new string[] {Connection.SEPARATOR2}, StringSplitOptions.None);
			int id = Int32.Parse (array[0]);

			if (requests.ContainsKey(id)) {
				requests [id].DispatchResult (array);
				requests.Remove (id);
			}
		}

		private void SensorListenerRegisterSuccessHandler(string data) {
			int id = Int32.Parse (data);
			if (listeners.ContainsKey(id)) {
				listeners [id].DispatchRegisterSuccess ();
			}
		}

		private void SensorListenerRegisterFailHandler(string data) {
			int id = Int32.Parse (data);
			if (listeners.ContainsKey(id)) {
				listeners [id].DispatchRegisterFail ();
			}
		}

		private void SensorListenerDataPointHandler(string data) {
			string[] array = data.Split (new string[] { Connection.SEPARATOR2 }, StringSplitOptions.None);
			int id = Int32.Parse(array [0]);

			if (listeners.ContainsKey(id)) {
				listeners [id].DispatchDataPointEvent (array);
			}
		}

		//--------------------------------------
		// GET / SET
		//--------------------------------------

	}
}
