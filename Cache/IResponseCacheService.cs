using System;
using System.Threading.Tasks;

namespace Cache
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, string cacheValue);

        Task<string> GetCachedResponseAsync(string cacheKey);
    }
}