using kulonut_Mobil.Models;
using kulonut_Mobil.Pages;
using kulonut_Mobil.ViewModels;
using System.Diagnostics;

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
		public UserHeader()
		{
			InitializeComponent();
		}
		private async void UserIconButton_Clicked(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync($"{nameof(UserDetailsPage)}");
		}
	}
}
