using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Models.DTOs
{
	public class RegisterRequestDTO
	{
		public string? name { get; set; }
		public string? display_name { get; set; }
		public string? profile_icon { get; set; }
		public string? email { get; set; }
		public string? password { get; set; }
		public string? password_confirmation { get; set; }
	}
}
