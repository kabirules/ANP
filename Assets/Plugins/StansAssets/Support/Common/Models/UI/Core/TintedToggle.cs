using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace SA.Common.Models.UI.Core {

	[ExecuteInEditMode]
	[RequireComponent(typeof(Toggle))]
	public class TintedToggle : MonoBehaviour {

		[SerializeField]
		private Toggle ToggleButton;

		[SerializeField]
		private Text Label;

		[SerializeField]
		private Color TintColor = Color.white;

		[SerializeField]
		private Color Color;

		void Awake() {

		}

		// Use this for initialization
		void Start () {
			Label = GetComponentInChildren<Text> ();

			ToggleButton = GetComponent<Toggle> ();
			ToggleButton.onValueChanged.AddListener ((b) => { Label.color = b ? this.Color : TintColor; });

			Label.color = ToggleButton.isOn ? this.Color : this.TintColor;
		}
	}
}