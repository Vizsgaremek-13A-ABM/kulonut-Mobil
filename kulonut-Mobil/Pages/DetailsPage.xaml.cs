using kulonut_Mobil.ViewModels;

namespace kulonut_Mobil.Pages;

public partial class DetailsPage : BasePage
{
	public DetailsPage(DetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}