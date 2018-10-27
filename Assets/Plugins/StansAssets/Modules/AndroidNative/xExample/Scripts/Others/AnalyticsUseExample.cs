////////////////////////////////////////////////////////////////////////////////
//  
// @module Android Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////


using UnityEngine;
using System.Collections;

public class AnalyticsUseExample : MonoBehaviour {


	void Awake () {

		//Staring Analytics
		//It will be started with ID spesifayed in xml
		AndroidGoogleAnalytics.Instance.StartTracking();

		//If you want ot change default tracking id use this function after StartTracking:
	//	AndroidGoogleAnalytics.instance.SetTrackerID("UA-55040152-1");
	}


	void Start() {
		//Tracking first screen
		AndroidGoogleAnalytics.Instance.SendView("Home Screen");

		//Send event example + 1 more implementation
		AndroidGoogleAnalytics.Instance.SendEvent("Category", "Action", "label");
		//Send event example with addition values + 1 more implementation
		AndroidGoogleAnalytics.Instance.SendEvent("Category", "Action", "label", 100, "screen", "main");

		//Send timing event + 2 more implementation 
		AndroidGoogleAnalytics.Instance.SendTiming("App Started", (long) Time.time);

		//Set session key
		AndroidGoogleAnalytics.Instance.SetKey("SCREEN", "MAIN");

		AndroidGoogleAnalytics.Instance.EnableAdvertisingIdCollection(true);


		//To remove session key use
		//AndroidGoogleAnalytics.instance.ClearKey("SCREEN");


		//To Chnage login level use
		//AndroidGoogleAnalytics.instance.SetLogLevel(AndroidLogLevel.VERBOSE);

		//To disable data sending use
		//AndroidGoogleAnalytics.instance.SetDryRun(true);





		PurchaseTackingExample();
	}

	public void PurchaseTackingExample() {
		AndroidGoogleAnalytics.Instance.CreateTransaction("0_123456", "In-app Store", 2.1f, 0.17f, 0f, "USD");
		AndroidGoogleAnalytics.Instance.CreateItem("0_123456", "Level Pack: Space", "L_789", "Game expansions", 1.99f, 1, "USD");

	}
	

}
