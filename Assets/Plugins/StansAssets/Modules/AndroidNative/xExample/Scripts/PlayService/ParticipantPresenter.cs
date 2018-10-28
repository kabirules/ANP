using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ParticipantPresenter : MonoBehaviour {

	public Image avatar;
	public Text id;
	public Text status;
	public Text playerId;
	public Text playerName;

	private string _id = string.Empty;

	private Sprite defaulttexture;
	private Sprite icon;
	
	void Awake() {
		defaulttexture = avatar.sprite;
	}
	
	public void SetParticipant(GP_Participant p) {		
		id.text = "";
		playerId.text = "";
		playerName.text = "";
		status.text = GP_RTM_ParticipantStatus.STATUS_UNRESPONSIVE.ToString();
		
		GooglePlayerTemplate player = GooglePlayManager.Instance.GetPlayerById(p.playerId);
		if(player != null) {
			playerId.text = "Player Id: " + p.playerId;
			playerName.text = "Name: " + player.name;
			
			if(player.icon != null) {
				if (!_id.Equals(p.playerId)) {
					icon = Sprite.Create(player.icon, new Rect(0.0f, 0.0f, player.icon.width, player.icon.height), new Vector2(0.5f, 0.5f));
					avatar.sprite = icon;
					_id = p.playerId;
				}
			}			
		} else {
			avatar.sprite = defaulttexture;
			icon = null;
			_id = string.Empty;
		}
		id.text  = "ID: " +  p.id;
		status.text = "Status: " + p.Status.ToString();		
	}
}
