using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using TMall.ManageSite.Controllers;
using TMall.ManageSite.ViewModel;
using System.Web.Mvc.Html;
using System.IO;

namespace TMall.Infrastructure.Web
{
    public static class UrlHelperEx
    {
        /// <summary>
        /// 根据权限判断按钮是否显示
        /// </summary>
        /// <param name="urlHelper"></param>
        /// <param name="linkStr"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public static MvcHtmlString ActionLinkEx(this HtmlHelper htmlHelper, string linkText, string linkPart, string actionName, object htmlAttributes, string controllerName = "")
        {
            MvcHtmlString link = MvcHtmlString.Empty;

            //如果输入的控制器为空
            if (string.IsNullOrEmpty(controllerName))
            {
                RouteValueDictionary implicitRouteValues = htmlHelper.ViewContext.RequestContext.RouteData.Values;
                object implicitValue;
                if (implicitRouteValues != null && implicitRouteValues.TryGetValue("controller", out implicitValue))
                {
                    controllerName = implicitValue.ToString();
                }
            }

            List<PermissionVM> permissionList = AccountManager.Current.GetCurrentPermission();
            //如果没有相应权限
            if (!permissionList.Exists(x =>
            {
                return x.PermissionController == controllerName &&
                    x.PermissionAction == actionName;
            }))
                return link;

            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder imgTagBuilder = new TagBuilder("span");
            imgTagBuilder.SetInnerText(linkText);
            string span = imgTagBuilder.ToString(TagRenderMode.Normal);

            TagBuilder linkTagBuilder = new TagBuilder("a") { InnerHtml = span };

            string linkUrl = urlHelper.Action(actionName, controllerName);
            linkTagBuilder.MergeAttribute("href", linkUrl + "/" + linkPart);
            linkTagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            link = new MvcHtmlString(linkTagBuilder.ToString(TagRenderMode.Normal));
            return link;
        }
    }
}
