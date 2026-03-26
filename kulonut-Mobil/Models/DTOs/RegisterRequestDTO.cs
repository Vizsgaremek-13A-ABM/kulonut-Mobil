using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Models.DTOs
{
	public class RegisterRequestDTO
	{
		public string? name { get; set; } = "bela";
		public string? email { get; set; } = "bela@gmail.com";
		public string? password { get; set; } = "password";
		public string? password_confirmation { get; set; } = "password";
	}
}
