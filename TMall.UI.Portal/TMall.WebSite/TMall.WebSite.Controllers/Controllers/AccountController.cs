using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TMall.WebSite.ViewModel;
using TMall.WebSite.IBizProcess;

namespace TMall.WebSite.Controllers
{
    public class AccountController : Controller
    {
        private IAccountBizProcess _accountBizProcess;

        public AccountController(IAccountBizProcess accountBizProcess)
        {
            _accountBizProcess = accountBizProcess;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(CustomerVM customer)
        {
            if (ModelState.IsValid)
            {
                bool result = _accountBizProcess.CreateCustomer(customer);
            }

            return View(customer);
        }

        [HttpPost]
        public ActionResult Login(string customerName, string passwordHash)
        {
            _accountBizProcess.Login(customerName, passwordHash);
            return View();
        }
    }
}
