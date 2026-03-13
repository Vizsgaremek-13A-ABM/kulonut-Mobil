using Android.Net.Http;
using kulonut_Mobil.Pages;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace kulonut_Mobil.API
{
	public class OptimizedApiClient : IApiClient
	{
		public const string NAME = "OptimizedClient";

		private readonly HttpClient httpClient;
		private readonly ICacheService cacheService;

		public OptimizedApiClient(ICacheService _cacheService, IHttpClientFactory _factory)
		{
			cacheService = _cacheService;
			httpClient = _factory.CreateClient(NAME);
		}

		public async Task<T?> GetWithCachingAsync<T>(string url, string cacheKey)
		{
			T? cachedData = await cacheService.GetAsync<T>(cacheKey);
			if (Connectivity.NetworkAccess != NetworkAccess.Internet)
				return cachedData;
			try
			{
				T? apiData = await GetAsync<T>(url, cacheKey);
				return apiData ?? cachedData;
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error in GetAsync: {ex.Message}");
				return cachedData;
			}
		}
		public async Task<T?> PostAsync<T, TBody>(string url, TBody body, string cacheKey)
		{
			try
			{
				using HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, body).ConfigureAwait(false);
				return await HandleResponse<T>(response, cacheKey);
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error in Post: {ex}");
				return default;
			}
		}
		private async Task<T?> GetAsync<T>(string url, string cacheKey)
		{
			using HttpResponseMessage response = await httpClient.GetAsync(url).ConfigureAwait(false);
			return await HandleResponse<T>(response, cacheKey);	
		}
		private async Task<T?> HandleResponse<T>(HttpResponseMessage response, string cacheKey)
		{
			if (!response.IsSuccessStatusCode)
				return default;

			try
			{
				using Stream content = await response.Content.ReadAsStreamAsync();
				T? result = await JsonSerializer.DeserializeAsync<T>(content);
				if (result != null)
					await cacheService.SetAsync(cacheKey, result);
				return result;
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"JSON deserialize failed {ex.Message}");
				return default;
			}
			
			
		}
	}
}
