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
        public CacheRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public T Get<T>(string key)
        {
            var value = _distributedCache.GetString(key);

            if (value != null)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default(T);
        }
        public T SetOrUpdate<T>(string key, T value)
        {
            _distributedCache.SetString(key, JsonConvert.SerializeObject(value));
            return value;
        }

        public void Delete<T>(string key)
        {
            _distributedCache.Remove(key);
        }
    }
}
