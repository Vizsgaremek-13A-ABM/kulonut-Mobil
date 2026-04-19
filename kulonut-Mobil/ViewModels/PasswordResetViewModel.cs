using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Models.DTOs;
using kulonut_Mobil.Pages;
using kulonut_Mobil.Services;
using kulonut_Mobil.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	public partial class PasswordResetViewModel : BaseViewModel
	{
		public ForgotPasswordRequestDTO ForgotPasswordRequest { get; set; } = new ForgotPasswordRequestDTO();
		private readonly IAuthService authService;
		private readonly IPopupService popupService;
		public PasswordResetViewModel(IAuthService _authService, IPopupService _popupService)
		{
			authService = _authService;
			popupService = _popupService;
		}
		public override bool OnBackButtonPressed()
		{
			MainThread.BeginInvokeOnMainThread(async () =>
			{
				await Shell.Current.GoToAsync($"..");
			});
			return true;
		}
		[RelayCommand]
		private async Task Reset()
		{
			bool check_result = await InputCheck();
			if (!check_result) return;
			try
			{
				await authService.RequestPasswordReset(ForgotPasswordRequest);
			}
			catch (DuplicateNameException)
			{ 
				await popupService.ShowMessageAsync("Nincs ilyen email-cím az adatbázisban");
				return;
			}
			catch (Exception)
			{
				await popupService.ShowMessageAsync("Váratlan hiba történt");
				return;
			}
			await popupService.ShowMessageAsync("A Jelszó visszaállítása sikeresen megkezdődött. A visszaállítás menetét email-ben részletezzük.", "Üzenet");
			await authService.Logout();
			await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
		}
		private async Task<bool> InputCheck()
		{
			string? email_valid = InputValidator.ValidateEmail(ForgotPasswordRequest.email);
			if (email_valid != null)
			{
				await popupService.ShowMessageAsync(email_valid);
				return false;
			}
			return true;
		}
	}
}
