using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SA.Common.Models.UI.Core;

namespace SA.AN.BILLING {
	
	public class BillingTab : FeatureTab {

		[SerializeField]
		private Button initButton;

		[SerializeField]
		private Button[] initBoundButtons;	
		
		public void Init() {
			GPaymnetManagerExample.init ();
		}
		
		void FixedUpdate() {		
			if(GPaymnetManagerExample.isInited) {			
				initButton.interactable = false;
				foreach(Button btn in initBoundButtons) {
					btn.interactable = true;
				}
			} else {
				initButton.interactable = true;
				foreach(Button btn in initBoundButtons) {
					btn.interactable = false;
				}
			}
		}	
		
		public void SuccsesPurchase() {
			if(GPaymnetManagerExample.isInited) {
				AndroidInAppPurchaseManager.Client.Purchase (GPaymnetManagerExample.ANDROID_TEST_PURCHASED);
			} else {
				AndroidMessage.Create("Error", "PaymnetManagerExample not yet inited");
			}		
		}	
		
		public void FailPurchase() {
			if(GPaymnetManagerExample.isInited) {
				AndroidInAppPurchaseManager.Client.Purchase (GPaymnetManagerExample.ANDROID_TEST_ITEM_UNAVAILABLE);
			} else {
				AndroidMessage.Create("Error", "PaymnetManagerExample not yet inited");
			}
		}	
		
		public void ConsumeProduct() {
			if(GPaymnetManagerExample.isInited) {
				if(AndroidInAppPurchaseManager.Client.Inventory.IsProductPurchased(GPaymnetManagerExample.ANDROID_TEST_PURCHASED)) {
					GPaymnetManagerExample.consume (GPaymnetManagerExample.ANDROID_TEST_PURCHASED);
				} else {
					AndroidMessage.Create("Error", "You do not own product to consume it");
				}
				
			} else {
				AndroidMessage.Create("Error", "PaymnetManagerExample not yet inited");
			}
		}
	}
}
