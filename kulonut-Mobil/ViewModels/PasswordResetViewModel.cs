using kulonut_Mobil.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	[QueryProperty(nameof(NavigatedFrom), NAV_URL)]
	public partial class PasswordResetViewModel : BaseViewModel
	{
		public const string NAV_URL = "navigatedFrom";
		public string? NavigatedFrom { get; set; }
		public override bool OnBackButtonPressed()
		{
			MainThread.BeginInvokeOnMainThread(async () =>
			{
				if(NavigatedFrom==nameof(PasswordChangePage))
					await Shell.Current.GoToAsync($"{NavigatedFrom}");
				else
					await Shell.Current.GoToAsync($"//{NavigatedFrom}");
			});
			return true;
		}
	}
}
