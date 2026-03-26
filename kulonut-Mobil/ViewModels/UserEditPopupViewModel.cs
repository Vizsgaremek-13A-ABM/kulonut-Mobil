using kulonut_Mobil.Models;
using kulonut_Mobil.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	public partial class UserEditPopupViewModel : PopupViewModel
	{
		public string? PropName { get; set; }
		public UserModel User { get; set; }

		private readonly IUserService _userService;
		public UserEditPopupViewModel(IUserService userService)
		{
			_userService = userService;
			User = userService.GetCurrentUser()!;
		}
		public void Initialize(string propname)
		{
			PropName = propname;
		}
	}
}
