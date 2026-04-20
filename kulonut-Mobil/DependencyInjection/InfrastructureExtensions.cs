using kulonut_Mobil.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace kulonut_Mobil.DependencyInjection
{
    public static class InfrastructureExtensions
    {
		public static IServiceCollection AddInfrastructure(this IServiceCollection services)
		{
			services.AddMemoryCache()
			.AddSingleton<ICacheService, DualLayerCacheService>()
			.AddHttpClient(OptimizedApiClient.NAME, client =>
			{
				client.Timeout = TimeSpan.FromSeconds(15);
				client.DefaultRequestHeaders.Add("Accept", "application/json");
				client.BaseAddress = new Uri("https://kulonutapi.jcloud.jedlik.cloud/api/");
			})
			.ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
			{
				PooledConnectionLifetime = TimeSpan.FromMinutes(2),
				AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
				AllowAutoRedirect = false,
			});
			return services;
		}
	}
}
