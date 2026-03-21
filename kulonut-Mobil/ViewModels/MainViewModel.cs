using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.API;
using kulonut_Mobil.Pages;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	public partial class MainViewModel : BaseViewModel
	{
		private IApiClient apiClient;

		[ObservableProperty]
		private string bindTest;
		public MainViewModel(IApiClient _apiClient)
		{
			apiClient = _apiClient;
			BindTest = "BindTest";
		}
		[RelayCommand]
		private async Task NavigateToMap()
		{
			await Shell.Current.GoToAsync($"//{nameof(MapPage)}");
		}
		[RelayCommand]
		private async Task NavigateToRegister()
		{
			await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
		}
	}
}
