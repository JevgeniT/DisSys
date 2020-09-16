using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Cache
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IConnectionMultiplexer _connection;

        public ResponseCacheService(IConnectionMultiplexer connection)
        {
            _connection = connection;
        }
        
        
        public async Task CacheResponseAsync(string cacheKey, string cacheValue)
        {
           
            var db = _connection.GetDatabase();
            await db.StringSetAsync(cacheKey, cacheValue);
           
        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var db = _connection.GetDatabase();
            return await db.StringGetAsync(cacheKey);
        }
    }
}