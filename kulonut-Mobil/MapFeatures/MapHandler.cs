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
using Polygon = NetTopologySuite.Geometries.Polygon;

namespace kulonut_Mobil.MapFeatures
{
	public class MapHandler : IMapHandler
	{
		private readonly Map map;
		private MemoryLayer? polyLayer;
		private MyLocationLayer? locationLayer;

		public const double DEFAULT_RESOLUTION = 15;
		public const int DEFAULT_ANIM_TIME = 500;


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
				throw new UnauthorizedAccessException("Location permission was denied.");

			GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
			Location? location = await Geolocation.Default.GetLocationAsync(request);
			if (location == null)
				throw new InvalidOperationException("Unable to retrieve location.");
			ZoomTo(location.Longitude, location.Latitude);
		}
		public void ZoomTo(double lon, double lat, double resolution = DEFAULT_RESOLUTION)
		{
			MPoint userPos = SphericalMercator.FromLonLat(lon, lat).ToMPoint();
			locationLayer!.UpdateMyLocation(userPos);
			map.Navigator.CenterOnAndZoomTo(userPos, resolution, DEFAULT_ANIM_TIME, Mapsui.Animations.Easing.CubicOut);
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

			Polygon ntsPolygon = coords.ToPolygon();
			PolygonFeature feature = new PolygonFeature(ntsPolygon, polygonModel.polygon_id);
			polyLayer!.Features = polyLayer.Features.Append(feature).ToList();
		}
		private IStyle CreatePolyStyle() => new VectorStyle
		{
			Fill = new Mapsui.Styles.Brush(Mapsui.Styles.Color.FromArgb(100, 109, 165, 242)),
			Outline = new Pen(Mapsui.Styles.Color.FromArgb(160, 109, 165, 242), 2)
		};
	}
}
