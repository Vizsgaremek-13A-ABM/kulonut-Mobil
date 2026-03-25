using kulonut_Mobil.Models;
using kulonut_Mobil.Pages;
using kulonut_Mobil.ViewModels;

namespace kulonut_Mobil.Views
{
	public partial class UserHeader : ContentView
	{
		public static readonly BindableProperty UserProperty = BindableProperty.Create(nameof(User), typeof(UserModel), typeof(UserHeader), default(UserModel), BindingMode.TwoWay);
		public UserModel User
		{
			get => (UserModel)GetValue(UserProperty);
			set => SetValue(UserProperty, value);
		}
		public string Title
		{
			get => TitleLabel.Text;
			set => TitleLabel.Text = value;
		}
		public string NavigatedFrom { get; set; }
		public UserHeader()
		{
			InitializeComponent();
		}
		private async void UserIconButton_Clicked(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync($"{nameof(UserDetailsPage)}?{UserDetailsViewModel.ID_URL}={User.id}&{UserDetailsViewModel.NAV_URL}={NavigatedFrom}");
		}
	}
}
