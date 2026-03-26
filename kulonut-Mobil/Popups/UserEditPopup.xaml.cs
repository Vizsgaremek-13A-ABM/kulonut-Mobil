using CommunityToolkit.Maui.Views;
using kulonut_Mobil.Models;
using kulonut_Mobil.Services;

namespace kulonut_Mobil.Popups;

public partial class UserEditPopup : Popup
{
	public string PropName { get; set; }
	public UserModel User { get; set; }
	private IUserService userService;
    public UserEditPopup(string propname, IUserService _userService)
	{
		InitializeComponent();
		BindingContext = this;
		PropName = propname;
		userService = _userService;
		User = userService.GetCurrentUser()!;
    }
}