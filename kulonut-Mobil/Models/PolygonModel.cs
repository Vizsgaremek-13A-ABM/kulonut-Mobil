using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Models
{
	public class PolygonModel
	{
		public int polygon_id { get; set; }
		public string? name { get; set; }
		public Coordinate[]? coordinates { get; set; }
		public ProjectMapModel[]? projects { get; set; }
	}
	public class Coordinate
	{
		public float latitude { get; set; }
		public float longitude { get; set; }
	}
}
