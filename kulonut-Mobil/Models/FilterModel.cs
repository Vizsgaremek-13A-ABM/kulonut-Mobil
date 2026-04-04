using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Models
{
	public partial class FilterModel : ObservableObject
	{
		public string? name { get; set; }
		[ObservableProperty]
		private DateTime? before;
		[ObservableProperty]
		private DateTime? after;
	}
}
