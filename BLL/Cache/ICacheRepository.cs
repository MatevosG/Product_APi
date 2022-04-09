using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Cache
{
    public interface ICacheRepository
    {
        T Get<T>(string key);
        T SetOrUpdate<T>(string key, T value);
        void Delete<T>(string key);
    }
}
