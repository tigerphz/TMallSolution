using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace TMall.Infrastructure.Web
{
    public static class UrlHelperEx
    {
        public static string ActionForArea(this UrlHelper urlHelper, string actionName, string controllerName, string areaName)
        {
            return urlHelper.Action(actionName, controllerName, new { area = areaName });
        }

        public static string ActionNoArea(this UrlHelper urlHelper, string actionName, string controllerName)
        {
            return urlHelper.Action(actionName, controllerName, new { area = "" });
        }
    }
}
