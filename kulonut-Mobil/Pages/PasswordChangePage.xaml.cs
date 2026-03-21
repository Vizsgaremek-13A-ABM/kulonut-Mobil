using kulonut_Mobil.ViewModels;

namespace kulonut_Mobil.Pages;

public partial class PasswordChangePage : BasePage
{
	public PasswordChangePage(PasswordChangeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}