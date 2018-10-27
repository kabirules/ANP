using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PermissionsAPIExample : MonoBehaviour {


	void Awake() {
		PermissionsManager.ActionPermissionsRequestCompleted += HandleActionPermissionsRequestCompleted;
	}



	public void CheckPermission() {
		Debug.Log("CheckPermission");

		bool val = PermissionsManager.IsPermissionGranted(AN_Permission.WRITE_EXTERNAL_STORAGE);
		Debug.Log(val);


		val = PermissionsManager.IsPermissionGranted(AN_Permission.INTERNET);
		Debug.Log(val);

        CheckShouldRequestPermission();
	}

	public void RequestPermission() {
        PermissionsManager.Instance.RequestPermissions(AN_Permission.WRITE_EXTERNAL_STORAGE, AN_Permission.CAMERA);
	}

    public void CheckShouldRequestPermission() {
        bool result = PermissionsManager.ShouldShowRequestPermission(AN_Permission.WRITE_EXTERNAL_STORAGE);
        Debug.Log("CheckShouldRequestPermission: " + result.ToString());
    }

	void HandleActionPermissionsRequestCompleted (AN_GrantPermissionsResult res) {
		Debug.Log("HandleActionPermissionsRequestCompleted");
		
		foreach(KeyValuePair<AN_Permission, AN_PermissionState> pair in res.RequestedPermissionsState) {
			Debug.Log(pair.Key.GetFullName() + " / " + pair.Value.ToString());
		}
		
	}
}
