using kulonut_Mobil.Pages;

namespace kulonut_Mobil.ViewModels
{
	public class RegisterConfirmationViewModel : BaseViewModel
	{
		public override bool OnBackButtonPressed()
		{
			MainThread.BeginInvokeOnMainThread(async () =>
			{
				await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
			});
			return true;
		}
	}
}
