using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.AndroidNative.DynamicLinks {
	public class Manager : SA.Common.Pattern.Singleton<Manager> {

		public Action<ShortLinkResult> OnShortLinkReceived = delegate {};

		void Awake() {
			DontDestroyOnLoad (gameObject);
		}

		public string GetPendingDynamicLink() {
			return Proxy.GetPendingLink ();
		}

		public void RequestShortDynamicLink(Link link) {
			Proxy.RequestLink (SA.Common.Data.Json.Serialize (link.Serialize()));
		}

		public void ShortLinkReceived(string data) {
			string[] bundle = data.Split (new string[] { global::AndroidNative.DATA_SPLITTER }, StringSplitOptions.None);
			int code = Int32.Parse (bundle [0]);

			ShortLinkResult result = code == 0 ? new ShortLinkResult (bundle [1]) : new ShortLinkResult (new SA.Common.Models.Error(bundle [1]));
			OnShortLinkReceived (result);
		}
	}
}
