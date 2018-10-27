using UnityEngine;
using System.Collections;

public class SALoadedSceneOnClick : SAOnClickAction {

	public string levelName;

	protected override void OnClick() {
		SALevelLoader.Instance.LoadLevel(levelName);
	}
}
