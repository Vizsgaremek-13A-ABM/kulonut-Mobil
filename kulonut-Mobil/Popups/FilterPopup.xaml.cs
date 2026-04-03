using CommunityToolkit.Maui.Views;
using kulonut_Mobil.ViewModels;

namespace kulonut_Mobil.Popups;

public partial class FilterPopup : Popup
{
	private readonly FilterPopupViewModel filterPopupViewModel;
	public FilterPopup(FilterPopupViewModel _filterPopupViewModel)
	{
		InitializeComponent();
		filterPopupViewModel = _filterPopupViewModel;
		BindingContext = filterPopupViewModel;
		filterPopupViewModel.OnClose += async result => await CloseAsync(result, CancellationToken.None);
	}
	public void Initialize(bool isPolygonFilter)
	{
		filterPopupViewModel.Initialize(isPolygonFilter);
	}
}