using kulonut_Mobil.API;
using kulonut_Mobil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Services
{
	public class DataService : IDataService
	{
		private IApiClient apiclient;
		public DataService(IApiClient _apiClient) 
		{
			apiclient = _apiClient;
		}
		public async Task<List<ProjectModel>?> GetProjects()
		{
			return await apiclient.GetWithCachingAsync<List<ProjectModel>>("projects", "AllProjects");
		}
		public async Task<List<ProjectModel>?> GetProjectsByPolygonId(int polygonId)
		{
			return await apiclient.GetWithCachingAsync<List<ProjectModel>>($"polygon/project/{polygonId}", $"Polygons{polygonId}");
		}
		public async Task<ProjectModel?> GetProjectById(int projectId)
		{
			return await apiclient.GetWithCachingAsync<ProjectModel>($"projects/{projectId}", $"Project{projectId}");
		}
		public async Task<List<ProjectMapModel>?> GetProjectsForMap()
		{
			return await apiclient.GetWithCachingAsync<List<ProjectMapModel>>($"projects/map", "MapProjects");
		}
		public async Task<List<PolygonModel>?> GetPolygons()
		{
			return await apiclient.GetWithCachingAsync<List<PolygonModel>>("polygons", "AllPolygons");
		}
		public async Task<List<PolygonModel>?> GetPolygonsByProject(int projectId)
		{
			return await apiclient.GetWithCachingAsync<List<PolygonModel>>($"projects/{projectId}/polygons", $"ProjectPoly{projectId}");
		}

	}
}
