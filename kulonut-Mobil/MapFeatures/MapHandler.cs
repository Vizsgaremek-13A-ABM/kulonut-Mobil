using kulonut_Mobil.Models;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Nts.Extensions;
using Mapsui.Projections;
using Mapsui.Styles;
using Mapsui.Tiling;
using Map = Mapsui.Map;

namespace kulonut_Mobil.MapFeatures
{
	public class MapHandler : IMapHandler
	{
		private readonly Map map;
		private MemoryLayer polyLayer;

		public MapHandler(Map _map)
		{
			map = _map;
		}

		public void CreateMap()
		{
			map.Layers.Add(OpenStreetMap.CreateTileLayer());
			polyLayer = new MemoryLayer { Name="PolygonLayer", Style = CreatePolyStyle() };
			map.Layers.Add(polyLayer);
			var gyor = SphericalMercator.FromLonLat(17.6504, 47.6875);
			map.Home = n => n.CenterOnAndZoomTo(gyor.ToMPoint(), 3.0);
		}

		public void ShowPolygons(List<PolygonModel> polygons)
		{
			polygons.ForEach(AddPolygon);
			polyLayer.DataHasChanged();
		}
		private void AddPolygon(PolygonModel polygonModel)
		{
			if (polygonModel.coordinates == null || !polygonModel.coordinates.Any())
				return;

			var coords = polygonModel.coordinates
				.Select(c => SphericalMercator.FromLonLat(c.lng, c.lat).ToCoordinate())
				.ToList();

			if (!coords.First().Equals2D(coords.Last()))
				coords.Add(coords.First());
			
			var ntsPolygon = coords.ToPolygon();
			var feature = new PolygonFeature(ntsPolygon, polygonModel.polygon_id);
			polyLayer.Features = polyLayer.Features.Append(feature).ToList();
		}
		private IStyle CreatePolyStyle() => new VectorStyle
		{
			Fill = new Mapsui.Styles.Brush(Mapsui.Styles.Color.FromArgb(80, 255, 165, 0)),
			Outline = new Pen(Mapsui.Styles.Color.Orange, 2)
		};
	}
}
