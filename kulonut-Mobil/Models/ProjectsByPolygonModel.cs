using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Models
{
    public class ProjectsByPolygonModel
    {
        public int polygon_id { get; set; }
        public string? polygon_name { get; set; }
        public List<ProjectMapModel>? projects { get; set; }
        public List<Coordinate>? coordinates { get; set; }
    }
}
