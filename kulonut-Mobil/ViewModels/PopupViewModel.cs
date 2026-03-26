using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	public delegate Task CloseHandler<T>(T result);
	public partial class PopupViewModel : BaseViewModel
	{
		public event CloseHandler<bool>? OnClose;
		public async Task CloseAsync()
		{
			if(OnClose != null)
				await OnClose.Invoke(true);
		}
	}
}
