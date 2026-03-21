using kulonut_Mobil.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.DependencyInjection
{
    public static class ViewModelExtensions
    {
		public static IServiceCollection AddViewModels(this IServiceCollection services)
		{
			services.AddSingleton<MainViewModel>()
			.AddSingleton<RegisterViewModel>()
			.AddSingleton<MapViewModel>()
			.AddSingleton<TableViewModel>()
			.AddSingleton<PasswordChangeViewModel>()
			.AddSingleton<ProjectDetailsViewModel>()
			.AddSingleton<PasswordResetViewModel>()
			.AddSingleton<UserDetailsViewModel>();
			return services;
		}
	}
}
