using System.Data;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace kulonut_Mobil.API
{
	public class OptimizedApiClient : IApiClient
	{
		public const string NAME = "OptimizedClient";

		private readonly HttpClient httpClient;
		private readonly ICacheService cacheService;
		private readonly ILogger<OptimizedApiClient> logger;

		public OptimizedApiClient(ICacheService _cacheService, IHttpClientFactory _factory, ILogger<OptimizedApiClient> _logger)
		{
			cacheService = _cacheService;
			httpClient = _factory.CreateClient(NAME);
			logger = _logger;
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
				logger.LogError(ex, "Error in GetAsync for URL {Url}", url);
				return cachedData;
			}
		}

		public async Task<T?> GetAsync<T>(string url, string? cacheKey = null)
		{
			using HttpResponseMessage response = await httpClient.GetAsync(url).ConfigureAwait(false);
			return await HandleResponse<T>(response, cacheKey);
		}

		public async Task<T?> PostAsync<T, TBody>(string url, TBody? body)
		{
			try
			{
				using HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, body).ConfigureAwait(false);
				return await HandleResponse<T>(response, null);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error in Post for URL {Url}", url);
				return default;
			}
		}

		public async Task<T?> PutAsync<T, TBody>(string url, TBody? body)
		{
			try
			{
				using HttpResponseMessage response = await httpClient.PutAsJsonAsync(url, body).ConfigureAwait(false);
				return await HandleResponse<T>(response, null);
			}
			catch (HttpRequestException ex)
			{
				logger.LogError(ex, "Network error in Put for URL {Url}", url);
				throw;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Unexpected error in Put for URL {Url}", url);
				throw;
			}
		}

		public async Task<T?> UploadPhotoAsync<T>(string url, MultipartFormDataContent data)
		{
			try
			{
				using HttpResponseMessage response = await httpClient.PostAsync(url, data).ConfigureAwait(false);
				return await HandleResponse<T>(response, null);
			}
			catch (HttpRequestException ex)
			{
				logger.LogError(ex, "Network error while uploading photo to {Url}", url);
				throw;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Unexpected error uploading photo to {Url}", url);
				throw;
			}
		}

		public void SetToken(string token)
		{
			httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
		}

		private async Task<T?> HandleResponse<T>(HttpResponseMessage response, string? cacheKey)
		{
			if (!response.IsSuccessStatusCode)
			{
				if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
					throw new DuplicateNameException("Email is taken");
				logger.LogWarning("Request failed with status code {StatusCode}", response.StatusCode);
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
				logger.LogError(ex, "JSON deserialize failed");
				return default;
			}
		}
	}
}
