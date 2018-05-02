using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace TMall.Infrastructure.Utility
{
    public static class ExpressionExtensions
    {
        /// <summary>
        /// 获取x=>x.a中的a的名称
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static string GetPropertyName<TSource, TKey>(this Expression<Func<TSource, TKey>> keySelector)
        {
            Check.NotNull(keySelector, "keySelector");
            var member = keySelector.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException("keySelector Expression body is not MemberExpression type");

            return member.Member.Name;
        }
    }
}
