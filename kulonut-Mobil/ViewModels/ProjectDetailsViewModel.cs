using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Pages;
using kulonut_Mobil.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	[QueryProperty(nameof(NavigatedFrom), NAV_URL)]
	[QueryProperty(nameof(Id), ID_URL)]
	public partial class ProjectDetailsViewModel : UserViewModelBase
	{
		public const string NAV_URL = "navigatedFrom";
		public const string ID_URL = "id";
		public ProjectDetailsViewModel(IUserService userService) : base(userService)
		{
		}
		public int Id { get; set; }
		public string? NavigatedFrom { get; set; }
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
