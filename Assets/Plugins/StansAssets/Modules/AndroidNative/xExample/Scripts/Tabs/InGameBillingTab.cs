using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SA.Common.Models.UI.Core;

namespace SA.AN.IN_GAME_BILLING {
	
	public class InGameBillingTab : FeatureTab {

		[SerializeField]
		private GameObject[] objectToEnbaleOnInit;

		[SerializeField]
		private Button[] initBoundButtons;
		
		public Text coinsLabel;
		public Text boostLabel;	
		
		void Awake() {
			GameBillingManagerExample.init();
		}
		
		void FixedUpdate() {
			coinsLabel.text = "Total Coins: " + GameDataExample.coins.ToString();
			
			if(GameDataExample.IsBoostPurchased) {
				boostLabel.text = "Boost Enabled";
			} else {
				boostLabel.text = "Boost Disabled";
			}
			
			if(GameBillingManagerExample.isInited) {
				foreach(GameObject o in objectToEnbaleOnInit) {
					o.SetActive(true);
				}

				foreach(Button btn in initBoundButtons) {
					btn.interactable = true;
				}
			} else {
				foreach(Button btn in initBoundButtons) {
					btn.interactable = false;
				}
			}
		}
		
		public void AddCoins () {
			GameBillingManagerExample.purchase(GameBillingManagerExample.COINS_ITEM);
		}
		
		public void Boost () {
			GameBillingManagerExample.purchase(GameBillingManagerExample.COINS_BOOST);
		}
	}
}
