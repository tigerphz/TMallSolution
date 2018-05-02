using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using System.Reflection;
using TMall.Infrastructure.IoC;
using TMall.ManageSite.BizProcess;
using TMall.ManageSite.IBizProcess;
using TMall.ManageSite.ViewModel;
using FluentValidation;
using FluentValidation.Mvc;
using TMall.Infrastructure.Core.DependencyManagement;
using TMall.Infrastructure.Core;
using TMall.ManageSite.ViewModel.Mapping;
using TMall.ManageSite.ViewModel.FluentValidate;
using TMall.ManageSite.BizProcess.Management;
using TMall.ManageSite.IBizProcess.Management;

namespace TMall.UI.ManageSite
{
    public class AutofacDependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<AccountBizProcess>().As<IAccountBizProcess>().InstancePerHttpRequest();
            builder.RegisterType<MenuBizProcess>().As<IMenuBizProcess>().InstancePerHttpRequest();
            builder.RegisterType<PermissionBizProcess>().As<IPermissionBizProcess>().InstancePerHttpRequest();
            builder.RegisterType<RoleBizProcess>().As<IRoleBizProcess>().InstancePerHttpRequest();
            builder.RegisterType<ExceptionLogBizProcess>().As<IExceptionLogBizProcess>().InstancePerHttpRequest();

            //builder.RegisterType<MallMSContext>();
            //builder.Register(x => { return new AccountRepository(x.Resolve<MallMSContext>()); }).As<IAccountRepository>();

            //builder.RegisterType<AccountBiz>().As<IAccountBiz>();

            //builder.Register(c => new Person()).As<IPerson>();//接口注册
            //builder.Register(c => { return new Person() { Name = "tiger", Age = 10 }; });//lambda表达式注册
            //builder.Register(c=>new Teacher(c.Resolve<Person>()));//带构造参数注册
            //builder.Register(c => new Student() { Person = c.Resolve<Person>() });//带属性复制注册
            //builder.RegisterInstance(MyInstance.Instance).ExternallyOwned();//实例来注册
            //builder.RegisterGeneric(typeof (MyList<>));//注册open generic类型var myIntList = container.Resolve<MyList<int>>();
            //containerBuilder.RegisterType<DbRepository>().Named<IRepository>("DB");//可以用Name来区分不同的实现，代替As方法
            //containerBuilder.RegisterType<TestRepository>().Named<IRepository>("Test");


            //注册控制器类型、类型绑定到IoC容器
            builder.RegisterControllers(Assembly.Load("TMall.ManageSite.Controllers")).InstancePerHttpRequest();
            Autofac.Integration.Mvc.RegistrationExtensions.RegisterModelBinders(builder, Assembly.GetExecutingAssembly()).InstancePerHttpRequest();

            //注册ViewModel对应的FluentValidateModel
            builder.RegisterType<ModelValidatorFactory>().As<IValidatorFactory>();
            Assembly validatorAssembly = Assembly.Load("TMall.ManageSite.ViewModel");
            AssemblyScanner findValidatorsInAssembly = AssemblyScanner.FindValidatorsInAssembly(validatorAssembly);
            //AssemblyScanner findValidatorsInAssembly = AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly());
            foreach (AssemblyScanner.AssemblyScanResult item in findValidatorsInAssembly)
            {
                builder.RegisterType(item.ValidatorType).As(item.InterfaceType).InstancePerHttpRequest();
            }
        }

        public int Order
        {
            get { return 0; }
        }
    }

    /// <summary>
    /// 使用Autofac自定义控制器构造工厂
    /// </summary>
    public class AutofacControllerFactory : DefaultControllerFactory
    {
        private IComponentContext _container;

        public AutofacControllerFactory(IComponentContext container)
        {
            this._container = container;
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new ArgumentNullException("controllerType");
            }

            return (IController)this._container.Resolve(controllerType);
        }
    }
}