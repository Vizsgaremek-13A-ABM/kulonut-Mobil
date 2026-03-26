using CommunityToolkit.Maui.Views;
using kulonut_Mobil.Models;
using kulonut_Mobil.ViewModels;

namespace kulonut_Mobil.Popups;

public partial class ProjectsPopup : Popup
{
	private readonly ProjectsPopupViewModel projectsPopupViewModel;
    public ProjectsPopup(ProjectsPopupViewModel _projectsPopupViewModel)
	{
		InitializeComponent();
		projectsPopupViewModel = _projectsPopupViewModel;
		BindingContext = _projectsPopupViewModel;
		projectsPopupViewModel.OnClose += async result => await CloseAsync(result, CancellationToken.None);
	}
	public void Initialize(ProjectsByPolygonModel projectsForPolygon)
	{
		projectsPopupViewModel.Initialize(projectsForPolygon);
	}
}