using System;
using System.Collections.Generic;

namespace TMall.Infrastructure.SearchModel
{
    public interface ITransformProvider
    {
        bool Match(ConditionItem item, Type type);
        IEnumerable<ConditionItem> Transform(ConditionItem item, Type type);
    }
}