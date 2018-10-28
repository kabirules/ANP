
#if UNITY_ANDROID
namespace AndroidGoodiesExamples
{
	using DeadMosquito.AndroidGoodies;
	using UnityEngine;

	public class AlarmClockTest : MonoBehaviour
	{
		public void OnShowAllAlarms()
		{
			AGAlarmClock.ShowAllAlarms();
		}

		public void OnSetAlarm()
		{
			var weekdays = new[]
			{
				AGAlarmClock.AlarmDays.Monday,
				AGAlarmClock.AlarmDays.Tuesday,
				AGAlarmClock.AlarmDays.Wednesday,
				AGAlarmClock.AlarmDays.Thursday,
				AGAlarmClock.AlarmDays.Friday,
			};
			const bool vibrate = true; // vibrate when alarm goes off
			const bool skipUI = false; // don't skip the UI and show in the app to approve
			AGAlarmClock.SetAlarm(1, 1, "My morning alarm", weekdays, vibrate, skipUI);
		}

		public void OnSetTimer()
		{
			AGUIMisc.ShowToast("Timer running in background...");
			const bool skipUI = true; // skip the UI and start the timer immediately
			AGAlarmClock.SetTimer(5, "My awesome timer", skipUI);
		}
	}
}
#endif