

using System.Collections.Generic;

namespace TMall.Framework.Data
{
    using System.Data.Common;
    using System.Data.Objects;
    using System;
    using System.Reflection;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity;
    using System.Configuration;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using EFCachingProvider.Caching;
    using EFCachingProvider;
    using TMall.Framework.ExceptionHanding;

    public sealed class DbContextBuilder<T> : AbstractDbContextBuilder<T> where T : DbContext
    {
        public DbContextBuilder(string connectionStringName, string[] mappingAssemblies, string[] mappingNamespaces,
                               bool recreateDatabaseIfExists,
                               bool lazyLoadingEnabled)
            : base(connectionStringName,
                    mappingAssemblies,
                    mappingNamespaces,
                    recreateDatabaseIfExists,
                    lazyLoadingEnabled)
        {

        }

        public override T BuildDbContext()
        {
            var cn = _factory.CreateConnection();
            if (cn == null)
                throw new ArgumentNullException("Connection");

            cn.ConnectionString = _cnStringSettings.ConnectionString;

            var dbModel = this.Build(cn);

            var ctx = dbModel.Compile().CreateObjectContext<ObjectContext>(cn);
            //[lazy loading enabled]
            ctx.ContextOptions.LazyLoadingEnabled = this._lazyLoadingEnabled;

            if (!ctx.DatabaseExists())
            {
                ctx.CreateDatabase();
            }
            else if (_recreateDatabaseIfExists)//[recreate database if exist]
            {
                ctx.DeleteDatabase();
                ctx.CreateDatabase();
            }

            return (T)new DbContext(ctx, true);
        }
    }
}
