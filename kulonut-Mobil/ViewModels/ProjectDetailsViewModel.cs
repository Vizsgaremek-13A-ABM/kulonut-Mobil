using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Models;
using kulonut_Mobil.Models.DTOs;
using kulonut_Mobil.Services;
using Map = Mapsui.Map;


namespace kulonut_Mobil.ViewModels
{
	[QueryProperty(nameof(NavigatedFrom), NAV_URL)]
	[QueryProperty(nameof(Id), ID_URL)]
	public partial class ProjectDetailsViewModel : MapViewModelBase
	{

		public const string NAV_URL = "navigatedFrom";
		public const string ID_URL = "id";
		public string? NavigatedFrom { get; set; }
		public int Id { get; set; }

		[ObservableProperty]
		private ProjectModel? currentProject;
		public ProjectDetailsViewModel(IUserService userService, IDataService _dataService, IPopupService _popupService) : base(userService, _dataService, _popupService)
		{
		}

		[RelayCommand]
		private async Task HandleProjectLoad()
		{
			IsLoading = true;
			ProjectResponseDTO? current_project = await dataService.GetProjectById(Id);
			if (current_project == null || current_project.data == null)
				await popupService.ShowMessageAsync("Nem találtunk ilyen területet", "Információ");
			else
			{
				CurrentProject = current_project.data;
				await HandleMapLoading();
			}
			IsLoading = false;
		}
		public override bool OnBackButtonPressed()
		{
			MainThread.BeginInvokeOnMainThread(async () =>
			{
				await Shell.Current.GoToAsync($"//{NavigatedFrom}");
			});
			return true;
		}
		private async Task HandleMapLoading()
		{
			PolygonsResponseDTO? polygons = await dataService.GetPolygonsByProject(CurrentProject!.id);
			if (polygons == null || polygons.data == null || polygons.data.Count == 0)
				await popupService.ShowMessageAsync("Nem találtunk területet az adott polygonhoz", "Információ");

			if (polygons != null && polygons.data != null)
			{
				mapHandler.ShowPolygons(polygons.data);
				NavigateCenter(polygons.data);
			}
		}
		private void NavigateCenter(List<PolygonModel> polygons)
		{
			List<PolygonModel> validPolygons = polygons.Where(p => p.coordinates != null && p.coordinates.Any()).ToList();
			if (validPolygons.Any())
			{
				double lat_center = validPolygons.Average(x => x.coordinates!.Average(y => y.latitude));
				double lon_center = validPolygons.Average(x => x.coordinates!.Average(y => y.longitude));
				mapHandler.ZoomTo(lon_center, lat_center);
			}
		}
	}
}
