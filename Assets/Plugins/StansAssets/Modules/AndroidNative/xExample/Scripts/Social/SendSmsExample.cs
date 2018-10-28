using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendSmsExample : MonoBehaviour {

	[SerializeField]
	Text smsBody;

	[SerializeField]
	Text reciever;

	public void SendSMS(){
		string body = smsBody.text;
		string recieverPhone = reciever.text;
		if (body.Length > 0 && recieverPhone.Length == 10) {
			AndroidSocialGate.SendTextMessage (body, recieverPhone);
		}
	}

}
