using System.Web;
using TMall.Framework.Data;
using TMall.Infrastructure.Utility;
using TMall.Infrastructure.Core;
using TMall.Infrastructure.Data;
using System.Data.Entity;

namespace TMall.UI.ManageSite
{
    public class DbContextInitConfig
    {
        public static void DbContextInit(HttpApplication httpApplication)
        {
            DbContextInitializer.Instance().InitializeDbContextOnce(() =>
            {
                DbContextManager.Init(
                    DbContextCategory.MallDBContext.ToString(),
                    new[] { "TMall.DomainModule.DBMapping.dll" },
                    new[] { "TMall.DomainModule.DBMapping.MainDB" },
                    false, true);

                DbContextManager.Init(
                    DbContextCategory.ManageDBContext.ToString(),
                    new[] { "TMall.DomainModule.DBMapping.dll" },
                    new[] { "TMall.DomainModule.DBMapping.ManageDB" },
                    false, true);
            });

            DbContextManager.InitStorage(new WebDbContextStorage(httpApplication));
        }
    }
}