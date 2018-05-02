using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Management;
using TMall.Infrastructure.Data;

namespace TMall.Services.IBizServices.Management
{
    public interface IMenuBizService
    {
        bool AddMenu(MenuInfo moduleInfo);

        bool UpdateMenu(UpdateInfo updateInfo);

        bool DeleteMenu(MenuInfo moduleInfo);

        IList<MenuInfo> GetMainMenu();

        IList<MenuInfo> GetChildMenus(Guid parentNo);

        MenuInfo GetMenuBySysno(Guid sysno);

        SearchPageInfo<MenuInfo> GetMainMenus(SearchPageInfo<MenuInfo> search);

        SearchPageInfo<MenuInfo> GetChildMenus(SearchPageInfo<MenuInfo> search);

        List<MenuTier> GetMenuTier(List<string> moduleControllers);
    }
}
