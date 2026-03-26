using CommunityToolkit.Maui.Views;
using kulonut_Mobil.Models;

namespace kulonut_Mobil.Popups;

public partial class ProjectsPopup : Popup
{
	public ProjectsByPolygonModel ProjectsForPolygon { get; set; }
    public ProjectsPopup(ProjectsByPolygonModel projectsForPolygon)
	{
		InitializeComponent();
		ProjectsForPolygon = projectsForPolygon;
		BindingContext = this;
    }
}