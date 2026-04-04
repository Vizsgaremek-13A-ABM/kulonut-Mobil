using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using kulonut_Mobil.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	public partial class BaseViewModel : ObservableObject
	{
		[ObservableProperty]
		private bool isLoading = false;
		public virtual bool OnBackButtonPressed()
		{
			var popup = new QuitPopup();
			Application.Current!.MainPage!.ShowPopup(popup);
			return true;
		}
	}
}
