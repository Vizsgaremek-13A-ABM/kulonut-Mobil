using Mapsui;
using Mapsui.UI.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace kulonut_Mobil.MapFeatures
{
	public class PolygonClickBehavior : Behavior<MapControl>
	{
		public static readonly BindableProperty CommandProperty =
			BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(PolygonClickBehavior));

		public ICommand Command
		{
			get => (ICommand)GetValue(CommandProperty);
			set => SetValue(CommandProperty, value);
		}

		protected override void OnAttachedTo(MapControl bindable)
		{
			base.OnAttachedTo(bindable);
			bindable.Info += OnInfo;
		}

		protected override void OnDetachingFrom(MapControl bindable)
		{
			bindable.Info -= OnInfo;
			base.OnDetachingFrom(bindable);
		}

		private void OnInfo(object sender, MapInfoEventArgs e)
		{
			var feature = e.MapInfo?.Feature;

			if (feature?.RenderedGeometry is Polygon)
			{
				if (Command?.CanExecute(feature) == true)
					Command.Execute(feature);
			}
		}

	}
}
