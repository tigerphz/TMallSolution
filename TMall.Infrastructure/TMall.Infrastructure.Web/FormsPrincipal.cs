using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using TMall.Infrastructure.Utility;

namespace TMall.Infrastructure.Web
{
    public class FormsPrincipal<TUserData> : IPrincipal
        where TUserData : class, new()
    {
        private readonly IIdentity _identity;
        private readonly TUserData _userData;

        public FormsPrincipal(FormsAuthenticationTicket ticket, TUserData userData)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");
            if (userData == null)
                throw new ArgumentNullException("userData");

            _identity = new FormsIdentity(ticket);
            _userData = userData;
        }

        public TUserData UserData
        {
            get { return _userData; }
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        public bool IsInRole(string role)
        {
            // 把判断用户组的操作留给UserData去实现。
            var principal = _userData as IPrincipal;
            return principal != null && principal.IsInRole(role);
        }

        /// <summary>
        /// 执行用户登录操作
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="userData">与登录名相关的用户信息</param>
        /// <param name="expiration">登录Cookie的过期时间，单位：分钟。</param>
        public static void Login(string loginName, TUserData userData, int expiration)
        {
            if (string.IsNullOrEmpty(loginName))
                throw new ArgumentNullException("loginName");
            if (userData == null)
                throw new ArgumentNullException("userData");

            // 1. 把需要保存的用户数据转成一个字符串。
            string data = JsonHelper.Serialize(userData);

            // 2. 创建一个FormsAuthenticationTicket，它包含登录名以及额外的用户数据。
            var ticket = new FormsAuthenticationTicket(
                2, loginName, DateTime.Now, DateTime.Now.AddDays(1), true, data);

            // 3. 加密Ticket，变成一个加密的字符串。
            var cookieValue = FormsAuthentication.Encrypt(ticket);

            // 4. 根据加密结果创建登录Cookie
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieValue)
                {
                    HttpOnly = true,
                    Secure = FormsAuthentication.RequireSSL,
                    Domain = FormsAuthentication.CookieDomain,
                    Path = FormsAuthentication.FormsCookiePath
                };
            if (expiration > 0)
                cookie.Expires = DateTime.Now.AddMinutes(expiration);

            HttpContext context = HttpContext.Current;
            if (context == null)
                throw new InvalidOperationException();

            // 5. 写登录Cookie
            context.Response.Cookies.Remove(cookie.Name);
            context.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 退出
        /// </summary>
        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// 根据HttpContext对象设置用户标识对象
        /// </summary>
        /// <param name="context"></param>
        public static void TrySetUserInfo(HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            // 1. 读登录Cookie
            HttpCookie cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                return;
            try
            {
                TUserData userData = null;
                // 2. 解密Cookie值，获取FormsAuthenticationTicket对象
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

                if (ticket != null && string.IsNullOrEmpty(ticket.UserData) == false)
                    // 3. 还原用户数据
                    userData = JsonHelper.Deserialize<TUserData>(ticket.UserData);

                if (ticket != null && userData != null)
                    // 4. 构造我们的MyFormsPrincipal实例，重新给context.User赋值。
                    context.User = new FormsPrincipal<TUserData>(ticket, userData);
            }
            catch { /* 有异常也不要抛出，防止攻击者试探。 */ }
        }
    }
}
