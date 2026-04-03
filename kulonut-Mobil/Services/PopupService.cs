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
		private readonly IServiceProvider _serviceProvider;

		public PopupService(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}
		public async Task ShowErrorAsync(string message, string titleHead = "Hiba")
        {
			ErrorPopup errorPopup = new ErrorPopup(message, titleHead);
            await Application.Current!.MainPage!.ShowPopupAsync(errorPopup);
        }
		public async Task ShowPolygonAsync(ProjectsByPolygonModel projectsForPolygon)
		{
			ProjectsPopup popup = _serviceProvider.GetRequiredService<ProjectsPopup>();
			popup.Initialize(projectsForPolygon);
			await Application.Current!.MainPage!.ShowPopupAsync(popup);
		}
		public async Task ShowUserEditAsync(string propname)
		{
			UserEditPopup popup = _serviceProvider.GetRequiredService<UserEditPopup>();
			popup.Initialize(propname);
			await Application.Current!.MainPage!.ShowPopupAsync(popup);
		}
		public async Task ShowFilterAsync()
		{
			FilterPopup filterPopup = _serviceProvider.GetRequiredService<FilterPopup>();
			await Application.Current!.MainPage!.ShowPopupAsync(filterPopup);

		}
	}
}
