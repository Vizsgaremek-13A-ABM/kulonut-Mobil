using CommunityToolkit.Maui.Views;
using System.Threading.Tasks;

namespace kulonut_Mobil.Popups;

public partial class QuitPopup : Popup
{
	public QuitPopup()
	{
		InitializeComponent();
	}

	private void QuitBTN_Clicked(object sender, EventArgs e)
	{
		Application.Current!.Quit();
	}

	private void CloseBTN_Clicked(object sender, EventArgs e)
	{
		Close();
	}
}