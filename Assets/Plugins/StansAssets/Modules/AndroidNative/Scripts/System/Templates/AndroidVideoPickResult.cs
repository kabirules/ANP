////////////////////////////////////////////////////////////////////////////////
//  
// @module Stan's Assets Android Native
// @author Alexey Yaremenko (Stan's Assets) 
// @support support@stansassets.com
//
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using UnityEngine;

public class AndroidVideoPickResult : AndroidActivityResult
{
	private string m_videoPath = string.Empty;

	public AndroidVideoPickResult(string resultCode, string videoData) : base("0", resultCode) {
		if (IsSucceeded) {
			m_videoPath = videoData;
		}
	}

	public string VideoPath {
		get {
			return m_videoPath;
		}
		set {
			m_videoPath = value;
		}
	}
}
