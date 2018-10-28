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
	public static class Proxy {

		private const string CLASS_NAME = "com.stansassets.fitness.Bridge";

		private static void Call(string methodName, params object[] args) {
			AN_ProxyPool.CallStatic(CLASS_NAME, methodName, args);
		}

#if UNITY_ANDROID
		private static ReturnType Call<ReturnType>(string methodName, params object[] args) {
			return AN_ProxyPool.CallStatic<ReturnType>(CLASS_NAME, methodName, args);
		}
#endif

		//--------------------------------------
		// CONNECTION
		//--------------------------------------

		public static void Connect(string connectionRequest) {
			Call ("connect", connectionRequest);
		}

		public static void Disconnect() {
			Call ("disconnect");
		}

		//--------------------------------------
		// SENSORS
		//--------------------------------------

		public static void RequestDataSources (string request) {
			Call ("requestDataSources", request);
		}

		public static void RegisterSensorListener (string request) {
			Call ("addSensorListener", request);
		}

		//--------------------------------------
		// RECORDING
		//--------------------------------------

		public static void ListSubscriptions(string request) {
			Call ("listSubscriptions", request);
		}

		public static void Subscribe(string request) {
			Call ("subscribe", request);
		}

		public static void Unsubscribe(string request) {
			Call ("unsubscribe", request);
		}

		//--------------------------------------
		// HISTORY
		//--------------------------------------

		public static void ReadData(string request) {
			Call ("readData", request);
		}

		public static void ReadDailyTotal(string request){
			Call ("readDailyTotal", request);
		}

		public static void ReadDailyTotalFromLocalDevice(string request){
			Call ("readDailyTotalFromLocalDevice", request);
		}

		public static void InsertData(string request){
			Call ("insertData", request);
		}

		public static void UpdateData(string request){
			Call ("updateData", request);
		}

		public static void DeleteData(string request) {
			Call ("deleteData", request);
		}

		//--------------------------------------
		// SESSIONS
		//--------------------------------------

		public static void StartSession(string request) {
			Call ("startSession", request);
		}

		public static void StopSession(string request) {
			Call ("stopSession", request);
		}

		public static void ReadSession(string request) {
			Call ("readSession", request);
		}

		public static void InsertSesison(string request) {
			Call ("insertSession", request);
		}
	}
}
