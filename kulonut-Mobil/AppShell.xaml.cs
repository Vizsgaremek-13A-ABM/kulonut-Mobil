using kulonut_Mobil.DependencyInjection;
using kulonut_Mobil.ViewModels;

namespace kulonut_Mobil
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            RouteExtensions.AddRoutes();
		}
    }
}
