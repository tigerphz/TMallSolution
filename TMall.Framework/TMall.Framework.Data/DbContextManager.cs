namespace TMall.Framework.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Configuration;

    public class DbContextManager
    {
        private static readonly object SyncLock = new object();

        //是否启用EF扩展缓存组件
        private static bool _secondCachingEnabled;

        public static void Init(string[] mappingAssemblies, string[] mappingNamespaces = null,
                                bool recreateDatabaseIfExist = false,
                                bool lazyLoadingEnabled = true)
        {
            Init(DefaultConnectionStringName, mappingAssemblies, mappingNamespaces, recreateDatabaseIfExist,
                 lazyLoadingEnabled);
        }

        public static void Init(string connectionStringName, string[] mappingAssemblies,
                                string[] mappingNamespaces = null,
                                bool recreateDatabaseIfExist = false, bool lazyLoadingEnabled = true)
        {
            _secondCachingEnabled = bool.Parse(ConfigurationManager.AppSettings["EntityFrameworkCachingEnabled"]);

            AddConfiguration(connectionStringName, mappingAssemblies, mappingNamespaces, recreateDatabaseIfExist,
                             lazyLoadingEnabled);
        }

        public static void InitStorage(IDbContextStorage storage)
        {
            if (storage == null)
            {
                throw new ArgumentNullException("storage");
            }
            if ((_storage != null) && (_storage != storage))
            {
                throw new ApplicationException("A storage mechanism has already been configured for this application");
            }
            _storage = storage;
        }

        /// <summary>
        /// The default connection string name used if only one database is being communicated with.
        /// </summary>
        public static readonly string DefaultConnectionStringName = "DefaultDb";

        /// <summary>
        /// Used to get the current db context session if you're communicating with a single database.
        /// When communicating with multiple databases, invoke <see cref="CurrentFor()" /> instead.
        /// </summary>
        public static DbContext Current
        {
            get { return CurrentFor(DefaultConnectionStringName); }
        }

        /// <summary>
        /// Used to get the current DbContext associated with a key; i.e., the key 
        /// associated with an object context for a specific database.
        /// 
        /// If you're only communicating with one database, you should call <see cref="Current" /> instead,
        /// although you're certainly welcome to call this if you have the key available.
        /// </summary>
        public static DbContext CurrentFor(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            if (_storage == null)
            {
                throw new ApplicationException("An IDbContextStorage has not been initialized");
            }

            DbContext context;
            lock (SyncLock)
            {
                if (!_dbContextBuilders.ContainsKey(key))
                {
                    throw new ApplicationException("An DbContextBuilder does not exist with a key of " + key);
                }

                context = _storage.GetDbContextForKey(key);

                if (context == null)
                {
                    context = _dbContextBuilders[key].BuildDbContext();
                    _storage.SetDbContextForKey(key, context);
                }
            }
            return context;
        }

        /// <summary>
        /// This method is used by application-specific db context storage implementations
        /// and unit tests. Its job is to walk thru existing cached object context(s) and Close() each one.
        /// </summary>
        public static void CloseAllDbContexts()
        {
            foreach (DbContext ctx in _storage.GetAllDbContexts())
            {
                if (((IObjectContextAdapter) ctx).ObjectContext.Connection.State == System.Data.ConnectionState.Open)
                    ((IObjectContextAdapter) ctx).ObjectContext.Connection.Close();
            }
        }

        private static void AddConfiguration(string connectionStringName, string[] mappingAssemblies,
                                             string[] mappingNamespaces,
                                             bool recreateDatabaseIfExists = false, bool lazyLoadingEnabled = true)
        {
            if (string.IsNullOrEmpty(connectionStringName))
            {
                throw new ArgumentNullException("connectionStringName");
            }

            if (mappingAssemblies == null)
            {
                throw new ArgumentNullException("mappingAssemblies");
            }

            lock (SyncLock)
            {
                if (_secondCachingEnabled)
                {
                    _dbContextBuilders.Add(connectionStringName,
                      new CacheWrapperDbContextBuilder<DbContext>(connectionStringName, mappingAssemblies,
                                                      mappingNamespaces,
                                                      recreateDatabaseIfExists, lazyLoadingEnabled));
                }
                else
                {
                    _dbContextBuilders.Add(connectionStringName,
                     new DbContextBuilder<DbContext>(connectionStringName, mappingAssemblies,
                                                      mappingNamespaces,
                                                      recreateDatabaseIfExists, lazyLoadingEnabled));
                }
            }
        }

        /// <summary>
        /// An application-specific implementation of IDbContextStorage must be setup either thru
        /// <see cref="InitStorage" /> or one of the <see cref="Init" /> overloads. 
        /// </summary>
        private static IDbContextStorage _storage { get; set; }

        /// <summary>
        /// Maintains a dictionary of db context builders, one per database.  The key is a 
        /// connection string name used to look up the associated database, and used to decorate respective
        /// repositories. If only one database is being used, this dictionary contains a single
        /// factory with a key of <see cref="DefaultConnectionStringName" />.
        /// </summary>
        private static readonly Dictionary<string, AbstractDbContextBuilder<DbContext>> _dbContextBuilders =
            new Dictionary<string, AbstractDbContextBuilder<DbContext>>();

    }
}
