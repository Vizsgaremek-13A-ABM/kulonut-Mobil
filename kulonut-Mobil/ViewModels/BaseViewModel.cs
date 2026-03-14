using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	public partial class BaseViewModel : ObservableObject
	{
		public virtual bool OnBackButtonPressed()
		{
			MainThread.BeginInvokeOnMainThread(async () =>
			{
				bool shouldExit = await Application.Current!.MainPage!.DisplayAlert("Kilépés", "Ki akarsz lépni az applikációból", "Igen", "Nem");
				if (shouldExit) CloseApp();
			});
			return true;
		}
		protected void CloseApp()
		{
			#if ANDROID
					Microsoft.Maui.ApplicationModel.Platform.CurrentActivity?.FinishAffinity();
			#elif WINDOWS
					Microsoft.UI.Xaml.Application.Current.Exit();
			#elif IOS
					System.Environment.Exit(0);
			#endif
		}
	}
}
