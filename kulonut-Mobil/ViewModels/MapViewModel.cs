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
	public partial class MapViewModel : BaseViewModel
	{
		[ObservableProperty]
		private UserModel user;

		private IUserService userService;
		public MapViewModel(IUserService _userService)
		{
			userService = _userService;
		}

		[RelayCommand]
		private async Task HandleUserLoad()
		{
			UserModel? _user = await userService.GetCurrentUserAsync();
			if (_user == null) User = userService.GetCurrentUser()!;
			else User = _user;
		}
		[RelayCommand]
		private async Task NavigateToDetails()
		{
			await Shell.Current.GoToAsync($"{nameof(ProjectDetailsPage)}?{ProjectDetailsViewModel.NAV_URL}={nameof(MapPage)}");
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
