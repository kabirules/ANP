using System;
using UnityEngine;

public class GP_RTM_Network_Package {

	private string _playerId;
	private byte[] _buffer;

	public GP_RTM_Network_Package(string player, string recievedData) {
		_playerId = player;

		Debug.Log("GOOGLE_PLAY_RESULT -> OnMatchDataRecieved " + recievedData);
		_buffer = Convert.FromBase64String(recievedData);
	}

	public string participantId {
		get { return _playerId; }
	}

	public byte[] buffer {
		get { return _buffer; }
	}
}
