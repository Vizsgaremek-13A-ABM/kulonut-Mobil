using kulonut_Mobil.ViewModels;

namespace kulonut_Mobil.Pages;

public partial class ProjectDetailsPage : BasePage
{
	public ProjectDetailsPage(ProjectDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}