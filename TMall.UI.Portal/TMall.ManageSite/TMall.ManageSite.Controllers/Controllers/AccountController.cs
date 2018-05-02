using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TMall.ManageSite.ViewModel;
using TMall.ManageSite.IBizProcess;
using TMall.Infrastructure.Web;
using TMall.ManageSite.IBizProcess.Management;
using System.Web;
using TMall.ManageSite.ViewModel.Mapping;

namespace TMall.ManageSite.Controllers
{
    public class AccountController : Controller
    {
        private IAccountBizProcess _accountBizProcess;

        public AccountController(IAccountBizProcess accountBizProcess)
        {
            _accountBizProcess = accountBizProcess;
        }

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.IsShowValidateCode = Convert.ToInt32(Session["LoginError"]) >= 3;
            //绑定错误信息
            ViewData.BindErrorMessage(ModelState);

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM loginModel)
        {
            ViewBag.IsShowValidateCode = false;

            if (ModelState.IsValid)
            {
                bool validatecode = true;
                if (Session["LoginError"] != null && Convert.ToInt32(Session["LoginError"]) >= 3)
                {
                    if (TempData.ContainsKey(SecurityController.VALIDATECODE))
                    {
                        string code = TempData[SecurityController.VALIDATECODE].ToString();
                        validatecode = loginModel.ValidateCode == code;
                    }
                    else validatecode = false;
                }

                if (validatecode)
                {
                    SysUserVM sysUser = _accountBizProcess.Login(loginModel.SysUserName, loginModel.PasswordHash);
                    if (sysUser != null)
                    {
                        UserInfo userInfo = sysUser.ToUserInfo();
                        FormsPrincipal<UserInfo>.Login(sysUser.UserName, userInfo, 30);
                        Session["LoginError"] = null;

                        //登录成功写cookie
                        var userNameCookie = new HttpCookie("username", sysUser.UserName);
                        userNameCookie.Expires = DateTime.Now.AddDays(365);
                        var rememberMeCookie = new HttpCookie("rememberme", loginModel.RememberMe.ToString().ToLower());
                        userNameCookie.Expires = DateTime.Now.AddDays(365);

                        Response.Cookies.Add(userNameCookie);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "用户名或者密码输入错误！");
                    }
                }
                else
                {
                    ModelState.AddModelError("ValidateCode", "验证码输入错误！");
                }
            }

            Session["LoginError"] = Session["LoginError"] == null ? 0 : Convert.ToInt32(Session["LoginError"]) + 1;
            if (Convert.ToInt32(Session["LoginError"]) >= 3)
            {
                ViewBag.IsShowValidateCode = true;
            }

            //绑定错误信息
            ViewData.BindErrorMessage(ModelState);

            return View(loginModel);
        }

        [HttpGet]
        public ActionResult MiniLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MiniLogin(LoginVM loginModel)
        {
            SysUserVM sysUser = _accountBizProcess.Login(loginModel.SysUserName, loginModel.PasswordHash);
            if (sysUser != null)
            {
                UserInfo userInfo = sysUser.ToUserInfo();
                FormsPrincipal<UserInfo>.Login(sysUser.UserName, userInfo, 30);

                //登录成功写cookie
                var userNameCookie = new HttpCookie("username", sysUser.UserName);
                userNameCookie.Expires = DateTime.Now.AddDays(365);
                var rememberMeCookie = new HttpCookie("rememberme", loginModel.RememberMe.ToString().ToLower());
                userNameCookie.Expires = DateTime.Now.AddDays(365);

                Response.Cookies.Add(userNameCookie);

                return DWZHelper.ReturnSuccAndClose("欢迎您回来！");
            }
            else
            {
                return DWZHelper.ReturnError("账号或密码输入错误");
            }
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            //清除缓存
            AccountManager.Current.ClearCache();
            FormsPrincipal<UserInfo>.Logout();

            return RedirectToAction("Login");
        }
    }
}
