using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FriendInfoPresenter : MonoBehaviour {

	[SerializeField]
	private Text Id;

	[SerializeField]
	private Text Name;

	[SerializeField]
	private Toggle HasIcon;

	[SerializeField]
	private Toggle HasImage;

	[SerializeField]
	private Image Avatar;

	private string playerId = "*****";
	private string playerName = "*****";
	private bool hasIcon = false;
	private bool hasImage = false;
	private Sprite avatar = null;

	// Use this for initialization
	void Start () {
		UpdateUi ();
	}

	public void SetInfo(string id, string name, bool icon, bool image, Texture2D srcImage) {
		this.playerId = id;
		this.playerName = name;
		this.hasIcon = icon;
		this.hasImage = image;
		if (this.avatar == null) {
			this.avatar = Sprite.Create(srcImage, new Rect(0.0f, 0.0f, srcImage.width, srcImage.height), new Vector2(0.5f, 0.5f));
		}
		UpdateUi ();
	}
	
	private void UpdateUi () {
		Id.text = playerId;
		Name.text = playerName;
		HasIcon.isOn = hasIcon;
		HasImage.isOn = hasImage;
		if (avatar != null)
			Avatar.sprite = avatar; 
	}
}
