using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Pages;
using kulonut_Mobil.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	[QueryProperty(nameof(NavigatedFrom), NAV_URL)]
	public partial class UserDetailsViewModel : UserViewModelBase
    {
		public const string NAV_URL = "navigatedFrom";
		public string? NavigatedFrom { get; set; }

		public UserDetailsViewModel(IUserService userService) : base(userService)
		{
			
		}
		

		[RelayCommand]
		private async Task UserSwipedRight()
		{
			await Shell.Current.GoToAsync($"//{NavigatedFrom}");
		}
		[RelayCommand]
		private async Task PasswordChange()
		{
			await Shell.Current.GoToAsync($"{nameof(PasswordChangePage)}");
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
