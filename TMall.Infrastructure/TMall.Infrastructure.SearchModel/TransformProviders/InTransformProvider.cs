using System;
using System.Collections.Generic;

namespace TMall.Infrastructure.SearchModel.TransformProviders
{
    internal class InTransformProvider : ITransformProvider
    {
        public bool Match(ConditionItem item, Type type)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            return item.Method == QueryMethod.In;
        }

        public IEnumerable<ConditionItem> Transform(ConditionItem item, Type type)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            var arr = (item.Value as Array);
            if (arr == null)
            {
                var arrStr = item.Value.ToString();
                if (!string.IsNullOrEmpty(arrStr))
                {
                    arr = arrStr.Split(',');
                }
            }
            return new[] {new ConditionItem(item.Field, QueryMethod.StdIn, arr)};
        }
    }
}