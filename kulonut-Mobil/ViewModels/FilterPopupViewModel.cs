using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Models;
using kulonut_Mobil.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	public partial class FilterPopupViewModel : PopupViewModelBase
	{
		private readonly IDataService dataService;
		[ObservableProperty]
		private FilterModel filterModel = new FilterModel();
		public FilterPopupViewModel(IDataService _dataService)
		{
			dataService = _dataService;
			if(dataService.FilterModel != null) 
				FilterModel = dataService.FilterModel;
		}
		[RelayCommand]
		private async Task Filter()
		{
			if (!inputCheck())
				return;
			dataService.FilterModel = FilterModel;
			await ClosePopup();
		}
		private bool inputCheck()
		{
			return true;
		}

	}
}
