using Android.Provider;
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
    }
}
