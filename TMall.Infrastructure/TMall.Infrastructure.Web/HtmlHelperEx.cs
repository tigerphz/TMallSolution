using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace TMall.Infrastructure.Web
{
    public static class HtmlHelperEx
    {
        public static MvcHtmlString ActionLinkForArea(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string areaName)
        {
            return htmlHelper.ActionLink(linkText, actionName, new { area = areaName }, null);
        }

        public static MvcHtmlString ActionLinkForArea(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string areaName, object htmlAttributes)
        {
            return htmlHelper.ActionLink(linkText, actionName, new { area = areaName }, htmlAttributes);
        }

        public static MvcHtmlString ActionLinkNoArea(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            return htmlHelper.ActionLink(linkText, actionName, controllerName, new { area = "" }, null);
        }

        public static MvcHtmlString ActionLinkNoArea(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object htmlAttributes)
        {
            return htmlHelper.ActionLink(linkText, actionName, controllerName, new { area = "" }, htmlAttributes);
        }
    }
}
