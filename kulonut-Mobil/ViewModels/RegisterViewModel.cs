using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Models;
using kulonut_Mobil.Models.DTOs;
using kulonut_Mobil.Pages;
using kulonut_Mobil.Services;
using kulonut_Mobil.Validation;
using System.Data;
using System.Threading.Tasks;

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
				UserModel? response = await authService.RegisterAsync(RegisterRequestDTO);
				if (response == null)
				{
					IsLoading = false;
					await popupService.ShowErrorAsync("Hiba történt a regisztrációban");
					return;
				}
				await Shell.Current.GoToAsync($"{nameof(RegisterConfirmationPage)}");
			}
			catch (DuplicateNameException)
			{
				await popupService.ShowErrorAsync("Az email cím már foglalt", "Információ");
			}
			catch (Exception)
			{
				await popupService.ShowErrorAsync("Váratlan hiba lépett fel regisztráció közben");
			}
			finally
			{
				IsLoading = false;
			}
			
		}
		private async Task<bool> InputCheck()
		{
			string? email_error = InputValidator.ValidateEmail(RegisterRequestDTO.email);
			if (email_error != null)
			{
				await popupService.ShowErrorAsync(email_error);
				return false;
			}
			string? name_error = InputValidator.ValidateName(RegisterRequestDTO?.name);
			if(name_error != null)
			{
				await popupService.ShowErrorAsync(name_error);
				return false;
			}
			string? password_error = InputValidator.ValidatePassword(RegisterRequestDTO?.password);
			if (password_error != null)
			{
				await popupService.ShowErrorAsync(password_error);
				return false;
			}	
			if(RegisterRequestDTO?.password != RegisterRequestDTO?.password_confirmation)
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
				await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
			});
			return true;
		}
	}
}
