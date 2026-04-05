using Android.Content.PM;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Text.Json;

namespace kulonut_Mobil.API
{
	public class DualLayerCacheService : ICacheService
	{
		private readonly IMemoryCache memoryCache;
		private readonly string localCachePath = FileSystem.CacheDirectory;
		private readonly ILogger<DualLayerCacheService> logger;
		public DualLayerCacheService(IMemoryCache _memoryCache, ILogger<DualLayerCacheService> _logger)
		{
			memoryCache = _memoryCache;
			logger = _logger;
		}

		public async Task<T?> GetAsync<T>(string key)
		{
			if (memoryCache.TryGetValue(key, out T? memoryValue))
				return memoryValue;

			string filePath = GetfilePath(key);
			if (File.Exists(filePath))
				return await GetFromFile<T>(key, filePath);
			return default;
		}
		private async Task<T?> GetFromFile<T>(string key, string filePath)
		{
			try
			{
				using FileStream stream = File.OpenRead(filePath);
				T? diskValue = await JsonSerializer.DeserializeAsync<T>(stream);
				if (diskValue != null)
					return memoryCache.Set(key, diskValue, TimeSpan.FromMinutes(10));
			}
			catch (JsonException ex)
			{
				logger.LogWarning(ex, "Invalid JSON in cache file {FilePath}", filePath);
				if (File.Exists(filePath))
					File.Delete(filePath);
			}
			catch (IOException ex)
			{
				logger.LogWarning(ex, "I/O error reading cache file {FilePath}", filePath);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Unexpected error reading cache file {FilePath}", filePath);
			}
			return default;
		}
		private string GetfilePath(string key)
		{
			return Path.Combine(localCachePath, $"{key}.json");
		}
		public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
		{
			if (string.IsNullOrEmpty(key)) return;
			memoryCache.Set(key, value, expiration ?? TimeSpan.FromMinutes(30));
			using FileStream stream = File.Create(GetfilePath(key));
			await JsonSerializer.SerializeAsync(stream, value);
		}
	}
}
