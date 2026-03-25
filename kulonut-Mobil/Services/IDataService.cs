using kulonut_Mobil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Services
{
	public interface IDataService
	{
		public Task<List<ProjectModel>?> GetProjects();
		public Task<List<ProjectModel>?>GetProjectsByPolygonId(int polygonId);
		public Task<ProjectModel?> GetProjectById(int projectId);
		public Task<List<ProjectMapModel>?>GetProjectsForMap();
		public Task<List<PolygonModel>?> GetPolygons();
		public Task<List<PolygonModel>?> GetPolygonsByProject(int projectId);
	}
}
