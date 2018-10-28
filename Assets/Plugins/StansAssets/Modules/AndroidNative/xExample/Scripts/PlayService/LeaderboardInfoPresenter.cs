using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LeaderboardInfoPresenter : MonoBehaviour {

	[SerializeField]
	private Text Rank;

	[SerializeField]
	private Text Score;

	[SerializeField]
	private Text PlayerId;

	[SerializeField]
	private Text Name;

	[SerializeField]
	private Image Avatar;

	private string id = "*****";
	private string playerName = "*****";
	private string rank = "*****";
	private string score = "*****";

	private Sprite avatar;
	private Sprite old;

	void Start() {
		this.old = Avatar.sprite;
	}

	public void SetInfo(string rank, string score, string id, string name, Texture2D icon) {
		this.rank = rank;
		this.score = score;
		this.playerName = name;
		if (!this.id.Equals(id) && icon != null) {
			avatar = Sprite.Create(icon, new Rect(0.0f, 0.0f, icon.width, icon.height), new Vector2(0.5f, 0.5f));
		}
		this.id = id;
		UpdateUi ();
	}

	public void Disable() {
		this.rank = "*****";
		this.score = "*****";
		this.playerName = "*****";
		this.id = "*****";
		this.avatar = old;
		UpdateUi ();
	}

	private void UpdateUi() {
		Rank.text = this.rank;
		Score.text = this.score;
		PlayerId.text = this.id;
		Name.text = this.playerName;

		if (this.avatar != null) 
			Avatar.sprite = this.avatar;
		else
			Avatar.sprite = this.old;
	}

}
