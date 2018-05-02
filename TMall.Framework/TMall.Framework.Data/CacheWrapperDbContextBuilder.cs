using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFCachingProvider.Caching;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Common;
using EFCachingProvider;

namespace TMall.Framework.Data
{
    public sealed class CacheWrapperDbContextBuilder<T> : AbstractDbContextBuilder<T> where T : DbContext
    {
        private static readonly InMemoryCache InMemoryCache = new InMemoryCache();

        public CacheWrapperDbContextBuilder(string connectionStringName, string[] mappingAssemblies, string[] mappingNamespaces,
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
            var dbModel = this.Build(cn).Compile();
            var wrapcn = CreateConnectionWrapper(cn);

            return (T)new DbContext(wrapcn, dbModel, true);
        }

        /// <summary>
        /// 由数据库连接串名称创建连接对象
        /// </summary>
        /// <param name="connectionStringName">数据库连接串名称</param>
        /// <returns></returns>
        private DbConnection CreateConnectionWrapper(DbConnection cn)
        {
            string providerInvariantName = _cnStringSettings.ProviderName;
            string connectionString = _cnStringSettings.ConnectionString;

            string wrappedConnectionString = "wrappedProvider=" + providerInvariantName + ";" + connectionString;

            EFCachingConnection connection = new EFCachingConnection(cn);

            connection.ConnectionString = wrappedConnectionString;
            connection.CachingPolicy = CachingPolicy.CacheAll;
            connection.Cache = InMemoryCache;

            return connection;
        }
    }
}
