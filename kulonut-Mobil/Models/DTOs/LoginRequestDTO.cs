using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Models.DTOs
{
	public class LoginRequestDTO
	{
		public string? Email { get; set; } = "user1@example.com";
		public string? Password { get; set; } = "password";
	}
}
