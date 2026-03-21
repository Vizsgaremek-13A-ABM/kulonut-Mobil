using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	public partial class PasswordChangeViewModel : BaseViewModel
	{
		[RelayCommand]
		private async Task ForgotPassword()
		{
			await Shell.Current.GoToAsync($"{nameof(MainPage)}");
		}
		public override bool OnBackButtonPressed()
		{
			MainThread.BeginInvokeOnMainThread(async () =>
			{
				await Shell.Current.GoToAsync($"{nameof(UserDetailsPage)}");
			});
			return true;
		}
	}
}
