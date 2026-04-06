using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Models;
using kulonut_Mobil.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	public partial class UserEditPopupViewModel : PopupViewModelBase
	{
		private static readonly Dictionary<string, Func<UserModel, string?>> _propMap = new()
		{
			{ "Felhasználónév", u => u.name },
			{ "Megjelenő Név", u => u.display_name },
			{ "Email Cím", u => u.email }
		};
		private static readonly Dictionary<string, string> _fieldMap = new()
		{
			{ "Felhasználónév", "name" },
			{ "Megjelenő Név", "display_name" },
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
			if (!InputCheck())
				return;
			try
			{
				UserModel? updatedUser = await userService.UpdateCurrentUserAsync(requestBody);
				if (updatedUser == null)
				{
					ErrorText = $"Nem módosítható felhasználó {user.id} id-vel";
					return;
				}
				await ClosePopup();

			}
			catch (DuplicateNameException)
			{
				ErrorText = "Az adott email-cím már foglalt";
			}
		}
		private bool InputCheck()
		{
			if (string.IsNullOrWhiteSpace(PropValue))
			{
				ErrorText = $"{PropName} megadása kötelező.";
				return false;
			}
			string? error = GetError();

			if (error != null)
			{
				ErrorText = error;
				return false;
			}
			ErrorText = null;
			return true;
		}
		private string? GetError()
		{
			return PropName switch
			{
				"Email Cím" => Validation.InputValidator.ValidateEmail(PropValue),
				"Név" => Validation.InputValidator.ValidateName(PropValue),
				"Felhasználónév" => Validation.InputValidator.ValidateName(PropValue),
				_ => null
			};
		}
		private Dictionary<string, string> requestBody => 
			string.IsNullOrEmpty(PropValue) ||
			!_fieldMap.TryGetValue(PropName!, out var key)
			? new Dictionary<string, string>()
			: new Dictionary<string, string> { { key, PropValue } };
		private string? prop => _propMap.TryGetValue(PropName!, out var selector) ? selector(user) : null;
	}
}
