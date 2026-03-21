using kulonut_Mobil.Pages;
using kulonut_Mobil.ViewModels;

namespace kulonut_Mobil.Views
{
	public partial class UserHeader : ContentView
	{
		public static readonly BindableProperty NameTextProperty = BindableProperty.Create(nameof(NameText), typeof(string), typeof(UserHeader), default(string), BindingMode.TwoWay);
		public static readonly BindableProperty RoleTextProperty = BindableProperty.Create(nameof(RoleText), typeof(string), typeof(UserHeader), default(string), BindingMode.TwoWay);
		public static readonly BindableProperty ImagePathProperty = BindableProperty.Create(nameof(ImagePath), typeof(string), typeof(UserHeader), default(string), BindingMode.TwoWay);
		public string NameText
		{
			get => (string)GetValue(NameTextProperty);
			set => SetValue(NameTextProperty, value);
		}
		public string RoleText
		{
			get => (string)GetValue(RoleTextProperty);
			set => SetValue(RoleTextProperty, value);
		}
		public string ImagePath
		{
			get => (string)GetValue(ImagePathProperty);
			set => SetValue(ImagePathProperty, value);
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
			await Shell.Current.GoToAsync($"{nameof(UserDetailsPage)}?{UserDetailsViewModel.ID_URL}={Id}&{UserDetailsViewModel.NAV_URL}={NavigatedFrom}");
		}
	}
}
