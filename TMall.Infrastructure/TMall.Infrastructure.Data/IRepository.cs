// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs" company="CIK">
//   Copyright by Thang Chung
// </copyright>
// <summary>
//   Defines the SortOrder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TMall.Infrastructure.SearchModel;

namespace TMall.Infrastructure.Data
{
    /// <summary>
    /// The Repository interface.
    /// </summary>
    public interface IRepository<TEntity> where TEntity : class
    {
        #region 属性

        /// <summary>
        ///     获取 当前实体的查询数据集
        /// </summary>
        IQueryable<TEntity> Entities { get; }

        #endregion

        #region 公共方法

        /// <summary>
        ///     插入实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Insert(TEntity entity, bool isSave = true);

        /// <summary>
        ///     批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Insert(IEnumerable<TEntity> entities, bool isSave = true);

        /// <summary>
        ///     删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(object id, bool isSave = true);

        /// <summary>
        ///     删除实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(TEntity entity, bool isSave = true);

        /// <summary>
        ///     删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(IEnumerable<TEntity> entities, bool isSave = true);

        /// <summary>
        ///     删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true);

        /// <summary>
        ///     更新实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Update(TEntity entity, bool isSave = true);

        /// <summary>
        /// 按需更新字段
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">包含部分需要更新字段的值的实体</param>
        /// <param name="modifiedProperty">描述哪些字段需要更新</param>
        void UpdateEntity(TEntity entity, params string[] modifiedProperty);

        /// <summary>
        ///     查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        TEntity GetByKey(object key);

        /// <summary>
        /// 分页查询信息
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="searchCondition">查询条件</param>
        /// <param name="PageInfo">分页信息</param>
        /// <returns></returns>
        SearchPageInfo<TEntity> GetPageEntities(SearchPageInfo<TEntity> searchPageInfo, Expression<Func<TEntity, bool>> criteria = null);

        /// <summary>
        /// 是否存在实体
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        bool IsExistEntity(Expression<Func<TEntity, bool>> criteria);

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <value>The unit of work.</value>
        IUnitOfWork UnitOfWork { get; }

        #endregion
    }
}
