using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Models;
using kulonut_Mobil.Pages;
using kulonut_Mobil.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	[QueryProperty(nameof(NavigatedFrom), NAV_URL)]
	public partial class UserDetailsViewModel : UserViewModelBase
    {
		private readonly IPopupService popupService;
		public const string NAV_URL = "navigatedFrom";
		public string? NavigatedFrom { get; set; }

		
		public UserDetailsViewModel(IUserService userService, IPopupService _popupService) : base(userService)
		{
			popupService = _popupService;
		}
		[RelayCommand]
		private async Task UserSwipedRight()
		{
			await Shell.Current.GoToAsync($"//{NavigatedFrom}");
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
				{
					return;
					
				}

				using Stream stream = await result.OpenReadAsync();
				UserModel? user = await userService.UploadUserImageAsync(stream, result.FileName);
				if (user != null)
					User = user;								
			}
			catch (Exception ex)
			{
				await popupService.ShowErrorAsync(ex.Message);
			}
		}
		public override bool OnBackButtonPressed()
		{
			MainThread.BeginInvokeOnMainThread(async () =>
			{
				await Shell.Current.GoToAsync($"//{NavigatedFrom}");
			});
			return true;
		}
	
	}
}
