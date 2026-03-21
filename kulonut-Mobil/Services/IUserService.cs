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
		public Task<UserModel?> GetCurrentUserAsync();
		public UserModel? GetCurrentUser();
		public Task<UserModel> UpdateCurrentUserAsync(UserModel? request);
		public void SetCurrentUser(UserModel _user);
		public void ClearCurrentUser();
	}
}
