
namespace TMall.Infrastructure.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Objects;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Entity.Design.PluralizationServices;
    using TMall.Infrastructure.Data;
    using TMall.Framework.Data;
    using System.Data.Entity.Infrastructure;
    using TMall.Infrastructure.SearchModel;
    using System.Reflection;
    using System.Data.Objects.DataClasses;
    using TMall.Infrastructure.Utility;

    /// <summary>
    /// Generic repository
    /// </summary>
    public class EfRepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext _context;
        private IUnitOfWork _unitOfWork;
        private readonly PluralizationService _pluralizer = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en"));

        #region 属性

        /// <summary>
        /// Initializes a new instance of the  class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EfRepositoryBase(DbContext context)
        {
            Check.NotNull(context, "context");

            _context = context;
        }

        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork ?? (_unitOfWork = new EFRepositoryContext(_context)); }
        }

        /// <summary>
        /// 获取或设置 EntityFramework的数据仓储上下文
        /// </summary>
        protected IUnitOfWorkContext EFContext
        {
            get
            {
                if (UnitOfWork is IUnitOfWorkContext)
                {
                    return UnitOfWork as IUnitOfWorkContext;
                }
                throw new ArgumentException(string.Format("数据仓储上下文对象类型不正确，应为IRepositoryContext，实际为 {0}", UnitOfWork.GetType().Name));
            }
        }

        /// <summary>
        ///     获取 当前实体的查询数据集
        /// </summary>
        public virtual IQueryable<TEntity> Entities
        {
            get { return EFContext.Set<TEntity>(); }
        }

        #endregion

        #region 公共方法

        /// <summary>
        ///     插入实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Insert(TEntity entity, bool isSave = true)
        {
            Check.NotNull(entity, "entity");
            EFContext.RegisterNew(entity);
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        ///     批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Insert(IEnumerable<TEntity> entities, bool isSave = true)
        {
            Check.NotNull(entities, "entities");
            EFContext.RegisterNew(entities);
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        ///     删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(object id, bool isSave = true)
        {
            Check.NotNull(id, "id");
            TEntity entity = EFContext.Set<TEntity>().Find(id);
            return entity != null ? Delete(entity, isSave) : 0;
        }

        /// <summary>
        ///     删除实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(TEntity entity, bool isSave = true)
        {
            Check.NotNull(entity, "entity");
            EFContext.RegisterDeleted(entity);
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        ///     删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(IEnumerable<TEntity> entities, bool isSave = true)
        {
            Check.NotNull(entities, "entities");
            EFContext.RegisterDeleted(entities);
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        ///     删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true)
        {
            Check.NotNull(predicate, "predicate");
            List<TEntity> entities = EFContext.Set<TEntity>().Where(predicate).ToList();
            return entities.Count > 0 ? Delete(entities, isSave) : 0;
        }

        /// <summary>
        ///     更新实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Update(TEntity entity, bool isSave = true)
        {
            Check.NotNull(entity, "entity");
            EFContext.RegisterModified(entity);
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        /// 按需更新实体,需要调用commit保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="modifiedProperty"></param>
        public virtual void UpdateEntity(TEntity entity, params string[] modifiedProperty)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                EFContext.Set<TEntity>().Attach(entity);
            }

            var stateEntry = ((IObjectContextAdapter)_context)
                .ObjectContext
                .ObjectStateManager
                .GetObjectStateEntry(entity);

            for (int i = 0; i < modifiedProperty.Length; i++)
            {
                stateEntry.SetModifiedProperty(modifiedProperty[i]);
            }
        }

        /// <summary>
        /// 查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        public virtual TEntity GetByKey(object key)
        {
            Check.NotNull(key, "key");
            return EFContext.Set<TEntity>().Find(key);
        }

        /// <summary>
        /// 是否存在实体
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public bool IsExistEntity(Expression<Func<TEntity, bool>> criteria)
        {
            return this.Entities.Count(criteria) > 0;
        }

        /// <summary>
        /// 分页查询信息
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="searchCondition">查询条件</param>
        /// <param name="PageInfo">分页信息</param>
        /// <returns></returns>
        public virtual SearchPageInfo<TEntity> GetPageEntities(SearchPageInfo<TEntity> searchPageInfo, Expression<Func<TEntity, bool>> criteria = null)
        {
            var query = criteria == null ? Entities : Entities.Where(criteria);
            query = searchPageInfo.SearchCondition == null || searchPageInfo.SearchCondition.Items.Count == 0 ?
                                        query : query.Where(searchPageInfo.SearchCondition);

            //查询出总数
            searchPageInfo.PageInfo.TotalCount = query.Count();

            //设置排序
            if (!string.IsNullOrEmpty(searchPageInfo.PageInfo.OrderField))
                query = query.OrderBy(searchPageInfo.PageInfo.OrderField,
                    searchPageInfo.PageInfo.OrderDirection == SortOrder.desc);

            query = query.Skip<TEntity>(searchPageInfo.PageInfo.PageSize * (searchPageInfo.PageInfo.PageIndex - 1)) //越过多少条 
                        .Take<TEntity>(searchPageInfo.PageInfo.PageSize); //取出多少条 


            searchPageInfo.DataList = query.ToList();
            return searchPageInfo;
        }

        #endregion
    }
}
