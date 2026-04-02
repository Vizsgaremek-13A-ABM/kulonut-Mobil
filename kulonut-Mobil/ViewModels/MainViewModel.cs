using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.API;
using kulonut_Mobil.Models;
using kulonut_Mobil.Models.DTOs;
using kulonut_Mobil.Pages;
using kulonut_Mobil.Services;
using kulonut_Mobil.Validation;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
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
		private async Task BiometricAuth()
		{
			bool is_available = await CrossFingerprint.Current.IsAvailableAsync(true);
			if (is_available)
			{
				AuthenticationRequestConfiguration request = new AuthenticationRequestConfiguration("Biometrikus azonosítás", "Kérjük azonosítsa magát.");
				FingerprintAuthenticationResult result = await CrossFingerprint.Current.AuthenticateAsync(request);
				if (result.Authenticated)
				{

					UserModel? user = await userService.GetCurrentUserFromStorageAsync();
					if (user != null)
					{
						await Login();
						return;
					}
					await popupService.ShowErrorAsync("Nem található a megadott felhasználó");
				}
			}
			else
				await popupService.ShowErrorAsync("A biometrikus azonosítás nem elérhető a készülékén!");
		}

		[RelayCommand]
		private async Task HandleRemember()
		{
			UserModel? user = await userService.GetCurrentUserFromStorageAsync();
			string? token = await authService.GetTokenAsync();
			if (token == null) return; 
			authService.SetToken(token);
			Debug.WriteLine(token);
			if(authService.IsAuthenticated())
			{
				if (user == null)
					user = await userService.GetCurrentUserAsync();

				if(user == null)
				{
					await popupService.ShowErrorAsync("Nem található a megadott felhasználóasdasd");
					return;
				}
				else if (user.role == null)
				{
					await Shell.Current.GoToAsync($"{nameof(RegisterConfirmationPage)}");
					return;
				}
				await SetUserAppshell(); 
				await Shell.Current.GoToAsync($"//{nameof(MapPage)}");
			}
			else
			{
				await popupService.ShowErrorAsync("Lejárt token");
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
			await SetUserAppshell();
			await Shell.Current.GoToAsync($"//{nameof(MapPage)}");
		}
		private async Task SetUserAppshell()
		{
			if (Shell.Current is AppShell shell && shell.BindingContext is AppShellViewModel appVm)
			{
				try
				{
					await appVm.HandleUserLoadCommand.ExecuteAsync(null);
				}
				catch
				{
					appVm.HandleUserLoadCommand.Execute(null);
				}
			}
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
