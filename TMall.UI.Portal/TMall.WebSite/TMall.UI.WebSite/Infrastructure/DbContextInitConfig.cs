using System.Web;
using TMall.Framework.Data;
using TMall.Infrastructure.Utility;

namespace TMall.UI.WebSite
{
    public class DbContextInitConfig
    {
        public static void DbContextInit(HttpApplication httpApplication)
        {
            DbContextInitializer.Instance().InitializeDbContextOnce(() =>
                            DbContextManager.Init(
                                DbContextCategory.MallDBContext.ToString(),
                                new[] {"TMall.DomainModule.DBMapping.dll"},
                                new[] {"TMall.DomainModule.DBMapping.MainDB"},
                                false, true));

            DbContextManager.InitStorage(new WebDbContextStorage(httpApplication));
        }
    }
}