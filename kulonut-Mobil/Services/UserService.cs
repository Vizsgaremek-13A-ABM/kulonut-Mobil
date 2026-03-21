using kulonut_Mobil.API;
using kulonut_Mobil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Services
{
    public class UserService : IUserService
    {
        private UserModel? user;
		private IApiClient apiClient;
		public UserService(IApiClient _apiClient)
		{
			apiClient = _apiClient;
		}

		public UserModel? GetCurrentUser()
		{
			return user;
		}

		public async Task<UserModel?> GetCurrentUserAsync()
		{
			return await apiClient.GetWithCachingAsync<UserModel?>("/users", "user");
		}

		public void SetCurrentUser(UserModel _user)
		{
			user = _user;
 		}

		public Task<UserModel> UpdateCurrentUserAsync(UserModel? request)
		{
			throw new NotImplementedException();
		}
		public void ClearCurrentUser()
		{
			user = null;
		}
	}
}
