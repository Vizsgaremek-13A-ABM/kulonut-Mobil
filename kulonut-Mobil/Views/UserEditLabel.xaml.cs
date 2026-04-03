using kulonut_Mobil.Services;
using System.Windows.Input;

namespace kulonut_Mobil.Views;

public partial class UserEditLabel : ContentView
{
	public static readonly BindableProperty BodyTextProperty = BindableProperty.Create(nameof(BodyText), typeof(string), typeof(UserEditLabel), default(string), BindingMode.TwoWay);
	public static readonly BindableProperty RefreshUserCommandProperty = BindableProperty.Create(nameof(RefreshUserCommand), typeof(ICommand), typeof(UserEditLabel));

	public ICommand? RefreshUserCommand
	{
		get => (ICommand?)GetValue(RefreshUserCommandProperty);
		set => SetValue(RefreshUserCommandProperty, value);
	}
	public string BodyText
	{
		get => (string)GetValue(BodyTextProperty);
		set => SetValue(BodyTextProperty, value);
	}
	public string HeaderText
	{
		get => HeaderLabel.Text;
		set { HeaderLabel.Text = value; Propname = value; }
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
		if (RefreshUserCommand?.CanExecute(null) == true)
			RefreshUserCommand.Execute(null);
	}
}