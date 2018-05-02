using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using TMall.Infrastructure.Utility;
using System.Reflection;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MigrationAppConsole
{
    public class MigrationDbContext : DbContext
    {
        public MigrationDbContext()
            : base("ManageDBContext")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<MigrationDbContext, Configuration<MigrationDbContext>>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            AddConfigurations(modelBuilder, new[] { "TMall.DomainModule.DBMapping.dll" },
                    new[] { "TMall.DomainModule.DBMapping.ManageDB" });

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Adds mapping classes contained in provided assemblies and register entities as well
        /// </summary>
        /// <param name="mappingAssemblies"></param>
        public void AddConfigurations(DbModelBuilder modelBuilder, string[] mappingAssemblies, string[] mappingNamespaces)
        {
            if (mappingAssemblies == null || mappingAssemblies.Length == 0)
            {
                throw new ArgumentNullException("mappingAssemblies", "You must specify at least one mapping assembly");
            }

            bool hasMappingClass = false;
            foreach (string mappingAssembly in mappingAssemblies)
            {
                //Assembly asm = Assembly.LoadFrom(MakeLoadReadyAssemblyName(mappingAssembly));
                Assembly asm = Assembly.Load(MakeLoadReadyNameSpace(mappingAssembly));

                Type[] allTypes = asm.GetTypes();
                List<Type> types = new List<Type>();
                if (mappingNamespaces != null && mappingNamespaces.Length > 0)
                {
                    Array.ForEach(mappingNamespaces, (x =>
                    {
                        types.AddRange(Array.FindAll(allTypes,
                                                     (y => y.Namespace.Equals(x, StringComparison.OrdinalIgnoreCase))));
                    }));
                }

                foreach (Type type in types)
                {
                    if (!type.IsAbstract)
                    {
                        if (type.BaseType.IsGenericType && IsMappingClass(type.BaseType))
                        {
                            hasMappingClass = true;

                            // http://areaofinterest.wordpress.com/2010/12/08/dynamically-load-entity-configurations-in-ef-codefirst-ctp5/
                            dynamic configurationInstance = Activator.CreateInstance(type);
                            modelBuilder.Configurations.Add(configurationInstance);
                        }
                    }
                }
            }

            if (!hasMappingClass)
            {
                throw new ArgumentException("No mapping class found!");
            }
        }

        /// <summary>
        /// Determines whether a type is a subclass of entity mapping type
        /// </summary>
        /// <param name="mappingType">Type of the mapping.</param>
        /// <returns>
        /// 	<c>true</c> if it is mapping class; otherwise, <c>false</c>.
        /// </returns>
        private bool IsMappingClass(Type mappingType)
        {
            Type baseType = typeof(EntityTypeConfiguration<>);
            if (mappingType.GetGenericTypeDefinition() == baseType)
            {
                return true;
            }
            if ((mappingType.BaseType != null) &&
                !mappingType.BaseType.IsAbstract &&
                mappingType.BaseType.IsGenericType)
            {
                return IsMappingClass(mappingType.BaseType);
            }
            return false;
        }

        /// <summary>
        /// Ensures the assembly name is qualified
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        private static string MakeLoadReadyAssemblyName(string assemblyName)
        {
            return (assemblyName.IndexOf(".dll", StringComparison.OrdinalIgnoreCase) == -1)
                       ? assemblyName.Trim() + ".dll"
                       : assemblyName.Trim();
        }

        /// <summary>
        /// Ensures the assembly name is qualified
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        private static string MakeLoadReadyNameSpace(string assemblyName)
        {
            return (assemblyName.IndexOf(".dll", StringComparison.OrdinalIgnoreCase) == -1)
                       ? assemblyName.Trim()
                       : (assemblyName.Substring(0, assemblyName.LastIndexOf('.'))).Trim();
        }
    }
}
