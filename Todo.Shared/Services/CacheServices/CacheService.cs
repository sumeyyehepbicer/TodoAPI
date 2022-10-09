using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Shared.Services.CacheServices
{
    public class CacheService : ICacheService
    {
        public readonly IMemoryCache _memoryCache;
        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public void CreateCache<T>(string key, T data)
        {
            _memoryCache.Set(key, data, TimeSpan.FromHours(1));
        }

        public void Delete(string key)
        {
            _memoryCache.Remove(key);
        }

        public bool GetCache<T>(string key, out T data)
        {
            return _memoryCache.TryGetValue(key, out data);
        }
    }
}
