using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.ManageSite.ViewModel;
using TMall.Infrastructure.Data;

namespace TMall.ManageSite.IBizProcess.Management
{
    public interface IMenuBizProcess
    {
        bool AddMenu(MenuVM menu);

        bool UpdateMenu(UpdateInfo menu);

        bool DeleteMenu(Guid sysno);

        List<MenuVM> GetMainMenu();

        List<MenuVM> GetChildMenus(Guid parentNO);

        MenuVM GetMenuBySysno(Guid sysno);

        SearchPageInfo<MenuVM> GetMainMenus(SearchPageInfo<MenuVM> search);

        SearchPageInfo<MenuVM> GetChildMenus(SearchPageInfo<MenuVM> search);

        /// <summary>
        /// 根据用户的权限加载菜单
        /// </summary>
        /// <param name="moduleControllers"></param>
        /// <returns></returns>
        List<MenuTierVM> GetMenuTier(List<string> moduleControllers);
    }
}
