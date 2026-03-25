using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Models;
using kulonut_Mobil.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	public partial class UserHeaderViewModel : BaseViewModel
	{
		protected IUserService userService;

		[ObservableProperty]
		private UserModel user;
		public UserHeaderViewModel(IUserService _userService)
		{
			userService = _userService;
		}

		[RelayCommand]
		private async Task HandleUserLoad()
		{
			UserModel? _user = await userService.GetCurrentUserAsync();
			if (_user == null) User = userService.GetCurrentUser()!;
			else User = _user;
		}
	}
}
