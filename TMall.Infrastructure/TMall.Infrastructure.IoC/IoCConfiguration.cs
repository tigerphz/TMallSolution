#region namespace
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Integration.Mvc;
using TMall.Framework.Data;
using TMall.Infrastructure.Utility;
using TMall.Framework.Mapping;
using System.Data.Entity;
using TMall.Infrastructure.Data;
using TMall.Services.BizServices.Management;
using TMall.Services.BizServices.Customer;
using TMall.Services.Repository.Customer;
using TMall.Services.Repository.Management;
using System.Web;
using TMall.Infrastructure.Web.Fakes;
using TMall.Infrastructure.Core.DependencyManagement;
using TMall.Infrastructure.Core;
using TMall.Services.IBizServices.Management;
using TMall.Services.IRepository.Management;
using TMall.Services.IBizServices.Customer;
using TMall.Services.IRepository.Customer;
using TMall.Framework.Caching;
using TMall.Infrastructure.Web;
using TMall.Framework.Logging;
#endregion

namespace TMall.Infrastructure.IoC
{
    public class IoCConfiguration : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {

            #region HTTP context and other related stuff
            //HTTP context and other related stuff
            builder.Register(c =>
                //register FakeHttpContext when HttpContext is not available
                HttpContext.Current != null ?
                (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
                (new FakeHttpContext("~/") as HttpContextBase))
                .As<HttpContextBase>()
                .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().ApplicationInstance)
                .As<HttpApplication>()
                .InstancePerHttpRequest();

            #endregion

            #region WebHelper

            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerHttpRequest();

            #endregion

            #region DbContext

            builder.Register(c => { return DbContextManager.CurrentFor(DbContextCategory.ManageDBContext.ToString()); })
                             .Named<DbContext>(DbContextCategory.ManageDBContext.ToString());

            builder.Register(c => { return DbContextManager.CurrentFor(DbContextCategory.MallDBContext.ToString()); })
                             .Named<DbContext>(DbContextCategory.MallDBContext.ToString());

            #endregion

            #region CacheProvider

            //注册缓存
            builder.RegisterType<MemoryCacheProvider>().As<ICacheProvider>().Named<ICacheProvider>(CacheProviderType.LOCALMEMORYCACHE.ToString()).SingleInstance();
            builder.RegisterType<PerRequestCacheManager>().As<ICacheProvider>().Named<ICacheProvider>(CacheProviderType.HTTPPREREQUEST.ToString()).InstancePerHttpRequest();

            #endregion

            #region Log

            //default is log4net
            builder.RegisterType<DefaultLog>().As<ILogger>().SingleInstance();
            
            #endregion

            #region IUnitOfWork

            //builder.RegisterType<EFRepositoryContext>().As<IUnitOfWork>().InstancePerHttpRequest();

            #endregion

            #region CustomerDomain
            //CustomerDomain
            builder.RegisterType<CustomerServiceFace>().InstancePerHttpRequest();
            builder.RegisterType<CustomerBizService>().As<ICustomerBizService>().InstancePerHttpRequest();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>()
                .WithParameter(ResolvedParameter.ForNamed<DbContext>(DbContextCategory.MallDBContext.ToString())).InstancePerHttpRequest();

            #endregion

            #region ManagerDomain

            //SysUser
            builder.RegisterType<SysUserBizService>().As<ISysUserBizService>().InstancePerHttpRequest();
            builder.RegisterType<SysUserRepository>().As<ISysUserRepository>()
                .WithParameter(ResolvedParameter.ForNamed<DbContext>(DbContextCategory.ManageDBContext.ToString())).InstancePerHttpRequest();

            //SysUserRole
            builder.RegisterType<SysUserRoleRepository>().As<ISysUserRoleRepository>()
                .WithParameter(ResolvedParameter.ForNamed<DbContext>(DbContextCategory.ManageDBContext.ToString())).InstancePerHttpRequest();

            //Menu
            builder.RegisterType<MenuBizService>().As<IMenuBizService>().InstancePerHttpRequest();
            builder.RegisterType<MenuRepository>().As<IMenuRepository>()
                .WithParameter(ResolvedParameter.ForNamed<DbContext>(DbContextCategory.ManageDBContext.ToString())).InstancePerHttpRequest();

            //ExceptionLog
            builder.RegisterType<ExceptionLogBizService>().As<IExceptionLogBizService>().InstancePerHttpRequest();
            builder.RegisterType<ExceptionLogRepository>().As<IExceptionLogRepository>()
                .WithParameter(ResolvedParameter.ForNamed<DbContext>(DbContextCategory.ManageDBContext.ToString())).InstancePerHttpRequest();

            //Role
            builder.RegisterType<RoleBizService>().As<IRoleBizService>().InstancePerHttpRequest();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>()
                .WithParameter(ResolvedParameter.ForNamed<DbContext>(DbContextCategory.ManageDBContext.ToString())).InstancePerHttpRequest();

            //Permission
            builder.RegisterType<PermissionBizService>().As<IPermissionBizService>().InstancePerHttpRequest();
            builder.RegisterType<PermissionRepository>().As<IPermissionRepository>()
                .WithParameter(ResolvedParameter.ForNamed<DbContext>(DbContextCategory.ManageDBContext.ToString())).InstancePerHttpRequest();

            //RolePermission
            builder.RegisterType<RolePermissionRepository>().As<IRolePermissionRepository>()
                .WithParameter(ResolvedParameter.ForNamed<DbContext>(DbContextCategory.ManageDBContext.ToString())).InstancePerHttpRequest();

            //RolePermissionFace
            builder.RegisterType<RolePermissionFace>();

            //SysUserRoleFace
            builder.RegisterType<SysUserRoleFace>();

            #endregion

        }

        public int Order
        {
            get { return 0; }
        }
    }
}
