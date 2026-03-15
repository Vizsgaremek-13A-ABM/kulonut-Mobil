using kulonut_Mobil.ViewModels;

namespace kulonut_Mobil.Pages;

public partial class UserDetailsPage : BasePage
{
	public UserDetailsPage(UserDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}