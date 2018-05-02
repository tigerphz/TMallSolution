using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.ManageSite.IBizProcess;
using TMall.ManageSite.ViewModel;
using TMall.ManageSite.ViewModel.Mapping;
using TMall.DomainModels.Management;
using TMall.Framework.ServiceLocation;
using TMall.DomainModule.Enums;
using TMall.Infrastructure.Data;
using TMall.Services.BizServices.Management;
using TMall.Infrastructure.Core;
using TMall.Services.IBizServices.Management;
using System.Web.Mvc;
using TMall.ManageSite.IBizProcess.Management;

namespace TMall.ManageSite.BizProcess.Management
{
    public class MenuBizProcess : IMenuBizProcess
    {
        private IMenuBizService _menuBizService;

        public MenuBizProcess(IMenuBizService menuBizService)
        {
            this._menuBizService = menuBizService;
        }

        /// <summary>
        /// 获取一级目录
        /// </summary>
        /// <returns></returns>
        public List<MenuVM> GetMainMenu()
        {
            List<MenuInfo> medules = this._menuBizService.GetMainMenu().ToList();
            medules = medules.Where(x => { return x.Status == CommonStatus.Valid; }).ToList();

            return medules.ToVM();
        }

        public List<MenuVM> GetChildMenus(Guid parentNO)
        {
            List<MenuInfo> medules = this._menuBizService.GetChildMenus(parentNO).ToList();
            return medules.ToVM();
        }

        public MenuVM GetMenuBySysno(Guid sysno)
        {
            MenuInfo module = _menuBizService.GetMenuBySysno(sysno);
            return module.ToVM();
        }

        public SearchPageInfo<MenuVM> GetMainMenus(SearchPageInfo<MenuVM> search)
        {
            SearchPageInfo<MenuInfo> searchModule = search.ToModel();
            searchModule = _menuBizService.GetMainMenus(searchModule);
            return searchModule.ToVM();
        }

        public SearchPageInfo<MenuVM> GetChildMenus(SearchPageInfo<MenuVM> search)
        {
            SearchPageInfo<MenuInfo> searchModule = search.ToModel();
            searchModule = _menuBizService.GetChildMenus(searchModule);
            return searchModule.ToVM();
        }

        /// <summary>
        /// 根据用户的权限加载菜单
        /// </summary>
        /// <param name="moduleControllers"></param>
        /// <returns></returns>
        public List<MenuTierVM> GetMenuTier(List<string> moduleControllers)
        {
            return _menuBizService.GetMenuTier(moduleControllers).ToVM();
        }

        public bool AddMenu(MenuVM menu)
        {
            MenuInfo module = menu.ToModel();
            return this._menuBizService.AddMenu(module);
        }

        public bool UpdateMenu(UpdateInfo menu)
        {
            return this._menuBizService.UpdateMenu(menu);
        }

        public bool DeleteMenu(Guid sysno)
        {
            MenuInfo moduleInfo = new MenuInfo() { SysNo = sysno };
            return this._menuBizService.DeleteMenu(moduleInfo);
        }
    }
}
