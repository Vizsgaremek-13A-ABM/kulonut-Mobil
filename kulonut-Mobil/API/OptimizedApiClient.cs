using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;

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
		public async Task<T?> PostAsync<T, TBody>(string url, TBody? body)
		{
			try
			{
				using HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, body).ConfigureAwait(false);
				Debug.WriteLine(url);
				return await HandleResponse<T>(response, null);
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error in Post: {ex}");
				return default;
			}
		}

		public void SetToken(string token)
		{
			httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
		}

		public async Task<T?> GetAsync<T>(string url, string? cacheKey = null)
		{
			using HttpResponseMessage response = await httpClient.GetAsync(url).ConfigureAwait(false);
			return await HandleResponse<T>(response, cacheKey);	
		}
		private async Task<T?> HandleResponse<T>(HttpResponseMessage response, string? cacheKey)
		{
			if (!response.IsSuccessStatusCode)
			{
				Debug.WriteLine(response.StatusCode);
				return default;
			}
			try
			{
				using Stream content = await response.Content.ReadAsStreamAsync();
				T? result = await JsonSerializer.DeserializeAsync<T>(content);
				if (result != null && cacheKey != null)
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
