using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.MapFeatures;
using kulonut_Mobil.Services;
using System;
using Map = Mapsui.Map;


namespace kulonut_Mobil.ViewModels
{
    public partial class MapViewModelBase : UserViewModelBase
    {
		protected readonly IDataService dataService;
		protected readonly IPopupService popupService;
		protected readonly IMapHandler mapHandler;
		public Map Map { get; } = new Map();

		public MapViewModelBase(IUserService userService, IDataService _dataService, IPopupService _popupService) : base(userService)
		{
			dataService = _dataService;
			popupService = _popupService;
			mapHandler = new MapHandler(Map);
			mapHandler.CreateMap();
		}
		[RelayCommand]
		protected async Task Locate()
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
