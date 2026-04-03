namespace kulonut_Mobil.Views;

public partial class Loader : ContentView
{
	public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(nameof(IsLoading), typeof(bool), typeof(Loader), default(bool), BindingMode.TwoWay);
	public bool IsLoading
	{
		get => (bool)GetValue(IsLoadingProperty);
		set => SetValue(IsLoadingProperty, value);
	}
	public Loader()
	{
		InitializeComponent();
	}
}