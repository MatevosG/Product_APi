using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Cache
{
    public interface ICacheRepository
    {
        bool AddCache(string cacheKey,object response);
        string GetCacheResponse(string cacheKey);
        object GetCacheOrTryCache(string cacheKey, object response);
        public void Refresh(string cacheKey);
    }
}
