using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace SA.Common.Models.UI.Core {

	[ExecuteInEditMode]
	[RequireComponent(typeof(Toggle))]
	public class FeatureSelector : MonoBehaviour {

		[SerializeField]
		private Toggle Selector;

		[SerializeField]
		private FeatureTab Tab;

		// Use this for initialization
		void Start () {
			Selector = GetComponent<Toggle> ();
			Selector.onValueChanged.AddListener ((b) => {
				if (b) {
					Tab.Show();
				} else {
					Tab.Hide();
				}
			});
		}
	}
}
