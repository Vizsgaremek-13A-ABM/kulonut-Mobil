using CommunityToolkit.Maui.Views;
using kulonut_Mobil.Models;
using kulonut_Mobil.Services;
using kulonut_Mobil.ViewModels;

namespace kulonut_Mobil.Popups;

public partial class UserEditPopup : Popup
{
	
	private IUserService userService;
	private readonly UserEditPopupViewModel userEditPopupViewModel;
    public UserEditPopup(UserEditPopupViewModel _userEditPopupViewModel)
	{
		InitializeComponent();
		userEditPopupViewModel = _userEditPopupViewModel;
		BindingContext = _userEditPopupViewModel;
		userEditPopupViewModel.OnClose += async result => await CloseAsync(result, CancellationToken.None);
	}
	public void Initialize(string propname)
	{
		userEditPopupViewModel.Initialize(propname);
	}
}