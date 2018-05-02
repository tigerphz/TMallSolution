using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMall.Framework.Mapping.MapAttribute
{
    /// <summary>
    /// 忽略映射成员特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class EIgnoreAttribute : Attribute
    {

    }
}
