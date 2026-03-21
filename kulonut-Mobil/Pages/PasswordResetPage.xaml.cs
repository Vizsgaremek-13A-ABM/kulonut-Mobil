using kulonut_Mobil.ViewModels;

namespace kulonut_Mobil.Pages;

public partial class PasswordResetPage : BasePage
{
	public PasswordResetPage(PasswordResetViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}