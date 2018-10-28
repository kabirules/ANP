using UnityEngine;
using System.Collections;

public static class AndroidAdMob {
	private static GoogleMobileAdInterface _Client = null;
	public static GoogleMobileAdInterface Client{
		get {
			if(_Client == null) {
				_Client = AndroidAdMobController.Instance;
			}

			return _Client;
		}
		
	}
}
