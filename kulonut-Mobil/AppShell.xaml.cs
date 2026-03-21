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
            Routing.RegisterRoute(nameof(ProjectDetailsPage), typeof(ProjectDetailsPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));
            Routing.RegisterRoute(nameof(TablePage), typeof(TablePage));
            Routing.RegisterRoute(nameof(UserDetailsPage), typeof(UserDetailsPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(PasswordChangePage), typeof(PasswordChangePage));
            Routing.RegisterRoute(nameof(PasswordResetPage), typeof(PasswordResetPage));
			//Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));
		}
    }
}
