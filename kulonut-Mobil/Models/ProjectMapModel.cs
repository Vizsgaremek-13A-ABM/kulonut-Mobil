using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Models
{
	public class ProjectMapModel
	{
		public int project_id { get; set; }
		public string? project_name { get; set; }
		public DateTime? plan_issue_date { get; set; }
	}
}
