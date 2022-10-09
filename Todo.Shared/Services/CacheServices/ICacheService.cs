using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Shared.Services.CacheServices
{
    public interface ICacheService
    {
        void CreateCache<T>(string key, T data);
        bool GetCache<T>(string key, out T data);
        void Delete(string key);
    }
}
