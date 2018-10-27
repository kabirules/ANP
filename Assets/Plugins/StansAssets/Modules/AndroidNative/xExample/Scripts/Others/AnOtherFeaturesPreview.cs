using UnityEngine;
using System.Collections;

public class AnOtherFeaturesPreview : MonoBehaviour {

	public GameObject image;
	public Texture2D helloWorldTexture;

	void Start() {
		LoadNetworkInfo();
	}

	public void SaveToGalalry() {
		AndroidCamera.Instance.OnImageSaved += OnImageSaved;
		AndroidCamera.Instance.SaveImageToGallery(helloWorldTexture, "Screenshot" + AndroidCamera.GetRandomString());

	}


	public void SaveScreenshot() {
		AndroidCamera.Instance.OnImageSaved += OnImageSaved;
		AndroidCamera.Instance.SaveScreenshotToGallery("Screenshot" + AndroidCamera.GetRandomString());

	}


	public void GetImageFromGallery() {
		AndroidCamera.Instance.OnImagePicked += OnImagePicked;
		AndroidCamera.Instance.GetImageFromGallery();
	}
	
	
	
	public void GetImageFromCamera() {
		AndroidCamera.Instance.OnImagePicked += OnImagePicked;
		AndroidCamera.Instance.GetImageFromCamera();
	}


	public void CheckForTV() {
		TVAppController.DeviceTypeChecked += OnDeviceTypeChecked;
		TVAppController.Instance.CheckForATVDevice();
	}


	public void LoadNetworkInfo() {
		AndroidNativeUtility.ActionNetworkInfoLoaded += HandleActionNetworkInfoLoaded;
		AndroidNativeUtility.Instance.LoadNetworkInfo();
	}

	void HandleActionNetworkInfoLoaded (AN_NetworkInfo info) {
		string infoString = "";
		infoString += "IpAddress: " + info.IpAddress + " \n";
		infoString += "SubnetMask: " + info.SubnetMask + " \n";
		infoString += "MacAddress: " + info.MacAddress + " \n";
		infoString += "SSID: " + info.SSID + " \n";
		infoString += "BSSID: " + info.BSSID + " \n";

		infoString += "LinkSpeed: " + info.LinkSpeed + " \n";
		infoString += "NetworkId: " + info.NetworkId + " \n";

		Debug.Log(infoString);

		AndroidNativeUtility.ActionNetworkInfoLoaded -= HandleActionNetworkInfoLoaded;
	}


	public void CheckAppInstalation() {

		AndroidNativeUtility.OnPackageCheckResult += OnPackageCheckResult;
		AndroidNativeUtility.Instance.CheckIsPackageInstalled("com.google.android.youtube");
	}


	public void RunApp() {
		AndroidNativeUtility.OpenSettingsPage(AN_SettingsActivityAction.ACTION_APPLICATION_DETAILS_SETTINGS);
		//AndroidNativeUtility.instance.RunPackage("com.google.android.youtube");
	}


	public void CheckAppLicense() {

		AN_LicenseManager.OnLicenseRequestResult += LicenseRequestResult;
		AN_LicenseManager.Instance.StartLicenseRequest (AndroidNativeSettings.Instance.base64EncodedPublicKey);
		SA_StatusBar.text =  "Get App License Request STARTED";
	}


	private void LicenseRequestResult(AN_LicenseRequestResult result) {
		SA_StatusBar.text =  "App License Status: " + result.ToString();
		AndroidMessage.Create("License Check Result: ", "AN_LicenseRequestResult: " +  result.ToString());
	}


	private void EnableImmersiveMode() {
		ImmersiveMode.Instance.EnableImmersiveMode();
	}
	

	public void GetAndroidId() {
		AndroidNativeUtility.OnAndroidIdLoaded += OnAndroidIdLoaded;
		AndroidNativeUtility.Instance.LoadAndroidId();
	}
	
	void OnAndroidIdLoaded (string id) {
		AndroidNativeUtility.OnAndroidIdLoaded -= OnAndroidIdLoaded;
		AndroidMessage.Create("Android Id Loaded", id);
	}

	private void LoadAppInfo() {

		AndroidAppInfoLoader.ActionPacakgeInfoLoaded += OnPackageInfoLoaded; 
		AndroidAppInfoLoader.Instance.LoadPackageInfo ();
	}


	private void LoadAdressBook() {
		AddressBookController.Instance.LoadContacts ();
		AddressBookController.OnContactsLoadedAction += OnContactsLoaded;

	}



	void OnDeviceTypeChecked () {
		AN_PoupsProxy.showMessage("Check for a TV Device Result" , TVAppController.Instance.IsRuningOnTVDevice.ToString());
		TVAppController.DeviceTypeChecked -= OnDeviceTypeChecked;
	}



	void OnPackageCheckResult (AN_PackageCheckResult res) {
		if(res.IsSucceeded) {
			AN_PoupsProxy.showMessage("On Package Check Result" , "Application  " + res.packageName + " is installed on this device");
		} else {
			AN_PoupsProxy.showMessage("On Package Check Result" , "Application  " + res.packageName + " is not installed on this device");
		}

		AndroidNativeUtility.OnPackageCheckResult -= OnPackageCheckResult;
	}



	void OnContactsLoaded () {
		AddressBookController.OnContactsLoadedAction -= OnContactsLoaded;
		AN_PoupsProxy.showMessage("On Contacts Loaded" , "Andress book has " + AddressBookController.Instance.contacts.Count + " Contacts");
	}
	

	private void OnImagePicked(AndroidImagePickResult result) {
		Debug.Log("OnImagePicked");
		if (result.IsSucceeded) {
			AN_PoupsProxy.showMessage ("Image Pick Rsult", "Succeeded, path: " + result.ImagePath);
			image.GetComponent<Renderer> ().material.mainTexture = result.Image;
		} else {
			AN_PoupsProxy.showMessage ("Image Pick Rsult", "Failed");
		}

		AndroidCamera.Instance.OnImagePicked -= OnImagePicked;
	}

	void OnImageSaved (GallerySaveResult result) {

		AndroidCamera.Instance.OnImageSaved -= OnImageSaved;

		if(result.IsSucceeded) {
			AN_PoupsProxy.showMessage("Saved", "Image saved to gallery \n" + "Path: " + result.imagePath);
			SA_StatusBar.text =  "Image saved to gallery";
		} else {
			AN_PoupsProxy.showMessage("Failed", "Image save to gallery failed");
			SA_StatusBar.text =  "Image save to gallery failed";
		}



	}

	private void OnPackageInfoLoaded(PackageAppInfo PacakgeInfo) {
		AndroidAppInfoLoader.ActionPacakgeInfoLoaded -= OnPackageInfoLoaded; 

		string msg = "";
		msg += "versionName: " + PacakgeInfo.versionName + "\n";
		msg += "versionCode: " + PacakgeInfo.versionCode + "\n";
		msg += "packageName" + PacakgeInfo.packageName + "\n";
		msg += "lastUpdateTime:" + System.Convert.ToString(PacakgeInfo.lastUpdateTime) + "\n";
		msg += "sharedUserId" + PacakgeInfo.sharedUserId + "\n";
		msg += "sharedUserLabel"  + PacakgeInfo.sharedUserLabel;

		AN_PoupsProxy.showMessage("App Info Loaded", msg);
	}




	public void LoadInternal() {
		AndroidNativeUtility.InternalStoragePathLoaded += InternalStoragePathLoaded;
		AndroidNativeUtility.Instance.GetInternalStoragePath();

	}

	public void LoadExternal() {
		AndroidNativeUtility.ExternalStoragePathLoaded += ExternalStoragePathLoaded;
		AndroidNativeUtility.Instance.GetExternalStoragePath();
	}

	public void LoadLocaleInfo() {
		AndroidNativeUtility.LocaleInfoLoaded += LocaleInfoLoaded;
		AndroidNativeUtility.Instance.LoadLocaleInfo();
	}

	void LocaleInfoLoaded (AN_Locale locale){
		AN_PoupsProxy.showMessage("Locale Indo:", locale.CountryCode + "/" 
		                          + locale.DisplayCountry + "  :   "
		                          + locale.LanguageCode + "/" 
		                          + locale.DisplayLanguage);
		AndroidNativeUtility.LocaleInfoLoaded -= LocaleInfoLoaded;
	}


	void ExternalStoragePathLoaded (string path) {
		AN_PoupsProxy.showMessage("External Storage Path:", path);
		AndroidNativeUtility.ExternalStoragePathLoaded -= ExternalStoragePathLoaded;
	}

	void InternalStoragePathLoaded (string path) {
		AN_PoupsProxy.showMessage("Internal Storage Path:", path);
		AndroidNativeUtility.InternalStoragePathLoaded -= InternalStoragePathLoaded;
	}
}
