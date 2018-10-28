
#if UNITY_ANDROID
namespace AndroidGoodiesExamples
{
	using DeadMosquito.AndroidGoodies;
	using UnityEngine;

	public class DialTest : MonoBehaviour
	{
		const string PhoneNumber = "123456789";

		public void OnShowDialer()
		{
			AGDialer.OpenDialer(PhoneNumber);
		}

		public void OnPlaceCall()
		{
			AGDialer.PlacePhoneCall(PhoneNumber);
		}
	}
}
#endif