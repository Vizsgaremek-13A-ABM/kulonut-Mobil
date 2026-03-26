using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.MapFeatures;
using kulonut_Mobil.Models.DTOs;
using kulonut_Mobil.Pages;
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
		private async Task HandleMapLoading()
		{
			PolygonsResponseDTO? polygons = await dataService.GetPolygons();
			if(polygons == null || polygons.data == null || polygons.data.Count == 0)
			{
				await popupService.ShowErrorAsync("Nem találtunk polygont");
				return;
			}
			Debug.WriteLine(polygons.data.Count);
			mapHandler.ShowPolygons(polygons.data);
		}
		[RelayCommand]
		private async Task PolygonClicked(PolygonFeature polygonFeature)
		{
			ProjectsByPolygonResponseDTO? response = await dataService.GetProjectsByPolygonId(polygonFeature.Id);
			if (response == null || response.data == null)
			{
				await popupService.ShowErrorAsync("Nem sikerült lekérni a projekt adatokat");
                return;
            }
			await popupService.ShowPolygonAsync(response.data);
        }
	}
}
