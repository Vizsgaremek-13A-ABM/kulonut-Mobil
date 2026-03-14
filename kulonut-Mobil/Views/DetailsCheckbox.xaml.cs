namespace kulonut_Mobil.Views
{
	public partial class DetailsCheckbox : ContentView
	{
		public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(DetailsLabel), default(bool), BindingMode.TwoWay);
		public bool IsChecked
		{
			get => (bool)GetValue(IsCheckedProperty);
			set => SetValue(IsCheckedProperty, value);
		}
		public DetailsCheckbox()
		{
			InitializeComponent();
		}
		public string HeaderText
		{
			get => HeaderLabel.Text;
			set => HeaderLabel.Text = value;
		}
	}
}
