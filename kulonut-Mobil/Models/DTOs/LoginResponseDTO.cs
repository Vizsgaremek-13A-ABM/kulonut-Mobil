using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Models.DTOs
{
	public class LoginResponseDTO
	{		
		public string? message { get; set; }
		public string? token { get; set; }
		public string? token_type { get; set; }
		public UserModel? user { get; set; }
	}
}
