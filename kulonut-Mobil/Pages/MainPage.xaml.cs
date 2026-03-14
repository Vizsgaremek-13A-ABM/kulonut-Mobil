using kulonut_Mobil.ViewModels;

namespace kulonut_Mobil.Pages
{
    public partial class MainPage : BasePage
    {
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        
    }

}
