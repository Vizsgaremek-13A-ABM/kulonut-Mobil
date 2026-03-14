using kulonut_Mobil.ViewModels;

namespace kulonut_Mobil
{
    public partial class App : Application
    {
        private AppShellViewModel vm = new AppShellViewModel();
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell(vm);
        }
    }
}
