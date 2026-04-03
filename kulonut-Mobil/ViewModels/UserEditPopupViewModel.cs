using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Models;
using kulonut_Mobil.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	public partial class UserEditPopupViewModel : PopupViewModel
	{
		private static readonly Dictionary<string, Func<UserModel, string?>> _propMap = new()
		{
			{ "Név", u => u.name },
			{ "Felhasználónév", u => u.display_name },
			{ "Email Cím", u => u.email }
		};
		private static readonly Dictionary<string, string> _fieldMap = new()
		{
			{ "Név", "name" },
			{ "Felhasználónév", "user_model" },
			{ "Email Cím", "email" }
		};

		[ObservableProperty]
		private string? errorText;
		[ObservableProperty]
		private string? propName;
		[ObservableProperty]
		private string? propValue;
		private UserModel user { get; set; }

		private readonly IUserService userService;
		public UserEditPopupViewModel(IUserService _userService)
		{
			userService = _userService;
			user = _userService.GetCurrentUser()!;
		}
		public void Initialize(string propname)
		{
			PropName = propname;
			PropValue = prop;	
		}
		
		[RelayCommand]
		private async Task Submit()
		{
			UserModel? updatedUser = await userService.UpdateCurrentUserAsync(requestBody, user.id);
			if(updatedUser == null)
			{
				ErrorText = $"Nem módosítható felhasználó {user.id} id-vel";
				return;
			}
			user = updatedUser;
			await CloseAsync();
		}
		[RelayCommand]
		private async Task Cancel()
		{
			PropValue = string.Empty;
			await CloseAsync();
		}
		private Dictionary<string, string> requestBody => 
			string.IsNullOrEmpty(PropValue) ||
			!_fieldMap.TryGetValue(PropName!, out var key)
			? new Dictionary<string, string>()
			: new Dictionary<string, string> { { key, PropValue } };
		private string? prop => _propMap.TryGetValue(PropName!, out var selector) ? selector(user) : null;
	}
}
