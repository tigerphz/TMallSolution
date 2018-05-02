using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TMall.ManageSite.ViewModel;
using TMall.ManageSite.IBizProcess;
using TMall.Infrastructure.Web;
using TMall.DomainModule.Enums;
using TMall.Infrastructure.Data;
using TMall.Infrastructure.Utility;
using TMall.Infrastructure.SearchModel;
using System.ComponentModel;
using TMall.ManageSite.IBizProcess.Management;
using TMall.ManageSite.Controllers.Attributes;

namespace TMall.ManageSite.Controllers.Management
{
    public class MenuController : BaseController
    {
        private IMenuBizProcess _moduleBizProcess;

        public MenuController(IMenuBizProcess moduleBizProcess)
        {
            _moduleBizProcess = moduleBizProcess;
        }

        [Description("[菜单管理首页]")]
        [ViewPage]
        public ActionResult Index(Guid id, SearchCondition searchCondition, PageInfo pageInfo, Guid? MenuIndexLevel1, Guid? MenuIndexLeve12)
        {
            //检测分页信息
            PageInfoHelper.CheckedPageInfo(pageInfo, "Sort");
            SearchPageInfo<MenuVM> search = new SearchPageInfo<MenuVM>()
            {
                PageInfo = pageInfo,
                SearchCondition = searchCondition
            };

            Guid? parentNo = MenuIndexLeve12 ?? MenuIndexLevel1;
            if (parentNo.HasValue)
            {
                search.SearchCondition.Items.Add(new ConditionItem()
                   {
                       Field = "ParentNo",
                       Method = QueryMethod.Equal,
                       Value = parentNo.Value
                   });

                search = _moduleBizProcess.GetChildMenus(search);
            }
            else
            {
                search = _moduleBizProcess.GetMainMenus(search);
            }

            //设置分页信息到界面
            ViewData.SetPageInfo(search.PageInfo);
            //设置状态信息
            ViewBag.StatusList = DDLValueExtensions.EnumToSelectListItems<CommonStatus>(AppendItemType.All);

            //重新绑定查询信息到界面
            ViewData.ReBindSearchData(searchCondition);

            var menuLevel1 = _moduleBizProcess.GetMainMenu();
            ViewBag.MenuLevel1List = menuLevel1.ListToSelectListItems(x => { return x.MenuName; },
                                x => { return x.SysNo.ToString(); }, AppendItemType.All);

            var menuLevel2 = new List<MenuVM>();
            if (MenuIndexLevel1.HasValue)
                menuLevel2 = _moduleBizProcess.GetChildMenus(MenuIndexLevel1.Value);

            ViewBag.MenuLevel2List = menuLevel2.ListToSelectListItems(x => { return x.MenuName; },
                                x => { return x.SysNo.ToString(); }, AppendItemType.All);

            ViewBag.navId = id.ToString();
            return View(search.DataList);
        }

        [HttpPost]
        [Description("[菜单管理首页]的首页菜单选择下拉框]")]
        [LoginAllowView]
        public ContentResult GetComboxAll(Guid? id)
        {
            return Content(GetDDLValueJson(id, AppendItemType.All));
        }

        [HttpPost]
        [Description("[菜单管理首页]添加、修改菜单页面的菜单选择下拉框")]
        [LoginAllowView]
        public ContentResult GetComboxSelected(Guid? id)
        {
            return Content(GetDDLValueJson(id, AppendItemType.Select));
        }

        [NonAction]
        private string GetDDLValueJson(Guid? id, AppendItemType appendItemType)
        {
            List<MenuVM> menuList = new List<MenuVM>();
            if (id != null && id.HasValue)
            {
                menuList = _moduleBizProcess.GetChildMenus(id.Value);
            }

            List<SelectListItem> itemList = menuList.ListToSelectListItems(x => { return x.MenuName; },
                            x => { return x.SysNo.ToString(); }, appendItemType);

            return itemList.ToDWZDDLJson();
        }

        [HttpGet]
        [Description("[菜单管理首页]添加菜单页面")]
        [ActionName("Add")]
        public ActionResult AddMenu(string id)
        {
            var menuLevel1 = _moduleBizProcess.GetMainMenu();
            ViewBag.MenuLevel1List = menuLevel1.ListToSelectListItems(x => { return x.MenuName; },
                                x => { return x.SysNo.ToString(); }, AppendItemType.Select);

            ViewBag.MenuLevel2List = new List<MenuVM>().ListToSelectListItems(x => { return x.MenuName; },
                                x => { return x.SysNo.ToString(); }, AppendItemType.Select);

            ViewBag.navId = id;

            return View();
        }

        [HttpPost]
        [Description("[菜单管理首页]添加菜单动作")]
        [ActionName("Add")]
        public ActionResult AddMenu(MenuVM mainMenu, string navId)
        {
            if (ModelState.IsValid)
            {
                mainMenu.MenuLinkUrl = mainMenu.MenuAddLeve12.HasValue ? mainMenu.MenuLinkUrl : string.Empty;
                mainMenu.ModuleController = mainMenu.MenuAddLeve12.HasValue ? mainMenu.ModuleController : string.Empty;

                mainMenu.ParentNo = mainMenu.MenuAddLeve12 ?? mainMenu.MenuAddLevel1;
                mainMenu.Status = CommonStatus.Valid;
                mainMenu.IsLeaf = mainMenu.MenuAddLeve12.HasValue;
                mainMenu.CreateUserSysNo = AccountManager.Current.CurrentUser.SysNo;

                bool status = _moduleBizProcess.AddMenu(mainMenu);
                string message = status ? "添加主菜单成功" : "添加主菜单失败";
                return this.ReturnDWZOperate(navId, message, status, ReturnDWZType.ReturnRefrash);
            }

            return this.ReturnDWZError("验证信息失败");
        }

        [HttpGet]
        [Description("[菜单管理首页]更新菜单页面")]
        [ActionName("Update")]
        public ActionResult UpdateMenu(Guid? id, string navId)
        {
            if (id != null && id.HasValue)
            {
                ViewBag.navId = navId;
                MenuVM menu = _moduleBizProcess.GetMenuBySysno(id.Value);
                Dictionary<string, string> dicStatus = EnumHelper.GetEnumDescValue<CommonStatus>();
                ViewBag.StatusList = dicStatus.ToSelectListItems();
                ViewBag.Status = (int)menu.Status.Value;

                if (menu.ParentNo.HasValue)
                {
                    MenuVM parentNo = _moduleBizProcess.GetMenuBySysno(menu.ParentNo.Value);
                    if (parentNo.ParentNo.HasValue)
                    {
                        ViewBag.MenuUpdateLevel1 = parentNo.ParentNo.Value;
                        ViewBag.MenuUpdateLeve12 = parentNo.SysNo;
                    }
                    else
                        ViewBag.MenuUpdateLevel1 = parentNo.SysNo;
                }

                var menuLevel1 = _moduleBizProcess.GetMainMenu();
                ViewBag.MenuLevel1List = menuLevel1.ListToSelectListItems(x => { return x.MenuName; },
                                    x => { return x.SysNo.ToString(); }, AppendItemType.Select);

                var menuLevel2 = new List<MenuVM>();
                if (ViewBag.MenuUpdateLevel1 != null)
                    menuLevel2 = _moduleBizProcess.GetChildMenus(ViewBag.MenuUpdateLevel1);

                ViewBag.MenuLevel2List = menuLevel2.ListToSelectListItems(x => { return x.MenuName; },
                                    x => { return x.SysNo.ToString(); }, AppendItemType.Select);

                return View(menu);
            }
            else
            {
                return this.ReturnDWZError("获取主菜单失败，没有获取到可用的ID");
            }
        }

        [HttpPost]
        [Description("[菜单管理首页]更新菜单动作")]
        [ActionName("Update")]
        public JsonResult UpdateMenu(MenuVM mainMenu, string navId)
        {
            if (ModelState.IsValid)
            {
                Guid? parentNo = mainMenu.MenuUpdateLeve12 ?? mainMenu.MenuUpdateLevel1 ?? mainMenu.ParentNo;
                mainMenu.MenuLinkUrl = mainMenu.MenuUpdateLeve12.HasValue ? mainMenu.MenuLinkUrl : string.Empty;
                mainMenu.ModuleController = mainMenu.MenuUpdateLeve12.HasValue ? mainMenu.ModuleController : string.Empty;

                UpdateInfo updateInfo = new UpdateInfo()
                {
                    dynamicData = new
                                {
                                    mainMenu.SysNo,
                                    mainMenu.MenuName,
                                    mainMenu.Sort,
                                    mainMenu.IsSelected,
                                    IsLeaf = mainMenu.MenuUpdateLeve12.HasValue,
                                    ParentNo = parentNo,
                                    StatusDB = (int)mainMenu.Status,
                                    mainMenu.MenuLinkUrl,
                                    mainMenu.ModuleController,
                                    ModifyDate = DateTime.Now,
                                    ModifyUserSysNo = AccountManager.Current.CurrentUser.SysNo
                                }
                };

                bool status = _moduleBizProcess.UpdateMenu(updateInfo);
                string message = status ? "修改主菜单成功" : "修改主菜单失败";
                return this.ReturnDWZOperate(navId, message, status, ReturnDWZType.ReturnRefrash);
            }

            return this.ReturnDWZError("验证主菜单信息失败");
        }

        [HttpPost]
        [Description("[菜单管理首页]删除菜单动作（真删除）")]
        [ActionName("Delete")]
        public ActionResult DeleteMenu(Guid? id, string navId)
        {
            bool result = false;
            if (id != null && id.HasValue)
            {
                result = _moduleBizProcess.DeleteMenu(id.Value);
            }

            string message = result ? "删除主菜单成功" : "删除主菜单失败，没有获取到可用的ID";
            return this.ReturnDWZOperate(navId, message, result, ReturnDWZType.ReturnOnlyRefrash);
        }

        [HttpPost]
        [Description("获取页面左侧的导航菜单")]
        [LoginAllowView]
        public ActionResult GetLeftMenu(Guid? id)
        {
            MenuTierVM menu = AccountManager.Current.GetCurrentMenu().Find(x => x.SysNo == id.Value);
            List<MenuTierVM> list = new List<MenuTierVM>();
            if (menu != null)
                list = menu.ChildMenus;

            return View(list);
        }
    }
}
