
#if UNITY_ANDROID
namespace AndroidGoodiesExamples
{
	using System.Collections;
	using DeadMosquito.AndroidGoodies;
	using UnityEngine;

	public class AlertDialogTest : MonoBehaviour
	{
		static readonly string[] Colors = {"Red", "Green", "Blue"};

		#region message_dialog

		public void OnMessageSingleButtonDialog()
		{
			AGAlertDialog.ShowMessageDialog("Single Button", "This dialog has only positive button", "Ok",
				() => AGUIMisc.ShowToast("Positive button Click"), () => AGUIMisc.ShowToast("Dismissed"),
				AGDialogTheme.Dark);
		}

		public void OnMessageTwoButtonDialog()
		{
			AGAlertDialog.ShowMessageDialog("Two Buttons", "This dialog has positive and negative button",
				"Ok", () => AGUIMisc.ShowToast("Positive button Click"),
				"No!", () => AGUIMisc.ShowToast("Negative button Click"),
				() => AGUIMisc.ShowToast("Dismissed"), AGDialogTheme.Dark);
		}

		public void OnMessageThreeButtonDialog()
		{
			AGAlertDialog.ShowMessageDialog("Three Buttons",
				"This dialog has positive, negative and neutral buttons button",
				"Ok", () => AGUIMisc.ShowToast("Positive button Click"),
				"No!", () => AGUIMisc.ShowToast("Negative button Click"),
				"Maybe!", () => AGUIMisc.ShowToast("Neutral button Click"),
				() => AGUIMisc.ShowToast("Dismissed"), AGDialogTheme.Light);
		}

		public void OnShowDialogChooseItem()
		{
			AGAlertDialog.ShowChooserDialog("Choose color", Colors,
				colorIndex => AGUIMisc.ShowToast(Colors[colorIndex] + " selected"), AGDialogTheme.Light);
		}

		#endregion

		public void OnShowDialogSingleChooseItem()
		{
			const int defaultSelectedItemIndex = 1;
			AGAlertDialog.ShowSingleItemChoiceDialog("Choose color", Colors, defaultSelectedItemIndex,
				colorIndex => AGUIMisc.ShowToast(Colors[colorIndex] + " selected"),
				"OK", () => AGUIMisc.ShowToast("OK!"), AGDialogTheme.Dark);
		}

		public void OnShowDialogMultiChooseItem()
		{
			bool[] initiallyCheckedItems = {false, true, false}; // second item is selected when dialog is shown
			AGAlertDialog.ShowMultiItemChoiceDialog("Choose color", Colors,
				initiallyCheckedItems,
				(colorIndex, isChecked) => AGUIMisc.ShowToast(Colors[colorIndex] + " selected? " + isChecked), "OK",
				() => AGUIMisc.ShowToast("OK!"), AGDialogTheme.Light);
		}

		#region progress_dialogs

		public void ShowSpinnerProgressDialog()
		{
			StartCoroutine(ShowSpinnerForDuration());
		}

		IEnumerator ShowSpinnerForDuration()
		{
			// Create spinner dialog
			var spinner = AGProgressDialog.CreateSpinnerDialog("Spinner", "Call Dismiss() to cancel", AGDialogTheme.Dark);
			spinner.Show();
			// Spin for some time (do important work)
			yield return new WaitForSeconds(3.0f);
			// Dismiss spinner after all the background work is done
			spinner.Dismiss();
		}

		public void ShowHorizontalProgressDialog()
		{
			StartCoroutine(ShowHorizontalForDuration());
		}

		IEnumerator ShowHorizontalForDuration()
		{
			const int dialogMaxProgress = 200;

			// Create progress bar dialog
			var progressBar = AGProgressDialog.CreateHorizontalDialog("Progress bar", "Call Dismiss() to cancel", dialogMaxProgress);
			progressBar.Show();

			const float progressDuration = 3.0f;
			float currentProgress = 0f;

			// Display incremental progress
			while (currentProgress < progressDuration)
			{
				currentProgress += Time.deltaTime;
				int progress = Mathf.CeilToInt((currentProgress / progressDuration) * dialogMaxProgress);
				progressBar.SetProgress(progress);
				yield return null;
			}

			// Dismiss spinner after all the background work is done
			progressBar.Dismiss();
		}

		#endregion
	}
}
#endif