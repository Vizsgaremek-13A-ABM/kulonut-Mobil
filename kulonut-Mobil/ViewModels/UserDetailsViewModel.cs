using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	[QueryProperty(nameof(NavigatedFrom), NAV_URL)]
	[QueryProperty(nameof(UserId), ID_URL)]
	public partial class UserDetailsViewModel : BaseViewModel
    {
		public const string ID_URL = "userId";
		public const string NAV_URL = "navigatedFrom";
		public string UserId { get; set; }
		public string NavigatedFrom { get; set; }

		[RelayCommand]
		private async Task UserSwipedRight()
		{
			await Shell.Current.GoToAsync($"//{NavigatedFrom}");
		}
		public override bool OnBackButtonPressed()
		{
			MainThread.BeginInvokeOnMainThread(async () =>
			{
				await Shell.Current.GoToAsync($"//{NavigatedFrom}");
			});
			return true;
		}
	
	}
}
