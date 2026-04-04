using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Models.DTOs
{
	public class ChangePasswordRequestDTO
	{
		public string? current_password { get; set; }
		public string? password { get; set; }
		public string? password_confirmation { get; set; }
	}
	public class ForgotPasswordRequestDTO
	{
		public string? email { get; set; }
	}
}
