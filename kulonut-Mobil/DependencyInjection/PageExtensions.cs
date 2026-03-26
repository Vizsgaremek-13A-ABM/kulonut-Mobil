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
			services.AddSingleton<MainPage>()
			.AddTransient<AppShell>()
			.AddSingleton<RegisterPage>()
			.AddSingleton<MapPage>()
			.AddSingleton<TablePage>()
			.AddSingleton<PasswordChangePage>()
			.AddSingleton<ProjectDetailsPage>()
			.AddSingleton<PasswordResetPage>()
			.AddSingleton<UserDetailsPage>()
			.AddSingleton<RegisterConfirmationPage>()
			.AddTransient<ProjectsPopup>()
			.AddTransient<UserEditPopup>();
			return services;
		}
	}
}
