using kulonut_Mobil.Models;
using kulonut_Mobil.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Services
{
	public interface IDataService
	{
		public Task<ProjectsResponseDTO?> GetProjects();
		public Task<ProjectsByPolygonResponseDTO?> GetProjectsByPolygonId(int polygonId);
		public Task<ProjectResponseDTO?> GetProjectById(int projectId);
		public Task<List<ProjectMapModel>?>GetProjectsForMap();
		public Task<PolygonsResponseDTO?> GetPolygons();
		public Task<List<PolygonModel>?> GetPolygonsByProject(int projectId);
	}
}
