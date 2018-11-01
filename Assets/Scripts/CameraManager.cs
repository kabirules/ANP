using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeadMosquito.AndroidGoodies;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour {

	public Sprite image;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void OpenCamera() {
		var shouldGenerateThumbnails = false;

		// if image is larger it will be downscaled to the max size proportionally
		var imageResultSize = ImageResultSize.Max1024;
		AGCamera.TakePhoto(
			selectedImage =>
			{
				// Load received image into Texture2D
				var imageTexture2D = selectedImage.LoadTexture2D();
				string msg = string.Format("{0} was taken from camera with size {1}x{2}",
					selectedImage.DisplayName, imageTexture2D.width, imageTexture2D.height);
				AGUIMisc.ShowToast(msg);
				Debug.Log(msg);
				image = SpriteFromTex2D(imageTexture2D);

				// Clean up
				Resources.UnloadUnusedAssets();
			},
			error => AGUIMisc.ShowToast("Cancelled taking photo from camera: " + error), imageResultSize, shouldGenerateThumbnails);
	}


	static Sprite SpriteFromTex2D(Texture2D texture)
	{
		return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
	}

	public void MovePanels() {
		GameObject mainPanel = GameObject.Find("MainPanel");
		if (mainPanel.transform.position.y > 600f) {
			mainPanel.transform.position = new Vector3(mainPanel.transform.position.x,
												   33f,
												   mainPanel.transform.position.z);
		} else {
			mainPanel.transform.position = new Vector3(mainPanel.transform.position.x,
												   603f,
												   mainPanel.transform.position.z);
												   // 615.0938f,
		}
		/*
		GameObject cameraObj = GameObject.Find("Camera");
		Camera camera = cameraObj.GetComponent<Camera>();
		camera.rect = new Rect(0.05f, camera.rect.y*-1f, 0.9f, 1f);
		*/
	}

	public void AddScriptRain() {
		this.ClearFilters();
		gameObject.AddComponent<CameraFilterPack_Atmosphere_Rain_Pro_3D>();
	}

	public void AddScriptSnow() {
		this.ClearFilters();
		gameObject.AddComponent<CameraFilterPack_Atmosphere_Snow_8bits>();
	}

	public void AddScriptFog() {
		this.ClearFilters();
		gameObject.AddComponent<CameraFilterPack_3D_Fog_Smoke>();
	}

	public void AddScriptBlizzard() {
		this.ClearFilters();
		gameObject.AddComponent<CameraFilterPack_Blizzard>();
	}	

	public void ClearFilters() {
		Destroy(gameObject.GetComponent<CameraFilterPack_Atmosphere_Snow_8bits>());
		Destroy(gameObject.GetComponent<CameraFilterPack_Atmosphere_Rain_Pro_3D>());
		Destroy(gameObject.GetComponent<CameraFilterPack_3D_Fog_Smoke>());
		Destroy(gameObject.GetComponent<CameraFilterPack_Blizzard>());
	}

	public void More() {
		Debug.Log("More Button Pressed");
	}

	

	

}
