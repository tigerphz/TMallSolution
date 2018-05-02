using System;
using System.Collections.Generic;
using System.Linq;
using TMall.Infrastructure.Core;
using TMall.Framework.ServiceLocation;
using TMall.Framework.ExceptionHanding;

namespace TMall.Infrastructure.Core.DependencyManagement
{
    /// <summary>
    /// Configures the inversion of control container with services used by Nop.
    /// </summary>
    public class ContainerConfigurer
    {
        public virtual void Configure(IEngine engine, ContainerManager containerManager)
        {
            if (containerManager == null)
            {
                throw new ArgumentNullException(string.Format("参数 {0} 为空引发异常", containerManager));
            }

            //other dependencies
            containerManager.AddComponentInstance<IEngine>(engine, "tmall.engine");
            containerManager.AddComponentInstance<ContainerConfigurer>(this, "tmall.containerConfigurer");

            //type finder
            containerManager.AddComponent<ITypeFinder, WebAppTypeFinder>("tmall.typeFinder");

            //register dependencies provided by other assemblies
            var typeFinder = containerManager.GetInstance<ITypeFinder>();
            containerManager.UpdateContainer(x =>
            {
                var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
                var drInstances = new List<IDependencyRegistrar>();
                foreach (var drType in drTypes)
                    drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
                //sort
                drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
                foreach (var dependencyRegistrar in drInstances)
                    dependencyRegistrar.Register(x, typeFinder);
            });
        }
    }
}
