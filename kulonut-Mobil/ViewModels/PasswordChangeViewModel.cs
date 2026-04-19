using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Models.DTOs;
using kulonut_Mobil.Pages;
using kulonut_Mobil.Services;
using kulonut_Mobil.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	public partial class PasswordChangeViewModel : BaseViewModel
	{
		private readonly IAuthService authService;
		private readonly IPopupService popupService;
		public ChangePasswordRequestDTO ChangePasswordRequest { get; set; } = new ChangePasswordRequestDTO();

		public PasswordChangeViewModel(IAuthService _authService, IPopupService _popupService)
		{
			authService = _authService;
			popupService = _popupService;
		}
		[RelayCommand]
		private async Task ForgotPassword()
		{
			await Shell.Current.GoToAsync($"{nameof(PasswordResetPage)}");
		}
		[RelayCommand]
		private async Task ChangePassword()
		{
			bool input_check = await InputCheck();
			if (!input_check)
				return;
			try
			{
				await authService.ChangePassword(ChangePasswordRequest);
				await authService.Logout();
				await popupService.ShowMessageAsync("Sikeresen megváltoztattuk a jelszavát, most újra be kell jelentkeznie.", "Siker");
				await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
			}
			catch (DuplicateNameException)
			{
				await popupService.ShowMessageAsync("Nem lehet ugyanaz a régi és az új jelszó", "Információ");
			}
			catch (InvalidCredentialException)
			{
				await popupService.ShowMessageAsync("Helytelen jelszavat adott meg.", "Információ");
			}
		}
		private async Task<bool> InputCheck()
		{
			string? old_password_error = InputValidator.ValidatePassword(ChangePasswordRequest?.current_password);
			if (old_password_error != null)
			{
				await popupService.ShowMessageAsync(old_password_error);
				return false;
			}
			string? password_error = InputValidator.ValidatePassword(ChangePasswordRequest?.password);
			if (password_error != null)
			{
				await popupService.ShowMessageAsync(password_error);
				return false;
			}
			if(ChangePasswordRequest?.password == ChangePasswordRequest?.current_password)
			{
				await popupService.ShowMessageAsync("Nem lehet ugyanaz a régi és az új jelszó");
				return false;
			}
			if (ChangePasswordRequest?.password != ChangePasswordRequest?.password_confirmation)
			{
				await popupService.ShowMessageAsync("Nem egyezik a két jelszó");
				return false;
			}
			return true;
		}
		public override bool OnBackButtonPressed()
		{
			MainThread.BeginInvokeOnMainThread(async () =>
			{
				await Shell.Current.GoToAsync($"..");
			});
			return true;
		}
	}
}
