namespace kulonut_Mobil.Views
{
	public partial class DetailsLabel : ContentView
	{
		public static readonly BindableProperty BodyTextProperty = BindableProperty.Create(nameof(BodyText), typeof(string), typeof(DetailsLabel), default(string), BindingMode.TwoWay);
		public string BodyText
		{
			get => (string)GetValue(BodyTextProperty);
			set => SetValue(BodyTextProperty, value);
		}
		public double Spacing
		{
			get => ContainerLayout.Spacing;
			set => ContainerLayout.Spacing = value;
		}
		public double BodyFontSize
		{
			get => BodyLabel.FontSize;
			set => BodyLabel.FontSize = value;
		}
		public string HeaderText
		{
			get => HeaderLabel.Text;
			set => HeaderLabel.Text = value;
		}
		public double HeaderFontSize
		{
			get => HeaderLabel.FontSize;
			set => HeaderLabel.FontSize = value;
		}
		public DetailsLabel()
		{
			InitializeComponent();
		}
	}
}
