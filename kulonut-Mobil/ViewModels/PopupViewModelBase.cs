using CommunityToolkit.Mvvm.Input;

namespace kulonut_Mobil.ViewModels
{
	public delegate Task CloseHandler<T>(T result);
	public partial class PopupViewModelBase : BaseViewModel
	{
		public event CloseHandler<bool>? OnClose;
		[RelayCommand]
		protected async Task ClosePopup()
		{
			if(OnClose != null)
				await OnClose.Invoke(true);
		}
	}
}
