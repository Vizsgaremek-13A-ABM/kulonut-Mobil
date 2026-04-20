using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Models;
using kulonut_Mobil.Pages;
using kulonut_Mobil.Services;

namespace kulonut_Mobil.ViewModels
{
	public partial class UserDetailsViewModel : UserViewModelBase
    {
		private readonly IPopupService popupService;
		public UserDetailsViewModel(IUserService userService, IPopupService _popupService) : base(userService)
		{
			popupService = _popupService;
		}
		[RelayCommand]
		private async Task UserSwipedRight()
		{
			await Shell.Current.GoToAsync($"..");
		}
		[RelayCommand]
		private async Task PasswordChange()
		{
			await Shell.Current.GoToAsync($"{nameof(PasswordChangePage)}");
		}
		[RelayCommand]
		private async Task UploadPicture()
		{
			try
			{
				
				FileResult? result = await MediaPicker.Default.PickPhotoAsync();
				if (result == null)
					return;
				
				IsLoading = true;
				using Stream stream = await result.OpenReadAsync();
				UserModel? user = await userService.UploadUserImageAsync(stream, result.FileName);
				if (user != null)
					User = user;
			}
			catch (ApplicationException)
			{
				await popupService.ShowMessageAsync("Túl nagy a fájl", "Információ");
			}
			catch (Exception)
			{
				await popupService.ShowMessageAsync("Váratlan hiba képfeltöltés közben");
			}
			finally 
			{
				IsLoading = false;
			}
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
