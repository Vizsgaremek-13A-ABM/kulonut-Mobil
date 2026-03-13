using CommunityToolkit.Mvvm.ComponentModel;
using kulonut_Mobil.API;

namespace kulonut_Mobil.ViewModels
{
	public partial class MainViewModel : ObservableObject
	{
		private IApiClient apiClient;

		[ObservableProperty]
		private string bindTest;
		public MainViewModel(IApiClient _apiClient)
		{
			apiClient = _apiClient;
			BindTest = "BindTest";
		}
	}
}
