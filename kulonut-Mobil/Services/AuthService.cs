using kulonut_Mobil.API;
using kulonut_Mobil.Models;
using kulonut_Mobil.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Services
{
	public class AuthService : IAuthService
	{
		private IApiClient apiClient;
		public string? token;
		public const string TOKEN_KEY = "token";

        public AuthService(IApiClient _apiClient)
		{
			apiClient = _apiClient;
			Task.Run(async () =>
			{
				token = await SecureStorage.GetAsync(TOKEN_KEY);
			});
		}
		public async Task<UserModel?> LoginAsync(LoginRequestDTO request, bool remember)
		{
			LoginResponseDTO? response = await apiClient!.PostAsync<LoginResponseDTO, LoginRequestDTO>("auth/login", request);
			Debug.WriteLine(response?.message);
			if (response != null && response.token != null)
			{
				token = response.token;
				if(remember)
				{
					await SecureStorage.SetAsync(TOKEN_KEY, response.token);

				}
			}
			return response?.user;
		}
		public async Task<UserModel?> RegisterAsync(RegisterRequestDTO request)
		{
			LoginResponseDTO? response = await apiClient!.PostAsync<LoginResponseDTO, RegisterRequestDTO>("/auth/register", request);
			if(response != null && response.token != null)
				token = response.token;
			return response?.user;
		}
		public async Task LogoutAsync()
		{
			await apiClient.PostAsync<LogoutResponseDTO?, object?>("/auth/logout", null);
			SecureStorage.Remove(TOKEN_KEY);
			token = null;
		}
		public async Task<string?> GetTokenAsync()
		{
			return await SecureStorage.GetAsync(TOKEN_KEY);
		}

		public bool IsAuthenticated()
		{
			return token != null && IsJwtValid();
		}

		private bool IsJwtValid()
		{
			if (string.IsNullOrWhiteSpace(token))
				return false;
			try
			{
				var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
				return jwt.ValidTo > DateTime.UtcNow.AddMinutes(1);
			}
			catch
			{
				return false;
			}
		}
		public Task RequestPasswordResetAsync(ForgotPasswordRequestDTO request)
		{
			throw new NotImplementedException();
		}
		public Task ChangePasswordAsync(ChangePasswordRequestDTO request)
		{
			throw new NotImplementedException();
		}
	}
}
