using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMall.ManageSite.Controllers.Attributes
{
    /// <summary>
    /// UI异常返回
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DWZExceptionResultAttribute : Attribute
    {

    }
}
