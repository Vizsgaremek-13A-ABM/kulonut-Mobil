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
		public const string ICON_NAME = "profile_icon";
		public event EventHandler? CurrentUserChanged;
		public UserService(IApiClient _apiClient)
		{
			apiClient = _apiClient;
		}

		public UserModel? GetCurrentUser()
		{
			return user;
		}
		public async Task<UserModel?> GetCurrentUserAsync(bool setCurrent = true)
		{
			GetUserResponseDTO? response = await apiClient.GetAsync<GetUserResponseDTO?> ("auth/user");
			if (response != null && response.data != null)
			{
				if(setCurrent)
					await SetCurrentUser(response.data);
				return response.data;
			}
			return default;
		}
		public async Task<UserModel?> UpdateCurrentUserAsync(Dictionary<string, string> request)
		{
			GetUserResponseDTO? response = await apiClient.PutAsync<GetUserResponseDTO?, Dictionary<string, string>>($"users/{user!.id}", request);
			if (response != null && response.data != null)
			{
				await SetCurrentUser(response.data);
				OnCurrentUserChanged();
				return response.data;
			}
			return default;
		}
		public async Task<UserModel?> UploadUserImageAsync(Stream stream, string fileName)
		{
			using var content = new MultipartFormDataContent
			{
				{ new StreamContent(stream), ICON_NAME, fileName } 
			};
			GetUserResponseDTO? response = await apiClient.UploadPhotoAsync<GetUserResponseDTO>($"users/{user!.id}/profile-icon", content);
			if(response != null && response.data != null)
			{
				await SetCurrentUser(response.data);
				OnCurrentUserChanged();
				return response.data;
			}
			return default;
		}
		public async Task SetCurrentUser(UserModel _user)
		{
			user = _user;
			await SecureStorage.SetAsync(USER_KEY, JsonSerializer.Serialize(_user));
 		}
		public void ClearCurrentUser()
		{
			user = null;
			SecureStorage.Remove(USER_KEY);
		}
		protected virtual void OnCurrentUserChanged()
		{
			CurrentUserChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
