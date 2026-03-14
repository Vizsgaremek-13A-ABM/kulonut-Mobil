using kulonut_Mobil.ViewModels;

namespace kulonut_Mobil.Pages;

public partial class MapPage : BasePage
{
	public MapPage(MapViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}