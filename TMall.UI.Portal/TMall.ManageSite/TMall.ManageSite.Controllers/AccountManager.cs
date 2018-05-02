using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMall.ManageSite.ViewModel;
using TMall.Infrastructure.Web;
using TMall.Infrastructure.Core;
using TMall.ManageSite.IBizProcess.Management;
using EntityFramework.Caching;
using TMall.DomainModule.Enums;

namespace TMall.ManageSite.Controllers
{
    /// <summary>
    /// 账户管理
    /// </summary>
    public class AccountManager
    {
        private const string cache_permission = "cache_permission";
        private const string cache_menu = "cache_menu";

        private static AccountManager _accountManager = null;

        private static object locker = new object();

        private AccountManager() { }

        public static AccountManager Current
        {
            get
            {
                if (_accountManager == null)
                {
                    lock (locker)
                    {
                        if (_accountManager == null)
                        {
                            _accountManager = new AccountManager();
                        }
                    }
                }
                return _accountManager;
            }
        }

        /// <summary>
        /// 获取当前登录用户基本信息
        /// </summary>
        public UserInfo CurrentUser
        {
            get
            {
                FormsPrincipal<UserInfo> fp = HttpContext.Current.User as FormsPrincipal<UserInfo>;
                if (fp == null)
                {
                    return null;
                }
                return fp.UserData;
            }
        }

        /// <summary>
        /// 获取当前用户具有的权限列表
        /// </summary>
        /// <returns></returns>
        public List<PermissionVM> GetCurrentPermission()
        {
            string key = string.Format("{0}_{1}", cache_permission, CurrentUser.SysNo.ToString());
            List<PermissionVM> permissionList = EngineContext.Current.CacheManager.Get(key, CurrentUser.SysNo.ToString())
                                        as List<PermissionVM>;
            if (permissionList != null)
            {
                return permissionList;
            }

            IPermissionBizProcess permissionProcess = EngineContext.Current.Resolve<IPermissionBizProcess>();
            permissionList = permissionProcess.GetPermissionByUserSysno(CurrentUser.SysNo);

            List<PermissionVM> invalidList = permissionList.FindAll(x => x.Status == CommonStatus.Invalid);
            if (invalidList != null && invalidList.Count > 0)
            {
                permissionList.RemoveAll(x =>
                {
                    return invalidList.Exists(y => y.SysNo == x.ParentID);
                });
                permissionList.RemoveAll(x => x.Status == CommonStatus.Invalid);
            }

            EngineContext.Current.CacheManager.Add(key, CurrentUser.SysNo.ToString(),
                                                    permissionList, new TimeSpan(0, 0, 20, 0));

            return permissionList;
        }

        /// <summary>
        /// 获取当前用户具有的菜单列表
        /// </summary>
        /// <returns></returns>
        public List<MenuTierVM> GetCurrentMenu()
        {
            string key = string.Format("{0}_{1}", cache_menu, CurrentUser.SysNo.ToString());
            List<MenuTierVM> menuTierList = EngineContext.Current.CacheManager.Get(key, CurrentUser.SysNo.ToString())
                                        as List<MenuTierVM>;
            if (menuTierList != null)
            {
                return menuTierList;
            }

            List<PermissionVM> permissionList = GetCurrentPermission();
            List<string> moduleControllerList = permissionList.Select(x => x.PermissionController).Distinct().ToList();

            IMenuBizProcess permissionProcess = EngineContext.Current.Resolve<IMenuBizProcess>();
            menuTierList = permissionProcess.GetMenuTier(moduleControllerList);
            if (menuTierList != null && menuTierList.Count > 0 && !menuTierList.Exists(x => x.IsSelected))
            {
                menuTierList[0].IsSelected = true;
            }

            EngineContext.Current.CacheManager.Add(key, CurrentUser.SysNo.ToString(),
                                                    menuTierList, new TimeSpan(0, 0, 20, 0));

            return menuTierList;
        }

        /// <summary>
        /// 退出时清除缓存
        /// </summary>
        public void ClearCache()
        {
            if (CurrentUser == null)
                return;

            string permissionkey = string.Format("{0}_{1}", cache_permission, CurrentUser.SysNo.ToString());
            string menukey = string.Format("{0}_{1}", cache_menu, CurrentUser.SysNo.ToString());

            EngineContext.Current.CacheManager.Remove(permissionkey);
            EngineContext.Current.CacheManager.Remove(menukey);
        }
    }
}