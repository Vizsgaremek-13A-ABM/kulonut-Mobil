using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Models;
using kulonut_Mobil.Models.DTOs;
using kulonut_Mobil.Pages;
using kulonut_Mobil.Services;
using kulonut_Mobil.Validation;
using System.Data;

namespace kulonut_Mobil.ViewModels
{
	public partial class RegisterViewModel : BaseViewModel
	{
		private readonly IAuthService authService;
		private readonly IPopupService popupService;
		public RegisterRequestDTO RegisterRequestDTO { get; set; } = new RegisterRequestDTO();
		public RegisterViewModel(IAuthService _authService, IPopupService _popupService)
		{
			authService = _authService;
			popupService = _popupService;
		}
		
		[RelayCommand]
		private async Task HandleRegister()
		{
			IsLoading = true;
			bool input_check = await InputCheck();
			if (!input_check)
			{
				IsLoading = false;
				return;
			}
			try
			{
				await HandleUserResponse();
			}
			catch (DuplicateNameException)
			{
				await popupService.ShowMessageAsync("Az email cím már foglalt", "Információ");
			}
			catch (Exception)
			{
				await popupService.ShowMessageAsync("Váratlan hiba lépett fel regisztráció közben");
			}
			finally
			{
				IsLoading = false;
			}
			
		}
		private async Task HandleUserResponse()
		{
			UserModel? response = await authService.Register(RegisterRequestDTO);
			if (response == null)
			{
				IsLoading = false;
				await popupService.ShowMessageAsync("Váratlan hiba történt a regisztrációban");
				return;
			}
			await Shell.Current.GoToAsync($"{nameof(RegisterConfirmationPage)}");
		}
		private async Task<bool> InputCheck()
		{
			string? email_error = InputValidator.ValidateEmail(RegisterRequestDTO.email);
			if (email_error != null)
			{
				await popupService.ShowMessageAsync(email_error, "Információ");
				return false;
			}
			string? name_error = InputValidator.ValidateName(RegisterRequestDTO?.name);
			if(name_error != null)
			{
				await popupService.ShowMessageAsync(name_error, "Információ");
				return false;
			}
			string? password_error = InputValidator.ValidatePassword(RegisterRequestDTO?.password);
			if (password_error != null)
			{
				await popupService.ShowMessageAsync(password_error, "Információ");
				return false;
			}	
			if(RegisterRequestDTO?.password != RegisterRequestDTO?.password_confirmation)
			{
				await popupService.ShowMessageAsync("Nem egyezik a két jelszó", "Információ");
				return false;
			}
			return true;
		}
		public override bool OnBackButtonPressed()
		{
			MainThread.BeginInvokeOnMainThread(async () =>
			{
				await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
			});
			return true;
		}
	}
}
