using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FluentValidation.Mvc;
using TMall.ManageSite.ViewModel;
using TMall.Infrastructure.Web;
using StackExchange.Profiling;
using System.Data.Entity;
using StackExchange.Profiling.Data;
using TMall.Infrastructure.Core;
using Autofac;
using Autofac.Integration.Mvc;
using FluentValidation;
using TMall.Infrastructure.Core.DependencyManagement;
using System.Web.Security;

namespace TMall.UI.ManageSite
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //初始化数据库、ioc容器、运行初始化任务等操作
            EngineContext.Initialize(false);

            IContainer container = EngineContext.Current.Container;

            //注册MVC使用的Ioc容器
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //注入自定义的FluentValidation类型提供工厂
            FluentValidationModelValidatorProvider.Configure(x =>
                x.ValidatorFactory = container.Resolve<IValidatorFactory>());

            //MiniProfiler EF性能监视初始化
            MiniProfilerEF.Initialize();

            //初始化EF数据库
            DbContextInitConfig.DbContextInit(this);
            
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            HttpCookie cookie = app.Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                cookie.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Add(cookie);
            }
        }

        public void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            FormsPrincipal<UserInfo>.TrySetUserInfo(app.Context);
        }
    }
}