using UnityEngine;
using System.Text;
using System.Collections.Generic;

namespace SA.AndroidNative.Firebase {
	public static class Analytics {

		private const string SEPARATOR1 = "%";
		private const string SEPARATOR2 = "|";

		public static void Init () {
			Proxy.Init ();
		}

		public static void SetEnabled (bool enabled) {
			Proxy.SetEnabled (enabled);
		}

		public static void SetMinimumSessionDuration(long milliseconds) {
			Proxy.SetMinimumSessionDuration (milliseconds);
		}

		public static void SetSessionTimeoutDuration(long milliseconds) {
			Proxy.SetSessionTimeoutDuration (milliseconds);
		}

		public static void SetUserId(string userId) {
			Proxy.SetUserId (userId);
		}

		public static void SetUserProperty(string name, string value) {
			Proxy.SetUserProperty (name, value);
		}

		public static void LogEvent(string name, Dictionary<string, object> data = null) {
			if (data == null || data.Count == 0) {
				Proxy.LogEvent (name, null);
			} else {			
				Dictionary<string, object>.Enumerator enumerator = data.GetEnumerator ();
				enumerator.MoveNext ();

				StringBuilder builder = new StringBuilder ();
				builder.Append (enumerator.Current.Key);
				builder.Append (SEPARATOR2);
				builder.Append (enumerator.Current.Value.ToString ());

				while (enumerator.MoveNext ()) {
					builder.Append (SEPARATOR1);
					builder.Append (enumerator.Current.Key);
					builder.Append (SEPARATOR2);
					builder.Append (enumerator.Current.Value.ToString ());
				}
				Proxy.LogEvent (name, builder.ToString ());
			}
		}
	}
}
