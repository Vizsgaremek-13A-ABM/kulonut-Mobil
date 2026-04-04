using kulonut_Mobil.Models;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Nts.Extensions;
using Mapsui.Projections;
using Mapsui.Styles;
using Mapsui.Tiling;
using System.Threading.Tasks;
using Map = Mapsui.Map;

namespace kulonut_Mobil.MapFeatures
{
	public class MapHandler : IMapHandler
	{
		private readonly Map map;
		private MemoryLayer? polyLayer;
		private MyLocationLayer? locationLayer;

		public MapHandler(Map _map)
		{
			map = _map;
		}

		public void CreateMap()
		{
			map.Layers.Add(OpenStreetMap.CreateTileLayer());
			locationLayer = new MyLocationLayer(map)
			{
				Enabled = true,
				Opacity = 1f,
			};
			map.Layers.Add(locationLayer);
			polyLayer = new MemoryLayer { Name="PolygonLayer", Style = CreatePolyStyle(), IsMapInfoLayer = true };
			map.Layers.Add(polyLayer);
			var gyor = SphericalMercator.FromLonLat(17.6504, 47.6875);
			map.Home = n => n.CenterOnAndZoomTo(gyor.ToMPoint(), 15, 500, Mapsui.Animations.Easing.CubicOut);
		}

		public void ShowPolygons(List<PolygonModel> polygons)
		{
			polyLayer!.Features = new List<IFeature>();
			polygons.ForEach(AddPolygon);
			polyLayer.DataHasChanged();
		}
		public async Task Locate()
		{
			PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
			if (status != PermissionStatus.Granted)
				status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
			
			if (status != PermissionStatus.Granted)
				throw new Exception("No permission");
			
			GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
			Location? location = await Geolocation.Default.GetLocationAsync(request);
			if (location == null)
				throw new Exception("GeoLocator not working");
			MPoint userPos = SphericalMercator.FromLonLat(location.Longitude, location.Latitude).ToMPoint();
			locationLayer!.UpdateMyLocation(userPos);
			map.Navigator.CenterOnAndZoomTo(userPos, 15, 500, Mapsui.Animations.Easing.CubicOut);				
		}
		private void AddPolygon(PolygonModel polygonModel)
		{
			if (polygonModel.coordinates == null || !polygonModel.coordinates.Any())
				return;

			var coords = polygonModel.coordinates
				.Select(c => SphericalMercator.FromLonLat(c.longitude, c.latitude).ToCoordinate())
				.ToList();

			if (!coords.First().Equals2D(coords.Last()))
				coords.Add(coords.First());
			
			var ntsPolygon = coords.ToPolygon();
			var feature = new PolygonFeature(ntsPolygon, polygonModel.polygon_id);
			polyLayer!.Features = polyLayer.Features.Append(feature).ToList();
		}
		private IStyle CreatePolyStyle() => new VectorStyle
		{
			Fill = new Mapsui.Styles.Brush(Mapsui.Styles.Color.FromArgb(80, 255, 165, 0)),
			Outline = new Pen(Mapsui.Styles.Color.Orange, 2)
		};
	}
}
