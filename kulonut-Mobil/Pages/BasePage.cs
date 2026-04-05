using kulonut_Mobil.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Pages
{
	public class BasePage : ContentPage
	{
		public BasePage()
		{
			BackgroundImageSource = "background";
		}
		protected override bool OnBackButtonPressed()
		{
			if (BindingContext is BaseViewModel vm)
				return vm.OnBackButtonPressed();
			return base.OnBackButtonPressed();
		}

	}
}
