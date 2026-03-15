using kulonut_Mobil.API;
using kulonut_Mobil.Pages;
using kulonut_Mobil.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using System.Net;

namespace kulonut_Mobil
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
				.ConfigureMauiHandlers(handlers =>
				{
#if ANDROID
					EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
					{
						handler.PlatformView.Background = null;
					});
#endif
				});

			builder.Services.AddMemoryCache();
			builder.Services.AddSingleton<ICacheService, DualLayerCacheService>();
			builder.Services.AddHttpClient(OptimizedApiClient.NAME, client =>
			{
				client.Timeout = TimeSpan.FromSeconds(5);
				client.DefaultRequestHeaders.Add("Accept", "application/json");
			})
			.ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
			{
				PooledConnectionLifetime = TimeSpan.FromMinutes(2),
				AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
				AllowAutoRedirect = false,
			});
			builder.Services.AddTransient<IApiClient, OptimizedApiClient>();

			builder.Services.AddSingleton<MainPage>();
			builder.Services.AddSingleton<MainViewModel>();
			builder.Services.AddSingleton<RegisterPage>();
			builder.Services.AddSingleton<MapPage>();
			builder.Services.AddSingleton<MapViewModel>();
			builder.Services.AddSingleton<TablePage>();
			builder.Services.AddSingleton<TableViewModel>();
			builder.Services.AddSingleton<ProjectDetailsPage>();
			builder.Services.AddSingleton<ProjectDetailsViewModel>();
			builder.Services.AddSingleton<UserDetailsPage>();
			builder.Services.AddSingleton<UserDetailsViewModel>();


#if DEBUG
			builder.Logging.AddDebug();
        #endif
			return builder.Build();
        }
    }
}
