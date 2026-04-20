using kulonut_Mobil.Models;
using kulonut_Mobil.Pages;
using kulonut_Mobil.ViewModels;
using System.Diagnostics;

namespace kulonut_Mobil.Views
{
	public partial class UserHeader : ContentView
	{
		public static readonly BindableProperty UserProperty = BindableProperty.Create(nameof(User), typeof(UserModel), typeof(UserHeader), default(UserModel), BindingMode.TwoWay, propertyChanged: OnUserChanged);
		private static void OnUserChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (UserHeader)bindable;
			control.OnPropertyChanged(nameof(DisplayNameOrFallback));
		}
		public UserModel User
		{
			get => (UserModel)GetValue(UserProperty);
			set => SetValue(UserProperty, value);
		}
		public string DisplayNameOrFallback
		{
			get
			{
				var name = string.IsNullOrWhiteSpace(User?.display_name)
					? User?.name
					: User?.display_name;

				if (string.IsNullOrEmpty(name))
					return string.Empty;

				const int maxLength = 20;

				return name.Length > maxLength
					? name.Substring(0, maxLength - 3) + "..."
					: name;
			}
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
