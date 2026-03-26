using CommunityToolkit.Maui.Views;

namespace kulonut_Mobil.Popups;

public partial class ErrorPopup : Popup
{
	public string TitleHead { get; set; }
	public string Description { get; set; }
	public ErrorPopup(string description, string titleHead)
	{
		InitializeComponent();
		Description = description;
		TitleHead = titleHead;
		BindingContext = this;
	}

	private void CloseBTN_Clicked(object sender, EventArgs e)
	{
		Close();
	}
}