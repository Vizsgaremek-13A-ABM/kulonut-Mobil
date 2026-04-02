using kulonut_Mobil.Services;

namespace kulonut_Mobil.Views;

public partial class UserEditLabel : ContentView
{
	public static readonly BindableProperty BodyTextProperty = BindableProperty.Create(nameof(BodyText), typeof(string), typeof(UserEditLabel), default(string), BindingMode.TwoWay);
	public string BodyText
	{
		get => (string)GetValue(BodyTextProperty);
		set => SetValue(BodyTextProperty, value);
	}
	public string HeaderText
	{
		get => HeaderLabel.Text;
		set => HeaderLabel.Text = value;
	}
	public string Propname { get; set; } = "";
	public UserEditLabel()
	{
		InitializeComponent();
	}

	private async void ImageButton_Clicked(object sender, EventArgs e)
	{
		IPopupService? popupService = Application.Current!.MainPage!.Handler!.MauiContext!.Services.GetService<IPopupService>();
		await popupService!.ShowUserEditAsync(Propname);
	}
}