using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	[QueryProperty(nameof(UserId), ID_URL)]
	public partial class UserDetailsViewModel : BaseViewModel
    {
		public const string ID_URL = "userId";
		public string UserId;
	}
}
