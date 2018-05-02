//-----------------------------------------------------------------------
// <copyright file="IQueryableExtensions.cs">
// </copyright>
// <author>Zou Jian</author>
// <addtime>2010-09-03</addtime>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;

namespace TMall.Infrastructure.SearchModel
{
    /// <summary>
    /// 对IQueryable的扩展方法
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// zoujian add , 使IQueryable支持QueryModel
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="table">IQueryable的查询对象</param>
        /// <param name="model">QueryModel对象</param>
        /// <param name="prefix">使用前缀区分查询条件</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> table, SearchCondition model, string prefix) where TEntity : class
        {
            Contract.Requires(table != null);
            if (table == null)
                throw new ArgumentNullException("table");
            if (model == null)
                throw new ArgumentNullException("model");
            return Where(table, model.Items, prefix);
        }

        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> table, SearchCondition model) where TEntity : class
        {
            Contract.Requires(table != null);
            if (table == null)
                throw new ArgumentNullException("table");
            if (model == null)
                throw new ArgumentNullException("model");
            return Where(table, model.Items, string.Empty);
        }

        private static IQueryable<TEntity> Where<TEntity>(IQueryable<TEntity> table, IEnumerable<ConditionItem> items, string prefix = "")
        {
            Contract.Requires(table != null);
            if (table == null)
                throw new ArgumentNullException("table");
            ICollection<ConditionItem> filterItems =
                (string.IsNullOrWhiteSpace(prefix)
                    ? items.Where(c => string.IsNullOrEmpty(c.Prefix))
                    : items.Where(c => c.Prefix == prefix)).ToList();
            if (!filterItems.Any()) return table;
            return new QueryableSearcher<TEntity>(table, filterItems).Search();
        }

        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> table, string propertyName) where TEntity : class
        {
            return QueryableHelper<TEntity>.OrderBy(table, propertyName, false);
        }

        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> table, string propertyName, bool desc)
        {
            return QueryableHelper<TEntity>.OrderBy(table, propertyName, desc);
        }

        internal static class QueryableHelper<TEntity>
        {
            private static Dictionary<string, LambdaExpression> cache = new Dictionary<string, LambdaExpression>();
          
            public static IQueryable<TEntity> OrderBy(IQueryable<TEntity> queryable, string propertyName, bool desc)
            {
                dynamic keySelector = GetLambdaExpression(propertyName);
                return desc ? Queryable.OrderByDescending(queryable, keySelector) : Queryable.OrderBy(queryable, keySelector);
            }
            private static LambdaExpression GetLambdaExpression(string propertyName)
            {
                if (cache.ContainsKey(propertyName)) return cache[propertyName];
                var param = Expression.Parameter(typeof(TEntity));
                var body = Expression.Property(param, propertyName);
                var keySelector = Expression.Lambda(body, param);
                cache[propertyName] = keySelector;
                return keySelector;
            }
        }
    }
}
