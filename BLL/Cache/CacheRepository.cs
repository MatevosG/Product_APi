using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
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

        public CacheRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public  bool CacheResponse(string cacheKey, object response)
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
    }
}
