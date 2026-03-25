using Mapsui;
using Mapsui.UI;
using Mapsui.UI.Maui;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace kulonut_Mobil.MapFeatures
{
	public class PolygonClickBehavior : Behavior<MapControl>
	{
		private MapControl? mapControl;
		public static readonly BindableProperty CommandProperty =
			BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(PolygonClickBehavior), default(ICommand));

		public ICommand? Command
		{
			get => (ICommand?)GetValue(CommandProperty);
			set => SetValue(CommandProperty, value);
		}

		protected override void OnAttachedTo(MapControl bindable)
		{
			base.OnAttachedTo(bindable);
			mapControl = bindable;
			BindingContext = bindable.BindingContext;
			bindable.BindingContextChanged += OnMapControlBinding;
			bindable.Info += OnInfo;
		}

		protected override void OnDetachingFrom(MapControl bindable)
		{
			bindable.Info -= OnInfo;
			bindable.BindingContextChanged -= OnMapControlBinding;
			mapControl = null;
			base.OnDetachingFrom(bindable);
		}

		void OnMapControlBinding(object? sender, System.EventArgs e)
		{
			BindingContext = mapControl?.BindingContext;
		}

		private void OnInfo(object sender, MapInfoEventArgs e)
		{
			if (e.MapInfo!.Feature is PolygonFeature feature)
			{
				if (Command == null) Debug.WriteLine("Command is null");
				if (Command?.CanExecute(feature) == true)
					Command.Execute(feature);
				else Debug.WriteLine("Cant exec command");
			}
		}

	}
}
