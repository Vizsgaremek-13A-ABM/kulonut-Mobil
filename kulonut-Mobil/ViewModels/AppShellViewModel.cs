using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Java.Lang;
using kulonut_Mobil.Models;
using kulonut_Mobil.Pages;
using kulonut_Mobil.Services;

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
			userService.CurrentUserChanged += OnCurrentUserChanged;
		}
		[RelayCommand]
		private async Task Logout()
		{
			await authService.Logout();
			FlyoutIsOpen = false;
			await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
		}
		private async void OnCurrentUserChanged(object? sender, EventArgs e)
		{
			await HandleUserLoad();
		}
		public override bool OnBackButtonPressed()
		{
			return true;
		}

	}
}
