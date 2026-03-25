using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Models;
using kulonut_Mobil.Pages;
using kulonut_Mobil.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	public partial class AppShellViewModel : UserViewModelBase
	{
		private IAuthService authService;

		[ObservableProperty]
		private bool flyoutIsOpen;

		public AppShellViewModel(IAuthService _authService, IUserService userService) : base(userService)
		{
			authService = _authService;
		}


		[RelayCommand]
		private async Task Logout()
		{
			await authService.LogoutAsync();
			userService.ClearCurrentUser();
			FlyoutIsOpen = false;
			await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
		}
		public override bool OnBackButtonPressed()
		{
			return true;
		}
	}
}
