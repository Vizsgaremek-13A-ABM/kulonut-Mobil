using kulonut_Mobil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.Services
{
    public interface IUserService
    {
		public Task<UserModel?> GetCurrentUserAsync(bool setCurrent = true);
		public UserModel? GetCurrentUser();
		public Task<UserModel?> GetCurrentUserFromStorageAsync(bool setCurrent = true);
		public Task<UserModel?> UpdateCurrentUserAsync(Dictionary<string, string> request);
		public Task<UserModel?> UploadUserImageAsync(Stream stream, string fileName);
		public Task SetCurrentUser(UserModel _user);
		public void ClearCurrentUser();
	}
}
