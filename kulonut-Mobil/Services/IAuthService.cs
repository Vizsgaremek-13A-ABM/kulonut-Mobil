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
		public Task<UserModel?> LoginAsync(LoginRequestDTO request, bool remember);
		public Task<UserModel?> RegisterAsync(RegisterRequestDTO request);
		public Task LogoutAsync();
		public void SetToken(string _token);
		public Task RequestPasswordResetAsync(ForgotPasswordRequestDTO request);
		public Task ChangePasswordAsync(ChangePasswordRequestDTO request);
		public Task<string?> GetTokenAsync();
		public bool IsAuthenticated();
	}
}
