using kulonut_Mobil.API;
using kulonut_Mobil.Pages;
using kulonut_Mobil.ViewModels;
using Microsoft.Extensions.Logging;
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


#if DEBUG
			builder.Logging.AddDebug();
        #endif
			return builder.Build();
        }
    }
}
