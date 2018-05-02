using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMall.ManageSite.Controllers.Attributes
{
    /// <summary>
    /// 匿名访问标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AnonymousAttribute : Attribute
    {
        
    }
}
