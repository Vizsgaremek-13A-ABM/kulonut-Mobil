using CommunityToolkit.Maui.Views;
using kulonut_Mobil.ViewModels;

namespace kulonut_Mobil.Popups;

public partial class FilterPopup : Popup
{
	public FilterPopup(FilterPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}