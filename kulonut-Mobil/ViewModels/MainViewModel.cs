using CommunityToolkit.Mvvm.ComponentModel;
using kulonut_Mobil.API;
using kulonut_Mobil.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	public partial class MainViewModel : ObservableObject
	{
		private IApiClient apiClient;
		public MainViewModel(IApiClient _apiClient)
		{
		}
	}
}
