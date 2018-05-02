namespace TMall.Framework.ServiceLocation
{
    using Autofac;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class AutofacServiceLocator : ServiceLocatorImplBase
    {
        private readonly IComponentContext _container;

        public AutofacServiceLocator(IComponentContext container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            _container = container;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }
            Type enumerableType = typeof(IEnumerable<>).MakeGenericType(new[] { serviceType });
            return ((IEnumerable) _container.Resolve(enumerableType)).Cast<object>();
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }
            if (key == null)
            {
                return _container.Resolve(serviceType);
            }
            return _container.ResolveNamed(key, serviceType);
        }
    }
}

