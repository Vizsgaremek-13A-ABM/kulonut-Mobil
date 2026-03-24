using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Services
{
    public interface IPopupService
    {
        Task ShowErrorAsync(string message);
    }
}
