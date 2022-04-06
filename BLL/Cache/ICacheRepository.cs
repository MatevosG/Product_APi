using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Cache
{
    public interface ICacheRepository
    {
        bool CacheResponse(string cacheKey,object response);
        string GetCacheResponse(string cacheKey);
    }
}
