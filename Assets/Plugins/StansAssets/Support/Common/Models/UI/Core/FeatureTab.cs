using UnityEngine;
using System.Collections;

namespace SA.Common.Models.UI.Core {

	public class FeatureTab : MonoBehaviour {

		public void Show() {
			gameObject.SetActive (true);
		}

		public void Hide() {
			gameObject.SetActive (false);
		}
	}
}
