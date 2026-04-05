using kulonut_Mobil.ViewModels;

namespace kulonut_Mobil.DependencyInjection
{
    public static class ViewModelExtensions
    {
		public static IServiceCollection AddViewModels(this IServiceCollection services)
		{
			services.AddTransient<MainViewModel>()
			.AddSingleton<AppShellViewModel>()
			.AddTransient<RegisterViewModel>()
			.AddSingleton<MapViewModel>()
			.AddSingleton<TableViewModel>()
			.AddTransient<PasswordChangeViewModel>()
			.AddTransient<ProjectDetailsViewModel>()
			.AddTransient<PasswordResetViewModel>()
			.AddSingleton<UserDetailsViewModel>()
			.AddTransient<ProjectsPopupViewModel>()
			.AddTransient<UserEditPopupViewModel>()
			.AddSingleton<RegisterConfirmationViewModel>()
			.AddTransient<FilterPopupViewModel>();
			return services;
		}
	}
}
