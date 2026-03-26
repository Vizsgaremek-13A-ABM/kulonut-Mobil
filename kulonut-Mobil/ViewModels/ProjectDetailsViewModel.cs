using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Models;
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
		public string? NavigatedFrom { get; set; }
		public int Id { get; set; }
		private readonly IDataService dataService;
		private readonly IPopupService popupService;

		[ObservableProperty]
		private ProjectModel? currentProject;
		public ProjectDetailsViewModel(IUserService userService, IDataService _dataService, IPopupService _popupService) : base(userService)
		{
			dataService = _dataService;
			popupService = _popupService;
		}

		[RelayCommand]
		private async Task HandleProjectLoad()
		{
			var current_project = await dataService.GetProjectById(Id);
			if (current_project == null || current_project.data == null)
				await popupService.ShowErrorAsync($"Nincs ilyen projekt");
			else
				CurrentProject = current_project.data;
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
