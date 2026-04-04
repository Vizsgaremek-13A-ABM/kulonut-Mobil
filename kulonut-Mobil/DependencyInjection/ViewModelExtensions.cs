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
			services.AddTransient<MainViewModel>()
			.AddTransient<AppShellViewModel>()
			.AddTransient<RegisterViewModel>()
			.AddSingleton<MapViewModel>()
			.AddSingleton<TableViewModel>()
			.AddTransient<PasswordChangeViewModel>()
			.AddTransient<ProjectDetailsViewModel>()
			.AddTransient<PasswordResetViewModel>()
			.AddSingleton<UserDetailsViewModel>()
			.AddTransient<ProjectsPopupViewModel>()
			.AddTransient<UserEditPopupViewModel>()
			.AddTransient<FilterPopupViewModel>();
			return services;
		}
	}
}
