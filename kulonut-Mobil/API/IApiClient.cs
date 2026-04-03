namespace kulonut_Mobil.API
{
	public interface IApiClient
	{
		public Task<T?> GetWithCachingAsync<T>(string url, string cacheKey);
		public Task<T?> GetAsync<T>(string url, string? cacheKey = null);
		public Task<T?> PostAsync<T, TBody>(string url, TBody body);
		public Task<T?> PutAsync<T, TBody>(string url, TBody body);
		public void SetToken(string token);

	}
}
