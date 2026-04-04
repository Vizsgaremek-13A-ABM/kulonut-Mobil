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
	public partial class UserViewModelBase : BaseViewModel
	{
		protected IUserService userService;

		[ObservableProperty]
		private UserModel? user;
		public UserViewModelBase(IUserService _userService)
		{
			userService = _userService;
		}

		[RelayCommand]
		protected async Task HandleUserLoad()
		{
			IsLoading = true;
			UserModel? _user = await userService.GetCurrentUserAsync();
			if (_user == null) User = userService.GetCurrentUser()!;
			else User = _user;
			IsLoading = false;
		}
	}
}
