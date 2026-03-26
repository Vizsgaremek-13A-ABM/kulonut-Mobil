using kulonut_Mobil.ViewModels;

namespace kulonut_Mobil.Pages;

public partial class RegisterConfirmationPage : BasePage
{
	public RegisterConfirmationPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}