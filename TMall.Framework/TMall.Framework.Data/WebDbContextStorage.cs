﻿namespace TMall.Framework.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Web;

    public class WebDbContextStorage : IDbContextStorage
    {
        private const string STORAGE_KEY = "HttpContextObjectContextStorageKey";

        public WebDbContextStorage(HttpApplication app)
        {
            app.EndRequest += (sender, args) =>
                {
                    DbContextManager.CloseAllDbContexts();
                    HttpContext.Current.Items.Remove(STORAGE_KEY);
                };
        }

        public DbContext GetDbContextForKey(string key)
        {
            IDbContextStorage storage = GetSimpleDbContextStorage();
            return storage.GetDbContextForKey(key);
        }

        public void SetDbContextForKey(string factoryKey, DbContext context)
        {
            IDbContextStorage storage = GetSimpleDbContextStorage();
            storage.SetDbContextForKey(factoryKey, context);
        }

        public IEnumerable<DbContext> GetAllDbContexts()
        {
            IDbContextStorage storage = GetSimpleDbContextStorage();
            return storage.GetAllDbContexts();
        }

        private SimpleDbContextStorage GetSimpleDbContextStorage()
        {
            HttpContext context = HttpContext.Current;
            var storage = context.Items[STORAGE_KEY] as SimpleDbContextStorage;
            if (storage == null)
            {
                storage = new SimpleDbContextStorage();
                context.Items[STORAGE_KEY] = storage;
            }
            return storage;
        }
    }
}
