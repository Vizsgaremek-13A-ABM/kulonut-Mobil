using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Models
{
	public class ProjectModel
	{
		public int id { get; set; }
		public string project_name { get; set; }
		public string work_number { get; set; }
		public string folder_number { get; set; }
		public string designer { get; set; }
		public string general_designer { get; set; }
		public string client { get; set; }
		public string geodesy { get; set; }
		public string plan_issue_date { get; set; }
		public string eutility_statement_issue_date { get; set; }
		public string road_construction_permit_date { get; set; }
		public string water_rights_permit_date { get; set; }
		public bool road_construction_plan { get; set; }
		public bool water_network_plan { get; set; }
		public bool sewage_plan { get; set; }
		public bool stormwater_drainage_plan { get; set; }
		public bool public_lighting_plan { get; set; }
		public string other_work_parts { get; set; }
		public string notes { get; set; }
		public int min_role_level { get; set; }
	}
}
