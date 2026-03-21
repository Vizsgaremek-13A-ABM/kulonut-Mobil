using Android.SE.Omapi;
using kulonut_Mobil.API;
using kulonut_Mobil.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.DependencyInjection
{
    public static class ServiceExtensions
    {
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddTransient<IApiClient, OptimizedApiClient>()
			.AddSingleton<IAuthService, AuthService>();
			return services;
		}
	}
}
