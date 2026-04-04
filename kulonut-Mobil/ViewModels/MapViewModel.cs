using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.MapFeatures;
using kulonut_Mobil.Models.DTOs;
using kulonut_Mobil.Services;
using System.Diagnostics;
using Map = Mapsui.Map;

namespace kulonut_Mobil.ViewModels
{
	public partial class MapViewModel : UserViewModelBase
	{

		private readonly IDataService dataService;
		private readonly IPopupService popupService;
		private readonly IMapHandler mapHandler;
		public Map Map { get; } = new Map();
    
		public MapViewModel(IUserService userService, IDataService _dataService, IPopupService _popupService) : base(userService)
		{
			dataService = _dataService;
			popupService = _popupService;
			mapHandler = new MapHandler(Map);
			mapHandler.CreateMap();
		}
		[RelayCommand]
		private async Task OpenFilter()
		{
			await popupService.ShowFilterAsync(true);
			await HandleMapLoading();
		}
		[RelayCommand]
		private async Task PolygonClicked(PolygonFeature polygonFeature)
		{
			IsLoading = true;
			ProjectsByPolygonResponseDTO? response = await dataService.GetProjectsByPolygonId(polygonFeature.Id);
			if (response == null || response.data == null)
			{
				await popupService.ShowErrorAsync("Nem sikerült lekérni a projekt adatokat");
				IsLoading = false;
                return;
            }
			await popupService.ShowPolygonAsync(response.data);
			IsLoading = false;
        }
		[RelayCommand]
		private async Task RemoveFilter()
		{
			dataService.PolygonFilterModel = null;
			await HandleMapLoading();
		}
		[RelayCommand]
		private async Task HandleMapLoading()
		{
			IsLoading = true;
			PolygonsResponseDTO? polygons = await dataService.GetPolygons();
			if (polygons == null || polygons.data == null || polygons.data.Count == 0)
				await popupService.ShowErrorAsync("Nem találtunk ilyen területet");

			if (polygons != null && polygons.data != null)
			{
				mapHandler.ShowPolygons(polygons.data);
				Debug.WriteLine(polygons.data.Count);
			}
			IsLoading = false;
		}
		[RelayCommand]
		private async Task Locate()
		{
			try
			{
				await mapHandler.Locate();
			}
			catch (UnauthorizedAccessException)
			{
				await popupService.ShowErrorAsync("A helymeghatározási engedély nincs megadva.");
			}
			catch (InvalidOperationException)
			{
				await popupService.ShowErrorAsync("Nem sikerült meghatározni a tartózkodási helyet.");
			}
			catch (Exception)
			{
				await popupService.ShowErrorAsync("Ismeretlen hiba történt a helymeghatározás során.");
			}
		}

	}
}
