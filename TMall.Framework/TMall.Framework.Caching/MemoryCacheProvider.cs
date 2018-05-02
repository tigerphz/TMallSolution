using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;

namespace TMall.Framework.Caching
{
    /// <summary>
    /// 内存缓存提供器
    /// </summary>
    public class MemoryCacheProvider : ICacheProvider
    {
        /// <summary>
        /// MemoryCacheCache
        /// </summary>
        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        /// <summary>
        /// 向缓存中添加一个对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <param name="value">需要缓存的对象。</param>
        public void Add(string key, string valKey, object value)
        {
            Dictionary<string, object> dict = null;
            if (Cache.Contains(key))
            {
                dict = (Dictionary<string, object>)Cache[key];
                dict[valKey] = value;
            }
            else
            {
                dict = new Dictionary<string, object>();
                dict.Add(valKey, value);
            }

            var policy = new CacheItemPolicy();
            policy.Priority = CacheItemPriority.NotRemovable;

            Cache.Add(new CacheItem(key, dict), policy);
        }

        /// <summary>
        /// 向缓存中添加一个对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <param name="value">需要缓存的对象。</param>
        /// <param name="value">活动过期时间。</param>
        public void Add(string key, string valKey, object value, TimeSpan slidingExpiration)
        {
            Dictionary<string, object> dict = null;
            if (Cache.Contains(key))
            {
                dict = (Dictionary<string, object>)Cache[key];
                dict[valKey] = value;
            }
            else
            {
                dict = new Dictionary<string, object>();
                dict.Add(valKey, value);
            }

            var policy = new CacheItemPolicy();
            policy.Priority = CacheItemPriority.Default;
            policy.SlidingExpiration = slidingExpiration;

            Cache.Add(new CacheItem(key, dict), policy);
        }

        /// <summary>
        /// 向缓存中添加一个对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <param name="value">需要缓存的对象。</param>
        /// <param name="value">绝对过期时间。</param>
        public void Add(string key, string valKey, object value, DateTime absoluteExpiration)
        {
            Dictionary<string, object> dict = null;
            if (Cache.Contains(key))
            {
                dict = (Dictionary<string, object>)Cache[key];
                dict[valKey] = value;
            }
            else
            {
                dict = new Dictionary<string, object>();
                dict.Add(valKey, value);
            }

            var policy = new CacheItemPolicy();
            policy.Priority = CacheItemPriority.Default;
            policy.AbsoluteExpiration = absoluteExpiration;

            Cache.Add(new CacheItem(key, dict), policy);
        }

        /// <summary>
        /// 向缓存中更新一个对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <param name="value">需要缓存的对象。</param>
        public void Put(string key, string valKey, object value)
        {
            Add(key, valKey, value);
        }

        /// <summary>
        /// 向缓存中更新一个对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <param name="value">需要缓存的对象。</param>
        /// <param name="value">活动过期时间。</param>
        public void Put(string key, string valKey, object value, TimeSpan slidingExpiration)
        {
            Add(key, valKey, value, slidingExpiration);
        }

        /// <summary>
        /// 向缓存中更新一个对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <param name="value">需要缓存的对象。</param>
        /// <param name="value">绝对过期时间。</param>
        public void Put(string key, string valKey, object value, DateTime absoluteExpiration)
        {
            Add(key, valKey, value, absoluteExpiration);
        }

        /// <summary>
        /// 从缓存中读取对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <returns>被缓存的对象。</returns>
        public T Get<T>(string key, string valKey)
        {
            var value = Get(key, valKey);
            return value == null ? default(T) : (T)value;
        }

        /// <summary>
        /// 从缓存中读取对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <returns>被缓存的对象。</returns>
        public object Get(string key, string valKey)
        {
            if (Cache.Contains(key))
            {
                Dictionary<string, object> dict = (Dictionary<string, object>)Cache[key];
                if (dict != null && dict.ContainsKey(valKey))
                    return dict[valKey];
                else
                    return null;
            }
            return null;
        }

        /// <summary>
        /// 从缓存中移除对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        /// 获取一个<see cref="Boolean"/>值，该值表示拥有指定键值的缓存是否存在。
        /// </summary>
        /// <param name="key">指定的键值。</param>
        /// <returns>如果缓存存在，则返回true，否则返回false。</returns>
        public bool Exists(string key)
        {
            return Cache.Contains(key);
        }

        /// <summary>
        /// 获取一个<see cref="Boolean"/>值，该值表示拥有指定键值和缓存值键的缓存是否存在。
        /// </summary>
        /// <param name="key">指定的键值。</param>
        /// <param name="valKey">缓存值键。</param>
        /// <returns>如果缓存存在，则返回true，否则返回false。</returns>
        public bool Exists(string key, string valKey)
        {
            return Cache.Contains(key) &&
                ((Dictionary<string, object>)Cache[key]).ContainsKey(valKey);
        }

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        public void FlushAll()
        {
            foreach (var item in Cache)
                Remove(item.Key);
        }
    }
}
