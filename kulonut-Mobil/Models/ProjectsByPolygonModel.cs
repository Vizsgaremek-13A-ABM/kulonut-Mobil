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
        public string? name { get; set; }
        public List<ProjectMapModel>? projects { get; set; }
    }
}
