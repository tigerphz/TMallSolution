using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Management;
using TMall.DomainModule.Enums;
using TMall.Infrastructure.Data;
using TMall.Framework.ExceptionHanding;
using TMall.Infrastructure.Utility;
using TMall.Services.IBizServices.Management;
using TMall.Services.IRepository.Management;
using TMall.Infrastructure.Core.InterceptionBehaviors;
using TMall.Framework.Caching;
using AutoMapper;

namespace TMall.Services.BizServices.Management
{
    public class MenuBizService : IMenuBizService
    {
        private IMenuRepository _menuRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MenuBizService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
            _unitOfWork = _menuRepository.UnitOfWork;
        }

        #region QuerySerivce

        public IList<MenuInfo> GetChildMenus(Guid parentNo)
        {
            var data = _menuRepository.Entities.Where(x => x.ParentNo.Value.Equals(parentNo))
               .Select(x =>
                       new { MenuName = x.MenuName, x.SysNo, x.Sort })
               .OrderBy(x => x.Sort).ToList();

            return data.Select(Mapper.DynamicMap<MenuInfo>).ToList();
        }

        public IList<MenuInfo> GetMainMenu()
        {
            return _menuRepository.Entities.Where(x => !x.IsLeaf &&
                    x.ParentNo.HasValue == false)
               .OrderBy(x => x.Sort).ToList();
        }

        //[Caching(CachingMethod.Get, ExpiryType = ExpirationType.SlidingTime, ExpireTime = "00:05:00")]
        public MenuInfo GetMenuBySysno(Guid sysno)
        {
            return _menuRepository.GetByKey(sysno);
        }

        public SearchPageInfo<MenuInfo> GetMainMenus(SearchPageInfo<MenuInfo> search)
        {
            Check.NotNull(search, "search");
            Check.NotNull(search.PageInfo, "search.PageInfo");
            if (string.IsNullOrEmpty(search.PageInfo.OrderField))
            {
                throw ExceptionHelper.ThrowBusinessException("分页获取数据必须指定排序列");
            }

            search = _menuRepository.GetPageEntities(search, x => !x.IsLeaf && x.ParentNo.HasValue == false);

            return search;
        }

        public SearchPageInfo<MenuInfo> GetChildMenus(SearchPageInfo<MenuInfo> search)
        {
            Check.NotNull(search, "search");
            Check.NotNull(search.PageInfo, "search.PageInfo");
            if (string.IsNullOrEmpty(search.PageInfo.OrderField))
            {
                throw ExceptionHelper.ThrowBusinessException("分页获取数据必须指定排序列");
            }

            search = _menuRepository.GetPageEntities(search);

            return search;
        }

        /// <summary>
        /// 根据用户的权限加载菜单
        /// </summary>
        /// <param name="moduleControllers"></param>
        /// <returns></returns>
        public List<MenuTier> GetMenuTier(List<string> moduleControllers)
        {
            var result = (from m1 in _menuRepository.Entities.Select(x => new { x.SysNo, MenuName = x.MenuName, x.Sort, x.ParentNo, x.IsSelected, x.StatusDB })
                          join m2 in _menuRepository.Entities.Select(x => new { x.SysNo, MenuName = x.MenuName, x.Sort, x.ParentNo, x.IsSelected, x.StatusDB })
                          on m1.SysNo equals m2.ParentNo.Value
                          join m3 in _menuRepository.Entities.Select(x => new { x.SysNo, MenuName = x.MenuName, x.Sort, x.ParentNo, MenuLinkUrl = x.MenuLinkUrl, MenuIcon = x.MenuIcon, ModuleController = x.ModuleController, x.IsSelected, x.StatusDB })
                          on m2.SysNo equals m3.ParentNo.Value
                          where moduleControllers.Contains(m3.ModuleController) &&
                          m1.StatusDB == (int)CommonStatus.Valid &&
                          m2.StatusDB == (int)CommonStatus.Valid &&
                          m3.StatusDB == (int)CommonStatus.Valid
                          orderby m1.Sort ascending
                          select new { m1, m2, m3 }).ToList();

            List<MenuTier> menuList = new List<MenuTier>();
            var groupResult = result.GroupBy(x => x.m1);
            MenuTier menu = new MenuTier();
            MenuTier child = new MenuTier();

            foreach (var g in groupResult)
            {
                menu = new MenuTier();
                menu.Sort = g.Key.Sort;
                menu.MenuName = g.Key.MenuName;
                menu.SysNo = g.Key.SysNo;
                menu.IsSelected = g.Key.IsSelected;

                var childResult = g.GroupBy(x => x.m2);

                foreach (var c in childResult)
                {
                    child = new MenuTier();
                    child.Sort = c.Key.Sort;
                    child.MenuName = c.Key.MenuName;
                    child.SysNo = c.Key.SysNo;
                    child.IsSelected = c.Key.IsSelected;

                    child.ChildMenus = g.Select(x =>
                    {
                        return new MenuTier()
                        {
                            SysNo = x.m3.SysNo,
                            MenuName = x.m3.MenuName,
                            Sort = x.m3.Sort,
                            MenuLinkUrl = x.m3.MenuLinkUrl,
                            MenuIcon = x.m3.MenuIcon,
                            IsSelected = x.m3.IsSelected
                        };
                    }).OrderBy(item => item.Sort).ToList();

                    menu.ChildMenus.Add(child);
                }

                menuList.Add(menu);
            }

            return menuList;
        }

        #endregion

        #region ActionSerivce

        public bool AddMenu(MenuInfo menuInfo)
        {
            menuInfo.SysNo = EntityGuid.NewComb();
            return _menuRepository.Insert(menuInfo) > 0;
        }

        public bool UpdateMenu(UpdateInfo updateInfo)
        {
            var propertyList = updateInfo.GetPropertyNames();
            if (propertyList == null || propertyList.Length == 0)
                return false;

            _menuRepository.UpdateEntity(updateInfo.GetData<MenuInfo>(), propertyList);

            return _unitOfWork.Commit() > 0;
        }

        //[Caching(CachingMethod.Remove, CorrespondingMethodNames = new[] { "GetChildMenus", "GetChildModulesJsonFormat", 
        //    "GetMainMenu","GetMainMenuJsonFormat","GetMainMenuBySysno" })]
        public bool DeleteMenu(MenuInfo moduleInfo)
        {
            return _menuRepository.Delete(moduleInfo) > 0;
        }

        #endregion

    }
}
