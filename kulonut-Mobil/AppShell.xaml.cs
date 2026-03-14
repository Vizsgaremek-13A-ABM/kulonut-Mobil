using kulonut_Mobil.Pages;
using kulonut_Mobil.ViewModels;

namespace kulonut_Mobil
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));
            Routing.RegisterRoute(nameof(TablePage), typeof(TablePage));
			//Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));
		}
    }
}
