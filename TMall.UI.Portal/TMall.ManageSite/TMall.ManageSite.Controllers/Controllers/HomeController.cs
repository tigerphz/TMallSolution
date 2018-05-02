using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TMall.ManageSite.IBizProcess;
using TMall.ManageSite.ViewModel;
using TMall.ManageSite.IBizProcess.Management;
using TMall.ManageSite.Controllers.Attributes;

namespace TMall.ManageSite.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {

        }

        [LoginAllowView]
        public ActionResult Index()
        {
            List<MenuTierVM> menuList = AccountManager.Current.GetCurrentMenu();
            return View(menuList);
        }
    }
}
