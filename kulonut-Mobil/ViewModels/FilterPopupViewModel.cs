using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kulonut_Mobil.Models;
using kulonut_Mobil.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.ViewModels
{
	public partial class FilterPopupViewModel : PopupViewModelBase
	{
		private readonly IDataService dataService;
		[ObservableProperty]
		private string error = string.Empty;
		[ObservableProperty]
		private FilterModel filterModel = new FilterModel();
		private bool isPolygonFilter = false;
		public FilterPopupViewModel(IDataService _dataService)
		{
			dataService = _dataService;
			
		}
		public void Initialize(bool _isPolygonFilter)
		{
			isPolygonFilter = _isPolygonFilter;
			if (isPolygonFilter && dataService.PolygonFilterModel != null)
				FilterModel = dataService.PolygonFilterModel;
			else if (!isPolygonFilter && dataService.ProjectFilterModel != null)
				FilterModel = dataService.ProjectFilterModel;
		}
		[RelayCommand]
		private async Task Filter()
		{
			if (!inputCheck())
				return;
			Error = "";
			if(isPolygonFilter)
				dataService.PolygonFilterModel = FilterModel;
			else
				dataService.ProjectFilterModel = FilterModel;
			await ClosePopup();
		}
		[RelayCommand]
		private void ClearBefore()
		{
			FilterModel.Before = null;
		}
		[RelayCommand]
		private void ClearAfter()
		{
			FilterModel.After = null;
		}
		private bool inputCheck()
		{
			if(FilterModel.Before == null && FilterModel.After == null && string.IsNullOrEmpty(FilterModel.name))
			{
				Error = "Valamelyik mezőt muszáj kitölteni";
				return false;
			}
			return true;
		}

	}
}
