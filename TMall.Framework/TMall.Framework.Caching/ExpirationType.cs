using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMall.Framework.Caching
{
    [Serializable]
    public enum ExpirationType
    {
        /// <summary>
        /// 滑动过期
        /// </summary>
        SlidingTime,

        /// <summary>
        /// 绝对过期
        /// </summary>
        AbsoluteTime,

        /// <summary>
        /// 从不过期
        /// </summary>
        Never
    }

    /// <summary>
    /// 缓存类型
    /// </summary>
    public enum CacheProviderType
    {
        /// <summary>
        /// 本地缓存
        /// </summary>
        LOCALMEMORYCACHE = 1,

        /// <summary>
        /// httpContext.Items 一次请求有效缓存
        /// </summary>
        HTTPPREREQUEST = 2,

        /// <summary>
        /// MemcachedCache分布式缓存
        /// </summary>
        MEMCACHEDCACHE = 4
    }
}
