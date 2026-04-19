using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Models;
using kulonut_Mobil.Models.DTOs;
using kulonut_Mobil.Pages;
using kulonut_Mobil.Services;
using kulonut_Mobil.Validation;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System.Data;

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
					await Remember(true);
				else
					await popupService.ShowMessageAsync("Nem sikerült a biometrikus azonosítás", "Információ");
			}
			else
				await popupService.ShowMessageAsync("A biometrikus azonosítás nem elérhető a készülékén!");
		}

		[RelayCommand]
		private async Task HandleRemember()
		{
			LoginRequestDTO = new LoginRequestDTO();
			string? remember_str = await SecureStorage.GetAsync(AuthService.REMEMBER_KEY);
			if (remember_str == null || remember_str == "0")
				return;
			IsLoading = true;
			await Remember(false);
			IsLoading = false;
		}
		
		[RelayCommand]
		private async Task Login()
		{
			bool check_result = await InputCheck();
			if (!check_result) return;
			IsLoading = true;
			await HandleLogin();
			IsLoading = false;
		}
		
		[RelayCommand]
		private async Task NavigateToRegister()
		{
			await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
		}
		[RelayCommand]
		private async Task PasswordReset()
		{
			await Shell.Current.GoToAsync($"{nameof(PasswordResetPage)}");
		}
		private async Task Remember(bool isBiometric)
		{
			string? token = await authService.GetToken();
			if (string.IsNullOrEmpty(token))
			{
				if (isBiometric)
					await popupService.ShowMessageAsync("Nincs beállítva biometrikus bejelentkezés, jelentkezzen be manuálisan.", "Információ");
				return;
			}
			authService.SetToken(token);
			if (authService.IsAuthenticated())
				await HandleAuthenticatedRemember();
			else
				await popupService.ShowMessageAsync("Lejárt token");
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
		private async Task HandleAuthenticatedRemember()
		{
			UserModel? user = await userService.GetCurrentUserAsync();
			if (user == null)
			{
				await popupService.ShowMessageAsync("Nem található a megadott fehasználó", "Információ");
				return;
			}
			else if (user.email_verified_at == null)
			{
				await Shell.Current.GoToAsync($"{nameof(RegisterConfirmationPage)}");
				return;
			}
			await SetUserAppshell();

			await Shell.Current.GoToAsync($"//{nameof(MapPage)}");
		}
		private async Task HandleLogin()
		{
			try
			{
				UserModel? response = await authService.Login(LoginRequestDTO, RememberLogin);
				if (response == null)
				{
					await popupService.ShowMessageAsync("Helytelen Email-cím vagy jelszó", "Információ");
					return;
				}
				await userService.SetCurrentUser(response);
				if (response.email_verified_at == null)
				{
					await Shell.Current.GoToAsync($"{nameof(RegisterConfirmationPage)}");
				}

				else
				{
					await SetUserAppshell();
					await Shell.Current.GoToAsync($"//{nameof(MapPage)}");
				}
			}
			catch (DuplicateNameException)
			{
				await popupService.ShowMessageAsync("Helytelen Email-cím vagy jelszó");
			}
			catch (Exception)
			{
				await popupService.ShowMessageAsync($"Váratlan hiba lépett fel a bejelentkezés közben");
			}

		}
		private async Task<bool> InputCheck()
		{
			string? email_valid = InputValidator.ValidateEmail(LoginRequestDTO.email);
            if(email_valid != null)
			{
				await popupService.ShowMessageAsync(email_valid);
				return false;
			}
            string? user_password = InputValidator.ValidatePassword(LoginRequestDTO.password);
            if (user_password != null)
            {
                await popupService.ShowMessageAsync(user_password);
                return false;
            }
            return true;
		}
	}
}
