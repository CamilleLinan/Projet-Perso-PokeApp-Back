using PokeApp.Services.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace PokeApp.Services
{
	public class RedisService : IRedisService
	{ 
        private readonly IConnectionMultiplexer _redis;

		public RedisService(IConnectionMultiplexer redis)
		{
			_redis = redis;
		}

        public async Task<T> GetCachedDataAsync<T>(string key)
        {
            var db = _redis.GetDatabase();
            var cachedData = await db.StringGetAsync(key);

            if (!cachedData.HasValue)
                return default;

            return JsonSerializer.Deserialize<T>(cachedData);
        }

        public async Task SetCachedDataAsync<T>(string key, T data, TimeSpan? expiry = null)
        {
            var db = _redis.GetDatabase();
            var jsonData = JsonSerializer.Serialize(data);
            await db.StringSetAsync(key, jsonData, expiry);
        }
    }
}