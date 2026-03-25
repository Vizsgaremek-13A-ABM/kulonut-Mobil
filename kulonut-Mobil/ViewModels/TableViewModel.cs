using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Pages;
using kulonut_Mobil.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	public partial class TableViewModel : UserViewModelBase
	{
		public TableViewModel(IUserService _userService) : base(_userService)
		{
		}

		[RelayCommand]
		private async Task NavigateToDetails()
		{
			await Shell.Current.GoToAsync($"{nameof(ProjectDetailsPage)}?{ProjectDetailsViewModel.NAV_URL}={nameof(TablePage)}");
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
