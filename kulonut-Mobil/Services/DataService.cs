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
		private readonly IApiClient apiclient;
		public FilterModel? ProjectFilterModel { get; set; }
		public FilterModel? PolygonFilterModel { get; set; }

		public DataService(IApiClient _apiClient) 
		{
			apiclient = _apiClient;
		}
		public async Task<ProjectsResponseDTO?> GetProjects()
		{
			if(ProjectFilterModel == null)
				return await apiclient.GetWithCachingAsync<ProjectsResponseDTO>("projects", "AllProjects");
			
			ProjectsResponseDTO? dto = await apiclient.GetWithCachingAsync<ProjectsResponseDTO>("projects", "AllProjects");
			if (dto != null && dto.data != null)
				dto.data = dto.data.Where(projectFilterFunc).ToList();
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
			if(PolygonFilterModel == null)
				return await apiclient.GetWithCachingAsync<PolygonsResponseDTO>("polygons", "AllPolygons");
				
			PolygonsResponseDTO? dto = await apiclient.GetWithCachingAsync<PolygonsResponseDTO>("polygons", "AllPolygons");
			if (dto != null && dto.data != null)
				dto.data = dto.data.Where(polygon => polygon.projects!.Any(polygonFilterFunc)).ToList();
			return dto;
		}
		public async Task<PolygonsResponseDTO?> GetPolygonsByProject(int projectId)
		{
			return await apiclient.GetWithCachingAsync<PolygonsResponseDTO?>($"projects/{projectId}/polygons", $"ProjectPoly{projectId}");
		}
		private bool polygonFilterFunc(ProjectMapModel project)
		{
			if (PolygonFilterModel!.After != null && PolygonFilterModel.After > project.plan_issue_date)
				return false;
			if (PolygonFilterModel!.Before != null && PolygonFilterModel.Before < project.plan_issue_date)
				return false;
			if (!string.IsNullOrEmpty(PolygonFilterModel.name) && !project!.name!.Contains(PolygonFilterModel.name, StringComparison.InvariantCultureIgnoreCase))
				return false;
			return true;
		}
		private bool projectFilterFunc(ProjectModel project)
		{
			if (ProjectFilterModel!.After != null && ProjectFilterModel.After > project.plan_issue_date)
				return false;
			if (ProjectFilterModel!.Before != null && ProjectFilterModel.Before < project.plan_issue_date)
				return false;
			if (!string.IsNullOrEmpty(ProjectFilterModel.name) && !project!.project_name!.Contains(ProjectFilterModel.name, StringComparison.InvariantCultureIgnoreCase))
				return false;
			return true;	
		}
	}
}
