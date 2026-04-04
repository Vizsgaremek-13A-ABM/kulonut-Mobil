using kulonut_Mobil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.MapFeatures
{
	public interface IMapHandler
	{
		public void CreateMap();
		public void ShowPolygons(List<PolygonModel> polygons);
		public Task Locate();
		public void ZoomTo(double lon, double lat, double resolution = 15);
	}
}
