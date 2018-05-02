using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.Infrastructure.Core.DependencyManagement;
using TMall.Framework.ServiceLocation;
using Autofac;
using TMall.Framework.Caching;

namespace TMall.Infrastructure.Core
{
    /// <summary>
    /// 上下文引擎
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// 服务定位器
        /// </summary>
        IServiceLocator ServiceLocator { get; }

        /// <summary>
        /// Ioc容器管理
        /// </summary>
        IContainer Container { get; }

        /// <summary>
        /// 缓存管理
        /// </summary>
        ICacheProvider CacheManager { get; }

        /// <summary>
        /// 初始化组件
        /// </summary>
        /// <param name="config">Config</param>
        void Initialize();

        T Resolve<T>() where T : class;

        object Resolve(Type type);

        IList<T> ResolveAll<T>();
    }
}
