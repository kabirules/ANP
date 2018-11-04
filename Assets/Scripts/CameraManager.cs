using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeadMosquito.AndroidGoodies;
using UnityEngine.UI;
using System;

public class CameraManager : MonoBehaviour {

	public GameObject imageUI;
	private Texture2D imageToGallery;
	public Sprite image; //TODO remove

	SpriteRenderer spriteRenderer;
	GameObject mainPanel;
	GameObject canvas;

	// Use this for initialization
	void Start () {
		this.spriteRenderer = imageUI.GetComponent<SpriteRenderer>();
		this.mainPanel = GameObject.Find("MainPanel");
		this.canvas = GameObject.Find("Canvas");
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void OpenCamera() {
		AGUIMisc.ShowToast("Opening Camera");
		var shouldGenerateThumbnails = false;

		// if image is larger it will be downscaled to the max size proportionally
		var imageResultSize = ImageResultSize.Max1024;
		AGCamera.TakePhoto(
			selectedImage =>
			{
				// Load received image into Texture2D
				var imageTexture2D = selectedImage.LoadTexture2D();
				this.imageToGallery = imageTexture2D;
				string msg = string.Format("{0} was taken from camera with size {1}x{2}",
					selectedImage.DisplayName, imageTexture2D.width, imageTexture2D.height);
				AGUIMisc.ShowToast(msg);
				Debug.Log(msg);
				image = SpriteFromTex2D(imageTexture2D); //TODO remove
				spriteRenderer.sprite = SpriteFromTex2D(imageTexture2D);

				// Clean up
				Resources.UnloadUnusedAssets();
			},
			error => AGUIMisc.ShowToast("Cancelled taking photo from camera: " + error), imageResultSize, shouldGenerateThumbnails);
	}


	static Sprite SpriteFromTex2D(Texture2D texture)
	{
		return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
	}

	public void MovePanelUp() {
		GameObject mainPanel = GameObject.Find("MainPanel");
		RectTransform rectTransform = mainPanel.GetComponent<RectTransform>();
		mainPanel.GetComponent<RectTransform>().position = new Vector3(rectTransform.position.x,
																	   rectTransform.position.y+20f,
																	   rectTransform.position.z);
	}

	public void MovePanelDown() {
		GameObject mainPanel = GameObject.Find("MainPanel");
		RectTransform rectTransform = mainPanel.GetComponent<RectTransform>();
		mainPanel.GetComponent<RectTransform>().position = new Vector3(rectTransform.position.x,
																	   rectTransform.position.y-20f,
																	   rectTransform.position.z);
	}
	
	public void Screenshot() {
		var imageTitle = "Screenshot-" + System.DateTime.Now.ToString("yy-MM-dd-hh-mm-ss");
		const string folderName = "com.javifont.camera";
		AGGallery.SaveImageToGallery(imageToGallery, imageTitle, folderName, ImageFormat.JPEG);
		AGUIMisc.ShowToast("Image saved in the gallery", AGUIMisc.ToastLength.Long);
	}

	public void More() {
		Debug.Log("More Button Pressed");
	}	

/////////////////////
// FILTERS - Begin //
/////////////////////
	public void AddScriptRain() {
		this.ClearFilters();
		// gameObject.AddComponent<CameraFilterPack_Atmosphere_Rain_Pro_3D>();
		gameObject.AddComponent<CameraFilterPack_AAA_Blood>();
		gameObject.AddComponent<CameraFilterPack_AAA_Blood_Hit>();
		gameObject.AddComponent<CameraFilterPack_AAA_Blood_Plus>();
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

/////////////////////
// FILTERS - End //
/////////////////////	

}
