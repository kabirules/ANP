using System.Collections;
using UnityEngine;

namespace SA.AndroidNative.DynamicLinks {
	public static class Proxy {
		private const string CLASS_NAME = "com.androidnative.firebase.dynamiclinks.Bridge";

		public static string GetPendingLink() {
			#if UNITY_ANDROID
			return CallStaticFunction<string>("getPendingLink");
			#else
			return string.Empty;
			#endif
		}

		public static void RequestLink(string request) {
			CallStaticFunction ("requestShortLink", request);
		}

		#if UNITY_ANDROID
		private static T CallStaticFunction<T>(string methodName, params object[] args) {
			return AN_ProxyPool.CallStatic<T> (CLASS_NAME, methodName, args);
		}
		#endif

		private static void CallStaticFunction(string methodName, params object[] args) {
			AN_ProxyPool.CallStatic(CLASS_NAME, methodName, args);
		}
	}
}
