using kulonut_Mobil.API;
using kulonut_Mobil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace kulonut_Mobil.Services
{
    public class UserService : IUserService
    {
        private UserModel? user;
		private IApiClient apiClient;
		public const string USER_KEY = "user";
		public UserService(IApiClient _apiClient)
		{
			apiClient = _apiClient;
		}

		public UserModel? GetCurrentUser()
		{
			return user;
		}
		public async Task<UserModel?> GetCurrentUserFromStorageAsync(bool setCurrent = true)
		{
			string? user_str = await SecureStorage.GetAsync(USER_KEY);
			if (user_str != null)
				user = JsonSerializer.Deserialize<UserModel?>(user_str);
			return user;
		}
		public async Task<UserModel?> GetCurrentUserAsync(bool setCurrent = true)
		{
			UserModel? response = await apiClient.GetWithCachingAsync<UserModel?>("user", "user");
			if (response != null && setCurrent)
				await SetCurrentUser(response);
			return response;
		}

		public async Task SetCurrentUser(UserModel _user)
		{
			user = _user;
			await SecureStorage.SetAsync(USER_KEY, JsonSerializer.Serialize(_user));
 		}

		public Task<UserModel> UpdateCurrentUserAsync(UserModel? request)
		{
			throw new NotImplementedException();
		}
		public void ClearCurrentUser()
		{
			user = null;
			SecureStorage.Remove(USER_KEY);
		}
	}
}
