using kulonut_Mobil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Services
{
    public interface IPopupService
    {
        public Task ShowErrorAsync(string message);
        public Task ShowPolygonAsync(ProjectsByPolygonModel projectsForPolygon);
        public Task ShowUserEditAsync(string propname);
    }
}
