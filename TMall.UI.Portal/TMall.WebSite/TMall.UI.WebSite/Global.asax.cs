using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TMall.Infrastructure.Core;
using Autofac;
using Autofac.Integration.Mvc;
using FluentValidation;
using FluentValidation.Mvc;
using TMall.Framework.ServiceLocation;
using TMall.Infrastructure.Core.DependencyManagement;

namespace TMall.UI.WebSite
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            EngineContext.Initialize(false);

            IContainer container = EngineContext.Current.Container;           
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //注入自定义的FluentValidation类型提供工厂
            FluentValidationModelValidatorProvider.Configure(x =>
                x.ValidatorFactory = container.Resolve<IValidatorFactory>());

            //初始化EF的DbContext
            DbContextInitConfig.DbContextInit(this);

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}