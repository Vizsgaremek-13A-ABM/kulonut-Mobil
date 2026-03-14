namespace kulonut_Mobil.Views
{
	public partial class AuthEntry : ContentView
	{
	
		public static readonly BindableProperty UnFocusColorProperty = BindableProperty.Create(nameof(UnFocusColor), typeof(Brush), typeof(AuthEntry), default(Brush));
		public static readonly BindableProperty FocusColorProperty = BindableProperty.Create(nameof(FocusColor), typeof(Brush), typeof(AuthEntry), default(Brush));
		public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(AuthEntry), default(string), BindingMode.TwoWay);
		public Brush? UnFocusColor
		{
			get => (Brush)GetValue(UnFocusColorProperty);
			set => SetValue(UnFocusColorProperty, value);
		}
		public Brush? FocusColor
		{
			get => (Brush)GetValue(FocusColorProperty);
			set => SetValue(FocusColorProperty, value);
		}
		public string Text
		{
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}
		public string LabelText
		{
			get => HeadLabel.Text;
			set => HeadLabel.Text = value;
		}
		public bool IsPassword
		{
			get => TextInput.IsPassword;
			set => TextInput.IsPassword = value;
		}
		public string Placeholder
		{
			get => TextInput.Placeholder;
			set => TextInput.Placeholder = value;
		}

		public AuthEntry()
		{
			InitializeComponent();
			TextInput.Focused += (s, e) =>
			{
				InputBorder.Stroke = FocusColor;
			};

			TextInput.Unfocused += (s, e) =>
			{
				InputBorder.Stroke = UnFocusColor;
			};
		}
	}
}
