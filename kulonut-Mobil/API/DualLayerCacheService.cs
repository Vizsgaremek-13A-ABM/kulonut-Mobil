using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using System;
using System.Diagnostics;

namespace kulonut_Mobil.API
{
	public class DualLayerCacheService : ICacheService
	{
		private readonly IMemoryCache memoryCache;
		private readonly string localCachePath = FileSystem.CacheDirectory;

		public DualLayerCacheService(IMemoryCache memoryCache)
		{
			this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
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
			catch (Exception)
			{
				Debug.WriteLine("Error parsing from file cache");
			}
			return default;
		}
		private string GetfilePath(string key)
		{
			return Path.Combine(localCachePath, $"{key}.json");
		}
		public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
		{
			memoryCache.Set(key, value, expiration ?? TimeSpan.FromMinutes(30));
			using FileStream stream = File.Create(GetfilePath(key));
			await JsonSerializer.SerializeAsync(stream, value);
		}
	}
}
