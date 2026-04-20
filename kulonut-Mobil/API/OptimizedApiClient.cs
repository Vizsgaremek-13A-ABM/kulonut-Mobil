using Android.App;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace kulonut_Mobil.API
{
	public class FlexibleFloatConverter : JsonConverter<float>
	{
		public override float Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.Number)
			{
				return reader.GetSingle();
			}

			if (reader.TokenType == JsonTokenType.String)
			{
				var str = reader.GetString();

				if (float.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out float value))
					return value;
			}

			throw new JsonException("Invalid float format");
		}

		public override void Write(Utf8JsonWriter writer, float value, JsonSerializerOptions options)
		{
			writer.WriteNumberValue(value);
		}
	}
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
			try
			{
				using HttpResponseMessage response = await httpClient.GetAsync(url).ConfigureAwait(false);
				return await HandleResponse<T>(response, cacheKey);
			}
			catch (HttpRequestException ex)
			{
				logger.LogError(ex, "Network error in Put for URL {Url}", url);
				return default;
			}
			
		}

		public async Task<T?> PostAsync<T, TBody>(string url, TBody? body)
		{
			try
			{
				using HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, body).ConfigureAwait(false);
				return await HandleResponse<T>(response, null);
			}
			catch (HttpRequestException)
			{
				return default;
			}
			catch (DuplicateNameException)
			{
				throw;
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
				return default;
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
				if (response.StatusCode == System.Net.HttpStatusCode.RequestEntityTooLarge)
					throw new ApplicationException("Request entity too large (413).");
				logger.LogWarning("Request failed with status code {StatusCode}", response.StatusCode);
				return default;
			}

			try
			{
				using Stream content = await response.Content.ReadAsStreamAsync();
				T? result = await JsonSerializer.DeserializeAsync<T>(content, new JsonSerializerOptions { Converters = { new FlexibleFloatConverter() } });

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
