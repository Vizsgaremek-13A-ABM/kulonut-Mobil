using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	public partial class AppShellViewModel : BaseViewModel
	{
		[ObservableProperty]
		private bool flyoutIsOpen;
		[RelayCommand]
		private async Task Logout()
		{
			FlyoutIsOpen = false;
			await Shell.Current.GoToAsync($"{nameof(MainPage)}");
		}
		public override bool OnBackButtonPressed()
		{
			return true;
		}
	}
}
