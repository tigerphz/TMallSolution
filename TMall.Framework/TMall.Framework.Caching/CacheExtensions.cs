using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMall.Framework.Caching
{
    public static class CacheExtensions
    {
        public static T Get<T>(this ICacheProvider cacheManager, string key, string valKey, Func<T> acquire)
        {
            return Get(cacheManager, key, valKey, null, null, acquire);
        }

        public static T Get<T>(this ICacheProvider cacheManager, string key, string valKey, TimeSpan slidingExpiration, Func<T> acquire)
        {
            return Get(cacheManager, key, valKey, slidingExpiration, null, acquire);
        }

        public static T Get<T>(this ICacheProvider cacheManager, string key, string valKey, DateTime absoluteExpiration, Func<T> acquire)
        {
            return Get(cacheManager, key, valKey, null, absoluteExpiration, acquire);
        }

        private static T Get<T>(this ICacheProvider cacheManager, string key, string valKey, TimeSpan? slidingExpiration, DateTime? absoluteExpiration, Func<T> acquire)
        {
            if (cacheManager.Exists(key))
            {
                return cacheManager.Get<T>(key, valKey);
            }
            else
            {
                var result = acquire();
                if (slidingExpiration.HasValue)
                {
                    cacheManager.Add(key, valKey, result, slidingExpiration.Value);
                }
                else if (absoluteExpiration.HasValue)
                {
                    cacheManager.Add(key, valKey, result, absoluteExpiration.Value);
                }
                else
                {
                    cacheManager.Add(key, valKey, result);
                }

                return result;
            }
        }
    }
}
