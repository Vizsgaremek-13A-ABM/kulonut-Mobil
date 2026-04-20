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
		public string? email_verified_at { get; set; }
		public DateTime? joined_at { get; set; }
		public string? display_avatar
		{
			get
			{
				if (string.IsNullOrWhiteSpace(avatar) || avatar == "default-avatar.png")
					return "default_avatar";
				return avatar;
			}
		}
		public string formatted_name
		{
			get
			{
				string? _name = string.IsNullOrWhiteSpace(display_name) ? name : display_name;
				if (string.IsNullOrEmpty(_name))
					return string.Empty;

				const int maxLength = 15;

				return _name.Length > maxLength ? _name.Substring(0, maxLength - 3) + "..." : _name;
			}
		}
	}
}
