using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TMall.Infrastructure.Data;

namespace TMall.Infrastructure.Data
{
    /// <summary>
    ///     数据单元操作接口
    /// </summary>
    public interface IUnitOfWorkContext : IUnitOfWork, IDisposable
    {
        /// <summary>
        ///   为指定的类型返回 System.Data.Entity.DbSet，这将允许对上下文中的给定实体执行 CRUD 操作。
        /// </summary>
        /// <typeparam name="TEntity"> 应为其返回一个集的实体类型。 </typeparam>
        /// <returns> 给定实体类型的 System.Data.Entity.DbSet 实例。 </returns>
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        ///   注册一个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        void RegisterNew<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        ///   批量注册多个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        void RegisterNew<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        /// <summary>
        ///   更改的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        void RegisterModified<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        ///   删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class;
    }
}