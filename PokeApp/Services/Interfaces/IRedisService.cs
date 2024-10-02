namespace PokeApp.Services.Interfaces
{
    public interface IRedisService
    {
        Task<T> GetCachedDataAsync<T>(string key);
        Task SetCachedDataAsync<T>(string key, T data, TimeSpan? expiry = null);
    }
}