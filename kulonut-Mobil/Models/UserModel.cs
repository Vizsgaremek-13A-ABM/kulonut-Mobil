using kulonut_Mobil.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Models
{
	public class UserModel
	{		
		public int id { get; set; }
		public string? name { get; set; }
		public string? display_name { get; set; }
		public string? email { get; set; }
		public string? avatar { get; set; }
		public RoleModel? role { get; set; }
		public DateTime joined_at { get; set; }
	}
}
