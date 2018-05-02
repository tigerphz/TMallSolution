using System;
using System.Collections.Generic;

namespace TMall.Infrastructure.Core
{
    /// <summary>
    /// 单例模式存储
    /// </summary>
    /// <typeparam name="T">需要单例保存的对象</typeparam>
    public class Singleton<T> : Singleton
    {
        static T instance;

        /// <summary>返回类型T的实例，每一个类型T只会存在一个对应的实例.</summary>
        public static T Instance
        {
            get { return instance; }
            set
            {
                instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }
    }

    /// <summary>
    /// 单例模式存储集合.
    /// </summary>
    /// <typeparam name="T">需要单例保存的对象.</typeparam>
    public class SingletonList<T> : Singleton<IList<T>>
    {
        static SingletonList()
        {
            Singleton<IList<T>>.Instance = new List<T>();
        }

        /// <summary>返回类型T的实例，每一个类型T只会存在一个对应的实例.</summary>
        public new static IList<T> Instance
        {
            get { return Singleton<IList<T>>.Instance; }
        }
    }

    /// <summary>
    ///单例模式存储字典集合.
    /// </summary>
    /// <typeparam name="TKey">key.</typeparam>
    /// <typeparam name="TValue">value.</typeparam>
    public class SingletonDictionary<TKey, TValue> : Singleton<IDictionary<TKey, TValue>>
    {
        static SingletonDictionary()
        {
            Singleton<Dictionary<TKey, TValue>>.Instance = new Dictionary<TKey, TValue>();
        }

        /// <summary>返回类型T的实例，每一个类型T只会存在一个对应的实例.</summary>
        public new static IDictionary<TKey, TValue> Instance
        {
            get { return Singleton<Dictionary<TKey, TValue>>.Instance; }
        }
    }

    public class Singleton
    {
        static Singleton()
        {
            allSingletons = new Dictionary<Type, object>();
        }

        static readonly IDictionary<Type, object> allSingletons;

        /// <summary>字典保存对应类型的实例.</summary>
        public static IDictionary<Type, object> AllSingletons
        {
            get { return allSingletons; }
        }
    }
}
