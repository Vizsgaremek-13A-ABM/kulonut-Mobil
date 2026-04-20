using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.MapFeatures;
using kulonut_Mobil.Models.DTOs;
using kulonut_Mobil.Services;
using Map = Mapsui.Map;

namespace kulonut_Mobil.ViewModels
{
	public partial class MapViewModel : MapViewModelBase
	{
    
		public MapViewModel(IUserService userService, IDataService _dataService, IPopupService _popupService) : base(userService, _dataService, _popupService)
		{
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
				await popupService.ShowMessageAsync("Nem sikerült lekérni a projekt adatokat");
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
			{
				await popupService.ShowMessageAsync("Nem találtunk ilyen területet, ezért az előző szűrés eredménye marad megjelenítve", "Információ");
				IsLoading = false;
				return;
			}
			if (polygons != null && polygons.data != null)
				mapHandler.ShowPolygons(polygons.data);
			
			IsLoading = false;
		}
		

	}
}
