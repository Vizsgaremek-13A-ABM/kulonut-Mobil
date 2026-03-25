using Mapsui.Nts;
using Polygon = NetTopologySuite.Geometries.Polygon;

namespace kulonut_Mobil.MapFeatures
{
	public class PolygonFeature : GeometryFeature
	{
		public int Id { get; set; }
		public PolygonFeature(Polygon polygon, int id)
		{
			Id = id;
			Geometry = polygon;
		}
	}
}
