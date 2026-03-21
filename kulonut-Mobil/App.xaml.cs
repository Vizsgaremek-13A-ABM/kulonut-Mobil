using kulonut_Mobil.ViewModels;

namespace kulonut_Mobil
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            MainPage = serviceProvider.GetRequiredService<AppShell>();
        }
    }
}
