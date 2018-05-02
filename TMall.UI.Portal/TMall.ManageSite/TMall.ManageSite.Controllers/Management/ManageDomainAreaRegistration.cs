using System.Web.Mvc;

namespace TMall.ManageSite.Controllers.Management
{
    public class ManagementAreaRegistration : AreaRegistration
    {
        private string AreasNameSpace = "TMall.ManageSite.Controllers.Management";

        public override string AreaName
        {
            get
            {
                return "Management";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Management_default",
                "Management/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                null,
                new[] { AreasNameSpace }
            );

            context.MapRoute(
                "Management_dwz",
                "Management/{controller}/{action}/{navId}/{id}",
                new { navId = UrlParameter.Optional, id = UrlParameter.Optional },
                null,
                new[] { AreasNameSpace }
            );
        }
    }
}
