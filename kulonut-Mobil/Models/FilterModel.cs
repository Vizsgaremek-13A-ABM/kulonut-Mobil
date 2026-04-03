using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Models
{
	public class FilterModel
	{
		public string? name { get; set; }
		public DateTime? before { get; set; }
		public DateTime? after { get; set; }
	}
}
