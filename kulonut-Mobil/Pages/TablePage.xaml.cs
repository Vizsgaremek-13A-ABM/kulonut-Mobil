using kulonut_Mobil.ViewModels;

namespace kulonut_Mobil.Pages;

public partial class TablePage : BasePage
{
	public TablePage(TableViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}