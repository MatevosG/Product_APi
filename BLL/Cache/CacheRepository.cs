using DAL.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Cache
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _redis ;
        public CacheRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        
        public async Task  Test(string cacheKey, object response)
        {
            var db = _redis.GetDatabase();
            var foo = await db.StringGetAsync(cacheKey);
            return foo  
        }
        public  bool AddCache(string cacheKey, object response)
        {
            if (response == null)
                return false ; 

            var serializedResponse = JsonConvert.SerializeObject(response);
              _distributedCache.SetString(cacheKey,serializedResponse);
            return true;
        }
        
        public string GetCacheResponse(string cacheKey)
        {
            var cacheResponse = _distributedCache.GetString(cacheKey);
            if(string.IsNullOrEmpty(cacheResponse))
                return null;
            return cacheResponse;
        }
        public object GetCacheOrTryCache(string cacheKey, object response)
        {
            var cacheResponse = GetCacheResponse(cacheKey);
            if (!string.IsNullOrEmpty(cacheResponse))
                return cacheResponse;
            var cache = AddCache( cacheKey,  response);
            return cache;
        }
        public void Refresh(string cacheKey)
        {
            _distributedCache.Refresh(cacheKey);
        }
    }
}
