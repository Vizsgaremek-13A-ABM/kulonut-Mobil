using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Models.DTOs;
using kulonut_Mobil.Pages;
using kulonut_Mobil.Services;
using kulonut_Mobil.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
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
			await Shell.Current.GoToAsync($"{nameof(PasswordResetPage)}?{PasswordResetViewModel.NAV_URL}={nameof(PasswordChangePage)}");
		}
		[RelayCommand]
		private async Task ChangePassword()
		{
			bool input_check = await InputCheck();
			if (!input_check)
				return;
			await authService.ChangePasswordAsync(ChangePasswordRequest);
			await authService.LogoutAsync();
			await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
		}
		private async Task<bool> InputCheck()
		{
			string? old_password_error = InputValidator.ValidatePassword(ChangePasswordRequest?.current_password);
			if (old_password_error != null)
			{
				await popupService.ShowErrorAsync(old_password_error);
				return false;
			}
			string? password_error = InputValidator.ValidatePassword(ChangePasswordRequest?.password);
			if (password_error != null)
			{
				await popupService.ShowErrorAsync(password_error);
				return false;
			}
			if (ChangePasswordRequest?.password != ChangePasswordRequest?.password_confirmation)
			{
				await popupService.ShowErrorAsync("Nem egyezik a két jelszó");
				return false;
			}
			return true;
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
