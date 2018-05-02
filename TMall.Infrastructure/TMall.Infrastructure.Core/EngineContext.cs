using System;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TMall.Infrastructure.Core
{
    /// <summary>
    /// 引擎上下文中可以获取单例模式的引擎实例，启动初始化的入口
    /// </summary>
    public class EngineContext
    {
        #region Initialization Methods
        
        /// <summary>
        /// 初始化引擎，此方法已近设置为线程同步方法
        /// </summary>
        /// <param name="forceRecreate">是否强制重新初始化引擎，无论引擎是否已经初始化过</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(bool forceRecreate)
        {
            if (Singleton<IEngine>.Instance == null || forceRecreate)
            {
                //可以添加配置config，以便更好控制初始逻辑
                Debug.WriteLine("Constructing engine " + DateTime.Now);
                Singleton<IEngine>.Instance = DefaultEngine;
                Debug.WriteLine("Initializing engine " + DateTime.Now);
                Singleton<IEngine>.Instance.Initialize();
            }
            return Singleton<IEngine>.Instance;
        }

        /// <summary>
        /// 替换自定义上下文引擎
        /// </summary>
        /// <param name="engine"></param>
        public static void SetCurrent(IEngine engine)
        {
            Singleton<IEngine>.Instance = engine;
        }

        #endregion

        /// <summary>
        /// 默认的引擎
        /// </summary>
        private static IEngine DefaultEngine
        {
            get { return new DefaultEngine(); }
        }

        /// <summary>
        /// 获取单例形式的上下文引擎
        /// </summary>
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Initialize(false);
                }
                return Singleton<IEngine>.Instance;
            }
        }
    }
}
