using kulonut_Mobil.API;
using kulonut_Mobil.Models;
using kulonut_Mobil.Models.DTOs;
using Org.Apache.Http.Impl.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			if (!string.IsNullOrEmpty(user_str))
			{
				if(!setCurrent) 
					return JsonSerializer.Deserialize<UserModel?>(user_str);
				else
					user = JsonSerializer.Deserialize<UserModel?>(user_str);
			}
			return user;
		}
		public async Task<UserModel?> GetCurrentUserAsync(bool setCurrent = true)
		{
			GetUserResponseDTO? response = await apiClient.GetAsync<GetUserResponseDTO?> ("user");
			if (response != null && response.data != null)
			{
				if(setCurrent)
					await SetCurrentUser(response.data);
				return response.data;
			}
			Debug.WriteLine("No User Data Found");
			return default;
		}

		public async Task SetCurrentUser(UserModel _user)
		{
			user = _user;
			await SecureStorage.SetAsync(USER_KEY, JsonSerializer.Serialize(_user));
 		}

		public async Task<UserModel?> UpdateCurrentUserAsync(Dictionary<string, string> request, int userId)
		{
			GetUserResponseDTO? response = await apiClient.PutAsync<GetUserResponseDTO?, Dictionary<string, string>>($"users/{userId}", request);
			if (response != null && response.data != null)
			{
				await SetCurrentUser(response.data);
				return response.data;
			}
			
			Debug.WriteLine($"No User With Id {userId} found");
			return default;
		}
		public void ClearCurrentUser()
		{
			user = null;
			SecureStorage.Remove(USER_KEY);
		}
	}
}
