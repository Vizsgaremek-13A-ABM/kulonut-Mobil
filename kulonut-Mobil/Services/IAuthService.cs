using kulonut_Mobil.Models;
using kulonut_Mobil.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Services
{
    public interface IAuthService
    {
		public Task<UserModel?> Login(LoginRequestDTO request, bool remember);
		public Task<UserModel?> Register(RegisterRequestDTO request);
		public Task<MessageResponseDTO?> ResendRegister();
		public Task Logout();
		public void SetToken(string _token);
		public Task RequestPasswordReset(ForgotPasswordRequestDTO request);
		public Task ChangePassword(ChangePasswordRequestDTO request);
		public Task<string?> GetToken();
		public bool IsAuthenticated();
	}
}
