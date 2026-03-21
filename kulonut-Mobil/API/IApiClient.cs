namespace kulonut_Mobil.API
{
	public interface IApiClient
	{
		public Task<T?> GetWithCachingAsync<T>(string endpoint, string cacheKey);
		public Task<T?> PostAsync<T, TBody>(string endpoint, TBody body);

	}
}
