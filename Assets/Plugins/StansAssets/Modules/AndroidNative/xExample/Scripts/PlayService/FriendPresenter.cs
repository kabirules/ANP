using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FriendPresenter : MonoBehaviour {

	private string _pId = string.Empty;
	
	public Image avatar;
	public Text playerId;
	public Text playerName;
	
	private Sprite defaulttexture;
	
	void Awake() {
		defaulttexture = avatar.sprite;
	}
	
	public void SetFriendId(string pId) {
		_pId = pId;
		
		playerId.text = "";
		playerName.text = "";
		
		avatar.sprite = defaulttexture;		
		
		GooglePlayerTemplate player = GooglePlayManager.Instance.GetPlayerById(pId);
		if(player != null) {
			playerId.text = "Player Id: " + _pId;
			playerName.text = "Name: " + player.name;
			
			if(player.icon != null) {
				avatar.GetComponent<Renderer>().material.mainTexture = player.icon;
			}			
		}		
	}
	
	public void PlayWithFried() {
		GooglePlayRTM.Instance.FindMatch(1, 1, _pId);
	}
	
	void FixedUpdate() {
		if(_pId != string.Empty) {
			SetFriendId(_pId);
		}
	}
}
