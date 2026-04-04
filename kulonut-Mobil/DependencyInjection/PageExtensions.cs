using kulonut_Mobil.Pages;
using kulonut_Mobil.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.DependencyInjection
{
    public static class PageExtensions
    {
		public static IServiceCollection AddPages(this IServiceCollection services)
		{
			services.AddTransient<MainPage>()
			.AddSingleton<AppShell>()
			.AddTransient<RegisterPage>()
			.AddSingleton<MapPage>()
			.AddSingleton<TablePage>()
			.AddTransient<PasswordChangePage>()
			.AddTransient<ProjectDetailsPage>()
			.AddTransient<PasswordResetPage>()
			.AddSingleton<UserDetailsPage>()
			.AddSingleton<RegisterConfirmationPage>()
			.AddTransient<ProjectsPopup>()
			.AddTransient<UserEditPopup>()
			.AddTransient<FilterPopup>();
			return services;
		}
	}
}
