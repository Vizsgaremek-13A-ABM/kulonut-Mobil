using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Models.DTOs;
using kulonut_Mobil.Pages;
using kulonut_Mobil.Services;

namespace kulonut_Mobil.ViewModels
{
	public partial class RegisterConfirmationViewModel : UserViewModelBase
	{
		private IAuthService authService;
		private IPopupService popupService;
		public RegisterConfirmationViewModel(IUserService userService, IAuthService _authService, IPopupService _popupService) : base(userService)
		{
			authService = _authService;
			popupService = _popupService;
		}
		public override bool OnBackButtonPressed()
		{
			MainThread.BeginInvokeOnMainThread(async () =>
			{
				await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
			});
			return true;
		}
		[RelayCommand]
		public async Task NavBack()
		{
			await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
		}
		[RelayCommand]
		public async Task Resend()
		{
			try
			{
				MessageResponseDTO? response = await authService.ResendRegister();
				if(response == null) await popupService.ShowMessageAsync("Váratlan hiba történt az újraküldés közben");
			}
			catch (Exception)
			{
				await popupService.ShowMessageAsync("Váratlan hiba történt az újraküldés közben");
			}
		}
	}
}
