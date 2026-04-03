using kulonut_Mobil.API;
using kulonut_Mobil.Models;
using kulonut_Mobil.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Services
{
	public class DataService : IDataService
	{
		private IApiClient apiclient;
		private FilterModel? currentFilter;
		private FilterModel? filterModel;

		public FilterModel? FilterModel
		{
			get { return filterModel; }
			set { filterModel = value; currentFilter = value; }
		}
		public DataService(IApiClient _apiClient) 
		{
			apiclient = _apiClient;
		}
		public async Task<ProjectsResponseDTO?> GetProjects()
		{
			if(currentFilter == null)
				return await apiclient.GetWithCachingAsync<ProjectsResponseDTO>("projects", "AllProjects");
			
			ProjectsResponseDTO? dto = await apiclient.GetWithCachingAsync<ProjectsResponseDTO>("projects", "AllProjects");
			if (dto != null && dto.data != null)
				dto.data = dto.data.Where(projectFilterFunc).ToList();
			currentFilter = null;
			return dto;
		}
		public async Task<ProjectsByPolygonResponseDTO?> GetProjectsByPolygonId(int polygonId)
		{
			return await apiclient.GetWithCachingAsync<ProjectsByPolygonResponseDTO>($"polygons/{polygonId}", $"Polygons{polygonId}");
		}
		public async Task<ProjectResponseDTO?> GetProjectById(int projectId)
		{
			return await apiclient.GetWithCachingAsync<ProjectResponseDTO>($"projects/{projectId}", $"Project{projectId}");
		}
		public async Task<List<ProjectMapModel>?> GetProjectsForMap()
		{
			return await apiclient.GetWithCachingAsync<List<ProjectMapModel>>($"projects/map", "MapProjects");
		}
		public async Task<PolygonsResponseDTO?> GetPolygons()
		{
			if(currentFilter == null)
				return await apiclient.GetWithCachingAsync<PolygonsResponseDTO>("polygons", "AllPolygons");
				
			PolygonsResponseDTO? dto = await apiclient.GetWithCachingAsync<PolygonsResponseDTO>("polygons", "AllPolygons");
			if (dto != null && dto.data != null)
				dto.data = dto.data.Where(polygonFilterFunc).ToList();
			currentFilter = null;
			return dto;
		}
		public async Task<List<PolygonModel>?> GetPolygonsByProject(int projectId)
		{
			return await apiclient.GetWithCachingAsync<List<PolygonModel>>($"projects/{projectId}/polygons", $"ProjectPoly{projectId}");
		}
		private bool polygonFilterFunc(PolygonModel polygon)
		{
			return polygon.projects!.Any(projectFilterFunc);
		}
		private bool projectFilterFunc(ProjectMapModel project)
		{
			if (currentFilter!.after != null && currentFilter.after < project.plan_issue_date)
				return false;
			if (currentFilter!.before != null && currentFilter.before > project.plan_issue_date)
				return false;
			if (!string.IsNullOrEmpty(currentFilter.name) && !project!.name!.Contains(currentFilter.name, StringComparison.InvariantCultureIgnoreCase))
				return false;
			return true;
		}
		private bool projectFilterFunc(ProjectModel project)
		{
			if (currentFilter!.after != null && currentFilter.after < project.plan_issue_date)
				return false;
			if (currentFilter!.before != null && currentFilter.before > project.plan_issue_date)
				return false;
			if (!string.IsNullOrEmpty(currentFilter.name) && !project!.project_name!.Contains(currentFilter.name, StringComparison.InvariantCultureIgnoreCase))
				return false;
			Debug.WriteLine("True");
			return true;	
		}
	}
}
