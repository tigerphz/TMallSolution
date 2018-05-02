using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.ManageSite.IBizProcess;
using System.Web.Mvc;
using TMall.ManageSite.IBizProcess.Management;
using TMall.Infrastructure.SearchModel;
using TMall.Infrastructure.Data;
using TMall.Infrastructure.Web;
using TMall.DomainModule.Enums;
using TMall.ManageSite.ViewModel;
using System.ComponentModel;
using TMall.Infrastructure.Utility;
using TMall.ManageSite.Controllers.Attributes;

namespace TMall.ManageSite.Controllers.Management
{
    public class RoleController : BaseController
    {
        private IRoleBizProcess _roleBizProcess;

        public RoleController(IRoleBizProcess roleBizProcess)
        {
            _roleBizProcess = roleBizProcess;
        }

        [Description("[角色管理首页]")]
        [ViewPage]
        public ActionResult Index(Guid id, SearchCondition searchCondition, PageInfo pageInfo)
        {
            //检测分页信息
            PageInfoHelper.CheckedPageInfo(pageInfo, "Sort");
            SearchPageInfo<RoleVM> search = new SearchPageInfo<RoleVM>()
            {
                PageInfo = pageInfo,
                SearchCondition = searchCondition
            };

            search = _roleBizProcess.GetRoles(search);

            //设置分页信息到界面
            ViewData.SetPageInfo(search.PageInfo);
            //设置状态信息
            ViewBag.StatusList = DDLValueExtensions.EnumToSelectListItems<CommonStatus>(AppendItemType.All);

            //重新绑定查询信息到界面
            ViewData.ReBindSearchData(searchCondition);

            ViewBag.navId = id.ToString();
            return View(search.DataList);
        }

        [HttpGet]
        [Description("[角色管理首页]添加角色页面")]
        [ActionName("Add")]
        public ActionResult AddRole(string id)
        {
            ViewBag.navId = id;
            return View();
        }

        [HttpPost]
        [Description("[角色管理首页]添加角色动作")]
        [ActionName("Add")]
        [DWZExceptionResult]
        public ActionResult AddRole(RoleVM role, string navId)
        {
            if (ModelState.IsValid)
            {
                role.CreateDate = role.ModifyDate = DateTime.Now;
                role.Status = CommonStatus.Valid;
                role.CreateUserSysNo = AccountManager.Current.CurrentUser.SysNo;

                bool status = _roleBizProcess.AddRole(role);
                string message = status ? "添加角色成功" : "添加角色失败";
                return this.ReturnDWZOperate(navId, message, status, ReturnDWZType.ReturnRefrash);
            }

            return this.ReturnDWZError("验证信息失败");
        }

        [HttpGet]
        [Description("[角色管理首页]更新角色页面")]
        [ActionName("Update")]
        public ActionResult UpdateRole(Guid? id, string navId)
        {
            if (id != null && id.HasValue)
            {
                ViewBag.navId = navId;
                RoleVM menu = _roleBizProcess.GetRole(id.Value);
                Dictionary<string, string> dicStatus = EnumHelper.GetEnumDescValue<CommonStatus>();
                ViewBag.StatusList = dicStatus.ToSelectListItems();
                ViewBag.Status = (int)menu.Status.Value;

                return View(menu);
            }
            else
            {
                return this.ReturnDWZError("获取角色失败，没有获取到可用的ID");
            }
        }

        [HttpPost]
        [Description("[角色管理首页]更新角色动作")]
        [ActionName("Update")]
        public JsonResult UpdateRole(RoleVM role, string navId)
        {
            if (ModelState.IsValid)
            {
                UpdateInfo updateInfo = new UpdateInfo()
                {
                    dynamicData = new
                    {
                        role.SysNo,
                        role.RoleName,
                        StatusDB = (int)role.Status,
                        role.Description,
                        ModifyDate = DateTime.Now,
                        ModifyUserSysNo = AccountManager.Current.CurrentUser.SysNo
                    }
                };

                bool status = _roleBizProcess.UpdateRole(updateInfo);
                string message = status ? "修改角色成功" : "修改角色失败";
                return this.ReturnDWZOperate(navId, message, status, ReturnDWZType.ReturnRefrash);
            }

            return this.ReturnDWZError("验证角色信息失败");
        }

        [HttpPost]
        [Description("[角色管理首页]删除角色动作（真删除）")]
        [ActionName("Delete")]
        public ActionResult DeleteRole(Guid? id, string navId)
        {
            bool result = false;
            if (id != null && id.HasValue)
            {
                result = _roleBizProcess.DeleteRole(id.Value);
            }

            string message = result ? "删除主角色成功" : "删除主角色失败，没有获取到可用的ID";
            return this.ReturnDWZOperate(navId, message, result, ReturnDWZType.ReturnOnlyRefrash);
        }

        [HttpGet]
        [Description("[角色管理首页] 角色列表绑定权限页面")]
        public ActionResult BindIndex(string id, string type)
        {
            ViewBag.navId = id;
            ViewBag.type = type;

            return View(_roleBizProcess.GetRoles());
        }

        [HttpGet]
        [ActionName("BindPermission")]
        [Description("[角色管理首页] 单角色绑定权限页面")]
        public ActionResult BindPermission(Guid? id, string navId, string type)
        {
            if (id.HasValue)
            {
                ViewBag.permissionRoleId = id.Value;
                ViewBag.permissionNavId = navId;
                ViewBag.type = type;

                ViewBag.permissionData = _roleBizProcess.GetTreeGridJsonData(id.Value);
            }

            ViewBag.ErrorMessage = "传入的参数无效";
            return View();
        }

        [HttpPost]
        [ActionName("BindPermission")]
        [Description("[角色管理首页] 角色绑定权限设置")]
        public ActionResult SetBindPermission(Guid? id, string navId, string sysnoList)
        {
            if (id.HasValue)
            {
                List<Guid> list = sysnoList.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => { return new Guid(x); }).ToList();

                List<RolePermissionVM> rpList = new List<RolePermissionVM>();
                if (list != null && list.Count > 0)
                {
                    rpList = list.Select(x =>
                   {
                       return new RolePermissionVM()
                       {
                           CreateDate = DateTime.Now,
                           ModifyDate = DateTime.Now,
                           PermissionID = x,
                           RoleID = id.Value,
                           Status = CommonStatus.Valid,
                           CreateUserSysNo = AccountManager.Current.CurrentUser.SysNo,
                           ModifyUserSysNo = AccountManager.Current.CurrentUser.SysNo
                       };
                   }).ToList();
                }

                bool result = _roleBizProcess.BindPermission(id.Value, rpList);
                string message = result ? "绑定权限成功" : "绑定权限失败";
                return this.ReturnDWZOperate(navId, message, result, ReturnDWZType.ReturnSucc);
            }

            return this.ReturnDWZError("没有提交可用的权限信息");
        }
    }
}
