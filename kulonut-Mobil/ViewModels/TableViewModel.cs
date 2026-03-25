using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Models;
using kulonut_Mobil.Models.DTOs;
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
	public partial class TableViewModel : UserViewModelBase
	{
		[ObservableProperty]
		private List<ProjectModel>? projects;

		private IDataService dataService;
		private IPopupService popupService;
		public TableViewModel(IUserService _userService, IDataService _dataService, IPopupService _popupService) : base(_userService)
		{
			dataService = _dataService;
			popupService = _popupService;
		}
		[RelayCommand]
		private async Task HandleProjectLoad()
		{
			ProjectsResponseDTO? projects_response = await dataService.GetProjects();
			if (projects_response == null || projects_response.data == null)
			{
				await popupService.ShowErrorAsync("Nem sikerult betölteni a projekteket");
				return;
			}
			Projects = projects_response.data;
			Debug.WriteLine(Projects.Count);
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
