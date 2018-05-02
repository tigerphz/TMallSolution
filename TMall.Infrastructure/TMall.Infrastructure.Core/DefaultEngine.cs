using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Autofac;
using TMall.Infrastructure.Core;
using TMall.Infrastructure.Core.DependencyManagement;
using TMall.Framework.ServiceLocation;
using TMall.Framework.Caching;

namespace TMall.Infrastructure.Core
{
    /// <summary>
    /// 系统默认上下文引擎
    /// </summary>
    public class DefaultEngine : IEngine
    {
        /// <summary>
        /// 服务定位器
        /// </summary>
        private IServiceLocator _serviceLocator;

        /// <summary>
        /// 服务定位器
        /// </summary>
        public IServiceLocator ServiceLocator
        {
            get { return _serviceLocator; }
        }

        /// <summary>
        /// Ioc管理
        /// </summary>
        public IContainer Container
        {
            get { return (_serviceLocator as ContainerManager).Container; }
        }

        /// <summary>
        /// 缓存管理
        /// </summary>
        private ICacheProvider _cacheProvider;

        /// <summary>
        /// 缓存管理
        /// </summary>
        public ICacheProvider CacheManager
        {
            get { return _cacheProvider; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DefaultEngine()
            : this(new ContainerConfigurer()) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configurer">Ioc容器配置用来注册类型</param>
        public DefaultEngine(ContainerConfigurer configurer)
        {
            InitializeContainer(configurer);
        }

        /// <summary>
        /// 初始化注册类型
        /// </summary>
        /// <param name="configurer"></param>
        private void InitializeContainer(ContainerConfigurer configurer)
        {
            var builder = new ContainerBuilder();
            _serviceLocator = new ContainerManager(builder.Build());
            configurer.Configure(this, _serviceLocator as ContainerManager);
        }

        /// <summary>
        /// 运行开始任务，需要继承IStartupTask接口
        /// </summary>
        private void RunStartupTasks()
        {
            var typeFinder = _serviceLocator.GetInstance<ITypeFinder>();
            var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            var startUpTasks = new List<IStartupTask>();
            foreach (var startUpTaskType in startUpTaskTypes)
                startUpTasks.Add((IStartupTask)Activator.CreateInstance(startUpTaskType));

            startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();

            foreach (var startUpTask in startUpTasks)
                startUpTask.Execute();
        }

        public void Initialize()
        {
            //本地缓存
            _cacheProvider = ServiceLocator.GetInstance<ICacheProvider>(CacheProviderType.LOCALMEMORYCACHE.ToString());

            RunStartupTasks();
        }

        public T Resolve<T>() where T : class
        {
            return ServiceLocator.GetInstance<T>();
        }

        public object Resolve(Type type)
        {
            return ServiceLocator.GetInstance(type);
        }

        public IList<T> ResolveAll<T>()
        {
            return ServiceLocator.GetAllInstances<T>() as IList<T>;
        }
    }
}
