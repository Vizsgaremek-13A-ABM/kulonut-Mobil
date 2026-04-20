using kulonut_Mobil.API;
using kulonut_Mobil.Models;
using kulonut_Mobil.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Services
{
	public class AuthService : IAuthService
	{
		private IApiClient apiClient;
		private string? token;
		public const string TOKEN_KEY = "token";
		public const string REMEMBER_KEY = "remember";

        public AuthService(IApiClient _apiClient)
		{
			apiClient = _apiClient;
		}
		public async Task<UserModel?> Login(LoginRequestDTO request, bool remember)
		{
			LoginResponseDTO? response = await apiClient!.PostAsync<LoginResponseDTO, LoginRequestDTO>("auth/login", request);
			if (response != null && !string.IsNullOrEmpty(response.token))
			{
				token = response.token;
				SetToken(token);
				await SecureStorage.SetAsync(TOKEN_KEY, response.token);
				if (remember)
					await SecureStorage.SetAsync(REMEMBER_KEY, "1");
				else
					await SecureStorage.SetAsync(REMEMBER_KEY, "0");
				
			}
			return response?.user;
		}
		public async Task<UserModel?> Register(RegisterRequestDTO request)
		{
			LoginResponseDTO? response = await apiClient!.PostAsync<LoginResponseDTO, RegisterRequestDTO>("auth/register", request);
			if (response != null && response.token != null)
				SetToken(response.token);
			return response?.user;
		}
		public async Task<MessageResponseDTO?> ResendRegister()
		{
			return await apiClient.PostAsync<MessageResponseDTO?, object?>("email/verification-notification", null);
		}
		public async Task Logout()
		{
			await apiClient.PostAsync<MessageResponseDTO?, object?>("auth/logout", null);
			SecureStorage.Remove(TOKEN_KEY);
			token = null;
		}
		public async Task<string?> GetToken()
		{
			return await SecureStorage.GetAsync(TOKEN_KEY);
		}
		public bool IsAuthenticated()
		{
			if (string.IsNullOrEmpty(token)) return false;
			apiClient.SetToken(token);
			return true;
		}
		public void SetToken(string _token)
		{
			if(!string.IsNullOrEmpty(_token))
			{
				token = _token;
				apiClient.SetToken(_token);
			}
		}
		public async Task RequestPasswordReset(ForgotPasswordRequestDTO request)
		{
			await apiClient.PostAsync<MessageResponseDTO?, ForgotPasswordRequestDTO>("auth/forgot-password", request);
		}
		public async Task ChangePassword(ChangePasswordRequestDTO request)
		{
			var response = await apiClient.PostAsync<MessageResponseDTO?, ChangePasswordRequestDTO>("auth/update-password", request);
			if (response == null)
				throw new InvalidCredentialException("Rossz jelszó");
		}
	}
}
