﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMall.ManageSite.Controllers.Attributes
{
    /// <summary>
    /// 代表该方法可以允许登录用户都能访问
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class LoginAllowViewAttribute : Attribute
    {

    }
}
