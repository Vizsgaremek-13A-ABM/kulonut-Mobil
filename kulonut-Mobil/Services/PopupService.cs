using Android.Provider;
using CommunityToolkit.Maui.Views;
using kulonut_Mobil.Models;
using kulonut_Mobil.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Services
{
    public class PopupService : IPopupService
    {
        public async Task ShowErrorAsync(string message)
        {
            await Application.Current!.MainPage!.DisplayAlert("Error", message, "Ok");
        }
        public async Task ShowPolygonAsync(ProjectsByPolygonModel projectsForPolygon)
        {
            ProjectsPopup popup = new ProjectsPopup(projectsForPolygon);
            await Application.Current!.MainPage!.ShowPopupAsync(popup);
        }
    }
}
