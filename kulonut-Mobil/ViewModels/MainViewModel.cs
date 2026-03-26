using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.API;
using kulonut_Mobil.Models;
using kulonut_Mobil.Models.DTOs;
using kulonut_Mobil.Pages;
using kulonut_Mobil.Services;
using kulonut_Mobil.Validation;
using System.Diagnostics;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	public partial class MainViewModel : BaseViewModel
	{

		[ObservableProperty]
		private LoginRequestDTO loginRequestDTO = new LoginRequestDTO();

		[ObservableProperty]
		private bool rememberLogin = false;

		private IAuthService authService;
		private IPopupService popupService;
		private IUserService userService;

		public MainViewModel(IAuthService _authService, IPopupService _popupService, IUserService _userService)
		{
			authService = _authService;
			popupService = _popupService;
			userService = _userService;
		}

		[RelayCommand]
		private async Task HandleRemember()
		{
			string? token = await authService.GetTokenAsync();
			if (token == null) return; 
			authService.SetToken(token);
			if(authService.IsAuthenticated())
			{
				//UserModel? user = await userService.GetCurrentUserAsync();
				//if (user == null)
				//{
				//	await popupService.ShowErrorAsync("Nem található a megadott felhasználó");
				//	return;
				//}
				//else if(user.role == null)
				//{
				//	await NavigateToRegister();
				//	return;
				//}
				await Shell.Current.GoToAsync($"//{nameof(MapPage)}");
			}
			else
			{
				await popupService.ShowErrorAsync("Lejárt token");
				//await authService.LogoutAsync();
			}
		}

		[RelayCommand]
		private async Task Login()
		{
			bool check_result = await InputCheck();
			if (!check_result) return;
			UserModel? response = await authService.LoginAsync(LoginRequestDTO, RememberLogin);
			if(response == null)
			{
				await popupService.ShowErrorAsync("Wrong Password or Email");
				return;
			}
			await userService.SetCurrentUser(response);
			await Shell.Current.GoToAsync($"//{nameof(MapPage)}");
		}
		[RelayCommand]
		private async Task NavigateToRegister()
		{
			await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
		}
		[RelayCommand]
		private async Task PasswordReset()
		{
			await Shell.Current.GoToAsync($"{nameof(PasswordResetPage)}?{PasswordResetViewModel.NAV_URL}={nameof(MainPage)}");
		}
		private async Task<bool> InputCheck()
		{
			string? email_valid = InputValidator.ValidateEmail(LoginRequestDTO.Email);
            if(email_valid != null)
			{
				await popupService.ShowErrorAsync(email_valid);
				return false;
			}
            string? user_password = InputValidator.ValidatePassword(LoginRequestDTO.Password);
            if (user_password != null)
            {
                await popupService.ShowErrorAsync(user_password);
                return false;
            }
            return true;
		}
	}
}
