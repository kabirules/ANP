using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.AndroidNative.DynamicLinks {
	public class ShortLinkResult : SA.Common.Models.Result {

		private string shortLink = string.Empty;

		public ShortLinkResult(SA.Common.Models.Error error) : base (error) {}

		public ShortLinkResult(string link) : base() {
			shortLink = link;
		}

		public string ShortLink {
			get {
				return shortLink;
			}
		}
	}
}
