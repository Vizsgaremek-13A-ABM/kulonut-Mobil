using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.MapFeatures;
using kulonut_Mobil.Models;
using kulonut_Mobil.Models.DTOs;
using kulonut_Mobil.Services;
using Map = Mapsui.Map;


namespace kulonut_Mobil.ViewModels
{
	[QueryProperty(nameof(NavigatedFrom), NAV_URL)]
	[QueryProperty(nameof(Id), ID_URL)]
	public partial class ProjectDetailsViewModel : UserViewModelBase
	{
		private readonly IDataService dataService;
		private readonly IPopupService popupService;
		private readonly IMapHandler mapHandler;

		public const string NAV_URL = "navigatedFrom";
		public const string ID_URL = "id";
		public string? NavigatedFrom { get; set; }
		public int Id { get; set; }
		public Map Map { get; } = new Map();

		[ObservableProperty]
		private ProjectModel? currentProject;
		public ProjectDetailsViewModel(IUserService userService, IDataService _dataService, IPopupService _popupService) : base(userService)
		{
			dataService = _dataService;
			popupService = _popupService;
			mapHandler = new MapHandler(Map);
			mapHandler.CreateMap();
		}

		[RelayCommand]
		private async Task HandleProjectLoad()
		{
			IsLoading = true;
			ProjectResponseDTO? current_project = await dataService.GetProjectById(Id);
			if (current_project == null || current_project.data == null)
				await popupService.ShowErrorAsync($"Nincs ilyen projekt");
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
				await popupService.ShowErrorAsync("Nem találtunk területet az adott polygonhoz");

			if (polygons != null && polygons.data != null)
				mapHandler.ShowPolygons(polygons.data);
		}
	}
}
