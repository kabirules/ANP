using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.AndroidNative.DynamicLinks {
	public class Link {
		private string url = string.Empty;
		private string domain = string.Empty;

		private AndroidParameters androidParams;
		private IosParameters iosParams;
		private GoogleAnalyticsParameters googleAnalyticsParams;
		private ItunesConnectAnalyticsParameters itunesAnalyticsParams;
		private SocialMetaTagParameters socialMetaTagParams;

		private Link() {}

		public Dictionary<string, object> Serialize() {
			Dictionary<string, object> data = new Dictionary<string, object> ();
			data.Add ("link", url);
			data.Add ("domain", domain);

			if (androidParams != null)
				data.Add ("android_params", androidParams.Serialize());

			if (iosParams != null)
				data.Add ("ios_params", iosParams.Serialize());

			if (googleAnalyticsParams != null)
				data.Add ("google_params", googleAnalyticsParams.Serialize());

			if (itunesAnalyticsParams != null)
				data.Add ("itunes_params", itunesAnalyticsParams.Serialize());

			if (socialMetaTagParams != null)
				data.Add ("social_params", socialMetaTagParams.Serialize());

			return data;
		}

		public class SocialMetaTagParameters {
			private string title = string.Empty;
			private string description = string.Empty;
			private string imageUrl = string.Empty;

			private SocialMetaTagParameters() {}

			public Dictionary<string, object> Serialize() {
				Dictionary<string, object> data = new Dictionary<string, object> ();
				data.Add ("title", title);
				data.Add ("description", description);
				data.Add ("image_url", imageUrl);

				return data;
			}

			public class Builder {
				private SocialMetaTagParameters param;

				public Builder () {
					param = new SocialMetaTagParameters();
				}

				public Builder SetTitle(string title) {
					param.title = title;
					return this;
				}

				public Builder SetDescription (string description) {
					param.description = description;
					return this;
				}

				public Builder SetImageUrl (string url) {
					param.imageUrl = url;
					return this;
				}

				public SocialMetaTagParameters Build () {
					return param;
				}
			}

			public string Title {
				get {
					return title;
				}
			}

			public string Description {
				get {
					return description;
				}
			}

			public string ImageUrl {
				get {
					return imageUrl;
				}
			}

		}

		public class ItunesConnectAnalyticsParameters {
			private string providerToken = string.Empty;
			private string affiliateToken = string.Empty;
			private string campaignToken = string.Empty;

			private ItunesConnectAnalyticsParameters () {}

			public Dictionary<string, object> Serialize() {
				Dictionary<string, object> data = new Dictionary<string, object> ();
				data.Add ("provider_token", providerToken);
				data.Add ("affiliate_token", affiliateToken);
				data.Add ("campaign_token", campaignToken);

				return data;
			}

			public class Builder {
				private ItunesConnectAnalyticsParameters param;

				public Builder() {
					param = new ItunesConnectAnalyticsParameters();
				}

				public Builder SetProviderToken(string token) {
					param.providerToken = token;
					return this;
				}

				public Builder SetAffiliateToken(string token) {
					param.affiliateToken = token;
					return this;				
				}

				public Builder SetCampaignToken(string token) {
					param.campaignToken = token;
					return this;
				}

				public ItunesConnectAnalyticsParameters Build() {
					return param;
				}
			}

			public string CampaignToken {
				get {
					return campaignToken;
				}
			}

			public string AffiliateToken {
				get {
					return affiliateToken;
				}
			}

			public string ProviderToken {
				get {
					return providerToken;
				}
			}

		}

		public class GoogleAnalyticsParameters {
			private string source = string.Empty;
			private string medium = string.Empty;
			private string campaign = string.Empty;
			private string term = string.Empty;
			private string content = string.Empty;

			private GoogleAnalyticsParameters() {}

			public Dictionary<string, object> Serialize() {
				Dictionary<string, object> data = new Dictionary<string, object> ();
				data.Add ("source", source);
				data.Add ("medium", medium);
				data.Add ("campaign", campaign);
				data.Add ("term", term);
				data.Add ("content", content);

				return data;
			}

			public class Builder {
				private GoogleAnalyticsParameters param;

				public Builder() {
					param = new GoogleAnalyticsParameters();
				}

				public Builder SetSource (string source) {
					param.source = source;
					return this;
				}

				public Builder SetMedium (string medium) {
					param.medium = medium;
					return this;
				}

				public Builder SetCampaign (string campaign) {
					param.campaign = campaign;
					return this;
				}

				public Builder SetTerm (string term) {
					param.term = term;
					return this;
				}

				public Builder SetContent (string content) {
					param.content = content;
					return this;
				}

				public GoogleAnalyticsParameters Build () {
					return param;
				}
			}

			public string Content {
				get {
					return content;
				}
			}

			public string Term {
				get {
					return term;
				}
			}

			public string Campaign {
				get {
					return campaign;
				}
			}

			public string Medium {
				get {
					return medium;
				}
			}

			public string Source {
				get {
					return source;
				}
			}

		}

		public class IosParameters {
			private string appBundle =string.Empty;
			private string appStoreId = string.Empty;
			private string minimumVersion = string.Empty;
			private string ipadBundleId = string.Empty;
			private string ipadFallbackUrl = string.Empty;
			private string fallbackUrl = string.Empty;
			private string customScheme = string.Empty;

			private IosParameters() {}

			public Dictionary<string, object> Serialize() {
				Dictionary<string, object> data = new Dictionary<string, object> ();
				data.Add ("app_bundle", appBundle);
				data.Add ("appstore_id", appStoreId);
				data.Add ("minimum_version", minimumVersion);
				data.Add ("ipad_bundle_id", ipadBundleId);
				data.Add ("ipad_fallback_id", ipadFallbackUrl);
				data.Add ("fallback_url", fallbackUrl);
				data.Add ("custom_scheme", customScheme);

				return data;
			}

			public class Builder {
				private IosParameters param;

				public Builder(string appBundle) {
					param = new IosParameters();
					param.appBundle = appBundle;
				}

				public Builder SetFallbackUrl(string url) {
					param.fallbackUrl = url;
					return this;
				}

				public Builder SetCustomScheme(string scheme) {
					param.customScheme = scheme;
					return this;
				}

				public Builder SetIpadFallbackUrl (string ipadFallbackUrl) {
					param.ipadFallbackUrl = ipadFallbackUrl;
					return this;
				}

				public Builder SetIpadBundleId (string ipadBundleId) {
					param.ipadBundleId = ipadBundleId;
					return this;
				}

				public Builder SetAppStoreId (string appStoreId) {
					param.appStoreId = appStoreId;
					return this;
				}

				public Builder SetMinimumVersion(string minVersion) {
					param.minimumVersion = minVersion;
					return this;
				}

				public IosParameters Build() {
					return param;
				}
			}

			public string AppStoreId {
				get {
					return appStoreId;
				}
			}

			public string AppBundle {
				get {
					return appBundle;
				}
			}

			public string MinimumVersion {
				get {
					return minimumVersion;
				}
			}

			public string IpadBundleId {
				get {
					return ipadBundleId;
				}
			}

			public string IpadFallbackUrl {
				get {
					return ipadFallbackUrl;
				}
			}

			public string FallbackUrl {
				get {
					return fallbackUrl;
				}
			}

			public string CustomScheme {
				get {
					return customScheme;
				}
			}

		}

		public class AndroidParameters {
			private string appBundle = string.Empty;
			private string fallbackUrl = string.Empty;
			private int minimumVersion = 1;

			private AndroidParameters() {}

			public Dictionary<string, object> Serialize() {
				Dictionary<string, object> data = new Dictionary<string, object> ();
				data.Add ("app_bundle", appBundle);
				data.Add ("fallback_url", fallbackUrl);
				data.Add ("minimum_version", minimumVersion);

				return data;
			}

			public class Builder {
				private AndroidParameters param;

				public Builder(string appBundle) {
					param = new AndroidParameters();
					param.appBundle = appBundle;
				}

				public Builder SetFallbackUrl (string url) {
					param.fallbackUrl = url;
					return this;
				}

				public Builder SetMinimumVersion(int minVersion) {
					param.minimumVersion = minVersion;
					return this;
				}

				public AndroidParameters Build() {
					return param;
				}
			}

			public string FallbackUrl {
				get {
					return fallbackUrl;
				}
			}

			public int MinimumVersion {
				get {
					return minimumVersion;
				}
			}

			public string AppBundle {
				get {
					return appBundle;
				}
			}

		}

		public class Builder {
			private Link link;

			public Builder() {
				link = new Link();
			}

			public Builder SetLink(string url) {
				link.url = url;
				return this;
			}

			public Builder SetDynamicLinkDomain(string domain) {
				link.domain = domain;
				return this;
			}

			public Builder SetAndroidParameters(AndroidParameters param) {
				link.androidParams = param;
				return this;
			}

			public Builder SetIosParameters (IosParameters param) {
				link.iosParams = param;
				return this;
			}

			public Builder SetGoogleAnalyticsParameters (GoogleAnalyticsParameters param) {
				link.googleAnalyticsParams = param;
				return this;
			}

			public Builder SetItunesConnectAnalyticsParameters (ItunesConnectAnalyticsParameters param) {
				link.itunesAnalyticsParams = param;
				return this;
			}

			public Builder SetSocialMetaTagParameters (SocialMetaTagParameters param) {
				link.socialMetaTagParams = param;
				return this;
			}

			public Link Build() {
				return link;
			}
		}

		public string Url {
			get {
				return url;
			}
		}

		public string Domain {
			get {
				return domain;
			}
		}

		public AndroidParameters AndroidParams {
			get {
				return androidParams;
			}
		}

		public IosParameters IosParams {
			get {
				return iosParams;
			}
		}

		public GoogleAnalyticsParameters GoogleAnalyticsParams {
			get {
				return googleAnalyticsParams;
			}
		}

		public ItunesConnectAnalyticsParameters ItunesAnalyticsParams {
			get {
				return itunesAnalyticsParams;
			}
		}

		public SocialMetaTagParameters SocialMetaTagParams {
			get {
				return socialMetaTagParams;
			}
		}

	}
}
