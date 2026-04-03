using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Models;
using kulonut_Mobil.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
    public partial class ProjectsPopupViewModel : PopupViewModelBase
    {
        [ObservableProperty]
        private ProjectsByPolygonModel? projectsByPolygon;


		public void Initialize(ProjectsByPolygonModel projectsByPolygon)
        {
		    ProjectsByPolygon = projectsByPolygon;
        }

        [RelayCommand]
        private async Task NavigateToDetails(int project_id)
        {
            await ClosePopup();
            await Shell.Current.GoToAsync($"{nameof(ProjectDetailsPage)}?{ProjectDetailsViewModel.NAV_URL}={nameof(MapPage)}&{ProjectDetailsViewModel.ID_URL}={project_id}");
        }

	}
}
