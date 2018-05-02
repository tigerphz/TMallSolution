using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostSharp.Aspects;
using System.Reflection;
using PostSharp.Extensibility;
using TMall.Framework.Caching;
using TMall.Framework.ExceptionHanding;

namespace TMall.Infrastructure.Core.InterceptionBehaviors
{
    /// <summary>
    ///  表示由此特性所描述的方法，能够获得提供的缓存功能
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class CachingAttribute : OnMethodBoundaryAspect
    {
        //默认为60分钟
        private TimeSpan _expireTime = new TimeSpan(1, 0, 0); // 60 minutes

        //默认为绝对过期
        private ExpirationType _expiration = ExpirationType.AbsoluteTime;

        //缓存类型
        private CacheProviderType _cacheProviderType = CacheProviderType.LOCALMEMORYCACHE;

        /// <summary>
        /// 设置过期时间 ext: "01:00:00"
        /// </summary>
        public string ExpireTime
        {
            get { return _expireTime.Hours + ":" + _expireTime.Minutes + ":" + _expireTime.Seconds; }
            set { _expireTime = TimeSpan.Parse(value); }
        }

        /// <summary>
        /// 设置过期类型
        /// </summary>
        public ExpirationType ExpiryType
        {
            get { return _expiration; }
            set { _expiration = value; }
        }

        /// <summary>
        /// 设置缓存类型
        /// </summary>
        public CacheProviderType CacheProviderType
        {
            get { return _cacheProviderType; }
            set { _cacheProviderType = value; }
        }

        public CachingMethod Method { get; set; }

        /// <summary>
        /// 获取或设置一个<see cref="Boolean"/>值，该值表示当缓存方式为Put时，是否强制将值写入缓存中。
        /// </summary>
        public bool Force { get; set; }

        /// <summary>
        /// 获取或设置与当前缓存方式相关的方法名称。注：此参数仅在缓存方式为Remove时起作用。
        /// </summary>
        public string[] CorrespondingMethodNames { get; set; }

        /// <summary>
        /// 初始化一个新的<c>CachingAttribute</c>类型。
        /// </summary>
        /// <param name="method">缓存方式。</param>
        public CachingAttribute(CachingMethod method)
        {
            this.Method = method;
        }

        /// <summary>
        /// 初始化一个新的<c>CachingAttribute</c>类型。
        /// </summary>
        /// <param name="method">缓存方式。</param>
        /// <param name="correspondingMethodNames">与当前缓存方式相关的方法名称。注：此参数仅在缓存方式为Remove时起作用。</param>
        public CachingAttribute(CachingMethod method, params string[] correspondingMethodNames)
            : this(method)
        {
            this.CorrespondingMethodNames = correspondingMethodNames;
        }

        /// <summary>
        /// 根据指定的<see cref="CachingAttribute"/>以及<see cref="IMethodInvocation"/>实例，
        /// 获取与某一特定参数值相关的键名。
        /// </summary>
        /// <param name="cachingAttribute"><see cref="CachingAttribute"/>实例。</param>
        /// <param name="input"><see cref="IMethodInvocation"/>实例。</param>
        /// <returns>与某一特定参数值相关的键名。</returns>
        private string GetValueKey(MethodExecutionArgs eventArgs)
        {
            switch (Method)
            {
                // 如果是Remove，则不存在特定值键名，所有的以该方法名称相关的缓存都需要清除
                case CachingMethod.Remove:
                    return null;
                // 如果是Get或者Put，则需要产生一个针对特定参数值的键名
                case CachingMethod.Get:
                case CachingMethod.Put:
                    if (eventArgs.Arguments != null &&
                        eventArgs.Arguments.Count > 0)
                    {
                        var sb = new StringBuilder();
                        for (int i = 0; i < eventArgs.Arguments.Count; i++)
                        {
                            sb.Append(eventArgs.Arguments[i].ToString());
                            if (i != eventArgs.Arguments.Count - 1)
                                sb.Append("_");
                        }
                        return sb.ToString();
                    }
                    else
                        return "NULL";
                default:
                    throw ExceptionHelper.ThrowComponentException("无效的缓存方式,CachingMethod=" + Method.ToString());
            }
        }

        /// <summary>
        /// 编译时检测
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public override bool CompileTimeValidate(MethodBase method)
        {
            // 不支持构造函数缓存
            if (method is ConstructorInfo)
            {
                Message.Write(method, SeverityType.Error, "CX0001", "Cannot cache constructors.");
                return false;
            }

            MethodInfo methodInfo = (MethodInfo)method;

            // Does not support out parameters.
            ParameterInfo[] parameters = method.GetParameters();
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].IsOut)
                {
                    Message.Write(method, SeverityType.Error, "CX0003", "Cannot cache methods with return values.");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 进入方法时
        /// </summary>
        /// <param name="eventArgs"></param>
        public override void OnEntry(MethodExecutionArgs eventArgs)
        {
            string key = eventArgs.Method.DeclaringType == null ?
                            eventArgs.Method.Name :
                            eventArgs.Method.DeclaringType + "." + eventArgs.Method.Name;

            string valKey = GetValueKey(eventArgs);

            ICacheProvider cacheProvider = EngineContext.Current.ServiceLocator.GetInstance<ICacheProvider>(CacheProviderType.ToString());
            switch (Method)
            {
                case CachingMethod.Get:
                    if (cacheProvider.Exists(key, valKey))
                    {
                        var value = cacheProvider.Get(key, valKey);
                        eventArgs.ReturnValue = value;
                        eventArgs.FlowBehavior = FlowBehavior.Return;
                    }
                    else
                    {
                        eventArgs.MethodExecutionTag = new[] { key, valKey };
                    }
                    break;
                case CachingMethod.Put:
                    eventArgs.MethodExecutionTag = new[] { key, valKey };
                    break;
                case CachingMethod.Remove:
                    var removeKeys = CorrespondingMethodNames;
                    string rk = string.Empty;
                    foreach (var removeKey in removeKeys)
                    {
                        rk = eventArgs.Method.DeclaringType == null ? removeKey :
                            eventArgs.Method.DeclaringType + "." + removeKey;

                        if (cacheProvider.Exists(rk))
                            cacheProvider.Remove(rk);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 拦截方法执行完成后
        /// </summary>
        /// <param name="eventArgs"></param>
        public override void OnSuccess(MethodExecutionArgs eventArgs)
        {
            // 获取key
            string[] keys = (string[])eventArgs.MethodExecutionTag;
            if (keys == null || keys.Length != 2)
                return;

            string key = keys[0];
            string valKey = keys[1];
            var methodReturn = eventArgs.ReturnValue;
            ICacheProvider cacheProvider = EngineContext.Current.ServiceLocator.GetInstance<ICacheProvider>(CacheProviderType.ToString());

            switch (Method)
            {
                case CachingMethod.Get:
                    AddCache(cacheProvider, key, valKey, methodReturn);
                    break;
                case CachingMethod.Put:
                    if (cacheProvider.Exists(key))
                    {
                        if (Force)
                        {
                            cacheProvider.Remove(key);
                            AddCache(cacheProvider, key, valKey, methodReturn);
                        }
                        else
                            PutCache(cacheProvider, key, valKey, methodReturn);
                    }
                    else
                        AddCache(cacheProvider, key, valKey, methodReturn);
                    break;
                case CachingMethod.Remove:
                    var removeKeys = CorrespondingMethodNames;
                    string rk = string.Empty;

                    foreach (var removeKey in removeKeys)
                    {
                        rk = eventArgs.Method.DeclaringType == null ? removeKey :
                           eventArgs.Method.DeclaringType + "." + removeKey;

                        if (cacheProvider.Exists(rk))
                            cacheProvider.Remove(rk);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 添加过期时间
        /// </summary>
        /// <param name="cacheProvider"></param>
        /// <param name="key"></param>
        /// <param name="valKey"></param>
        /// <param name="value"></param>
        private void AddCache(ICacheProvider cacheProvider, string key, string valKey, object value)
        {
            if (_expiration == ExpirationType.AbsoluteTime)
            {
                cacheProvider.Add(key, valKey, value, DateTime.Now.Add(_expireTime));
            }
            else if (_expiration == ExpirationType.SlidingTime)
            {
                cacheProvider.Add(key, valKey, value, _expireTime);
            }
            else
            {
                cacheProvider.Add(key, valKey, value);
            }
        }

        /// <summary>
        /// 更新过期时间
        /// </summary>
        /// <param name="cacheProvider"></param>
        /// <param name="key"></param>
        /// <param name="valKey"></param>
        /// <param name="value"></param>
        private void PutCache(ICacheProvider cacheProvider, string key, string valKey, object value)
        {
            if (_expiration == ExpirationType.AbsoluteTime)
            {
                cacheProvider.Put(key, valKey, value, DateTime.Now.Add(_expireTime));
            }
            else if (_expiration == ExpirationType.SlidingTime)
            {
                cacheProvider.Put(key, valKey, value, _expireTime);
            }
            else
            {
                cacheProvider.Put(key, valKey, value);
            }
        }
    }
}
