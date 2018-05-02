using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMall.Infrastructure.Web;
using TMall.ManageSite.Controllers.Attributes;

namespace TMall.ManageSite.Controllers.Filter
{
    /// <summary>
    /// 权限拦截
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class PermissionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 权限拦截
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            //接下来进行权限拦截与验证
            if (!this.AuthorizeCore(filterContext))//根据验证判断进行处理
            {
                //是否ajax请求
                bool isAjax = filterContext.HttpContext.Request.IsAjaxRequest();

                if (isAjax)
                {
                    //未登录验证
                    if (!filterContext.HttpContext.Request.IsAuthenticated)
                    {
                        //跳转到登录页面
                        filterContext.Result = DWZHelper.ReturnTimeout("登录超时，请重新登录");
                        return;
                    }

                    filterContext.Result = DWZHelper.ReturnErrorAndClose("您没有权限执行此操作！");//功能权限弹出提示框
                    return;
                }

                //跳转到登录页面
                JavaScriptResult scriptResult = new JavaScriptResult();
                scriptResult.Script = "<script>window.location.href='/login';</script>";
                filterContext.Result = scriptResult;
                return;
            }
        }

        /// <summary>
        /// [Anonymous标记]验证是否匿名访问
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        public bool CheckAnonymous(ActionExecutingContext filterContext)
        {
            //验证是否是匿名访问的Action
            object[] attrsAnonymous = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AnonymousAttribute), true);
            //是否是Anonymous
            return attrsAnonymous.Length >= 1;
        }

        /// <summary>
        /// [LoginAllowView标记]验证是否登录就可以访问(如果已经登陆,那么不对于标识了LoginAllowView的方法就不需要验证了)
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        public bool CheckLoginAllowView(ActionExecutingContext filterContext)
        {
            //在这里允许一种情况,如果已经登陆,那么不对于标识了LoginAllowView的方法就不需要验证了
            object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(LoginAllowViewAttribute), true);
            //是否是LoginAllowView
            return attrs.Length >= 1;
        }

        /// <summary>
        /// //权限判断业务逻辑
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="isViewPage">是否是页面</param>
        /// <returns></returns>
        protected virtual bool AuthorizeCore(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            //验证当前Action是否是匿名访问Action
            if (CheckAnonymous(filterContext))
                return true;

            //未登录验证
            if (!filterContext.HttpContext.Request.IsAuthenticated)
            {
                return false;
            }

            //验证当前Action是否是登录就可以访问的Action
            if (CheckLoginAllowView(filterContext))
                return true;

            //下面开始用户权限验证
            //var user = new UserService();
            //SysCurrentUser CurrentUser = new SysCurrentUser();
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();
            //如果是超级管理员,直接允许
            //if (CurrentUser.UserID == ConfigSettings.GetAdminUserID())
            //{
            //    return true;
            //}
            //如果拥有超级管理员的角色就默认全部允许
            //string AdminUserRoleID = ConfigSettings.GetAdminUserRoleID().ToString();
            //检查当前角色组有没有超级角色
            //if (Tools.CheckStringHasValue(CurrentUser.UserRoles, ',', AdminUserRoleID))
            //{
            //    return true;
            //}

            return AccountManager.Current.GetCurrentPermission()
                            .Exists(x => x.PermissionController == controllerName &&
                                        x.PermissionAction == actionName);
        }
    }
}