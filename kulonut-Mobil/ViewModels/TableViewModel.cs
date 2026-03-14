using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	public partial class TableViewModel : BaseViewModel
	{
		[RelayCommand]
		private async Task NavigateToDetails()
		{
			await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?{DetailsViewModel.NAV_URL}={nameof(TablePage)}");
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
