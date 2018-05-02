using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMall.ManageSite.IBizProcess.Management;
using System.ComponentModel;
using TMall.Infrastructure.SearchModel;
using TMall.Infrastructure.Data;
using TMall.ManageSite.ViewModel;
using TMall.Infrastructure.Utility;
using TMall.Infrastructure.Web;
using TMall.DomainModule.Enums;
using TMall.ManageSite.Controllers.Attributes;

namespace TMall.ManageSite.Controllers.Management
{
    public class PermissionController : BaseController
    {
        private IPermissionBizProcess _permissionBizProcess;

        public PermissionController(IPermissionBizProcess permissionBizProcess)
        {
            _permissionBizProcess = permissionBizProcess;
        }

        [Description("[权限管理首页]")]
        [ViewPage]
        public ActionResult Index(Guid id, SearchCondition searchCondition, PageInfo pageInfo)
        {
            //检测分页信息
            PageInfoHelper.CheckedPageInfo(pageInfo, "PermissionController");
            SearchPageInfo<PermissionVM> search = new SearchPageInfo<PermissionVM>()
            {
                PageInfo = pageInfo,
                SearchCondition = searchCondition
            };

            search = _permissionBizProcess.GetPermissions(search);

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
        [Description("[权限管理首页]添加权限页面")]
        [ActionName("Add")]
        public ActionResult AddPermission(string id)
        {
            ViewBag.navId = id;
            return View();
        }

        [HttpPost]
        [Description("[权限管理首页]添加权限动作")]
        [ActionName("Add")]
        public ActionResult AddPermission(PermissionVM permission, string navId)
        {
            if (ModelState.IsValid)
            {
                permission.CreateDate = permission.ModifyDate = DateTime.Now;
                permission.Status = CommonStatus.Valid;
                permission.CreateUserSysNo = AccountManager.Current.CurrentUser.SysNo;

                bool status = _permissionBizProcess.AddPermission(permission);
                string message = status ? "添加权限成功" : "添加权限失败";
                return this.ReturnDWZOperate(navId, message, status, ReturnDWZType.ReturnRefrash);
            }

            return this.ReturnDWZError("验证信息失败");
        }

        [HttpGet]
        [Description("[权限管理首页]更新权限页面")]
        [ActionName("Update")]
        public ActionResult UpdatePermission(Guid? id, string navId)
        {
            if (id != null && id.HasValue)
            {
                ViewBag.navId = navId;
                PermissionVM permission = _permissionBizProcess.GetPermission(id.Value);
                Dictionary<string, string> dicStatus = EnumHelper.GetEnumDescValue<CommonStatus>();
                List<SelectListItem> itemList = dicStatus.ToSelectListItems();
                itemList.ForEach(x =>
                {
                    if (x.Value == ((int)permission.Status.Value).ToString())
                        x.Selected = true;
                });

                ViewBag.StatusList = itemList;

                ViewBag.topName = string.Empty;
                if (permission.ParentID.HasValue)
                {
                    PermissionVM topPermission = _permissionBizProcess.GetPermission(permission.ParentID.Value);
                    ViewBag.topName = string.Format("{0} {1} {2}", topPermission.PermissionName, topPermission.PermissionController, topPermission.PermissionAction);
                }

                return View(permission);
            }
            else
            {
                return this.ReturnDWZError("获取权限失败，没有获取到可用的ID");
            }
        }

        [HttpPost]
        [Description("[权限管理首页]更新权限动作")]
        [ActionName("Update")]
        public JsonResult UpdatePermission(PermissionVM permission, string navId)
        {
            if (ModelState.IsValid)
            {
                UpdateInfo updateInfo = new UpdateInfo()
                {
                    dynamicData = new
                    {
                        permission.ParentID,
                        permission.SysNo,
                        permission.PermissionName,
                        permission.PermissionController,
                        permission.PermissionAction,
                        permission.IsButton,
                        permission.Description,
                        StatusDB = (int)permission.Status,
                        ModifyDate = DateTime.Now,
                        ModifyUserSysNo = AccountManager.Current.CurrentUser.SysNo
                    }
                };

                bool status = _permissionBizProcess.UpdatePermission(updateInfo);
                string message = status ? "修改权限成功" : "修改权限失败";
                return this.ReturnDWZOperate(navId, message, status, ReturnDWZType.ReturnRefrash);
            }

            return this.ReturnDWZError("验证权限信息失败");
        }

        [HttpPost]
        [Description("[权限管理首页]删除权限动作（真删除）")]
        [ActionName("Delete")]
        public ActionResult DeletePermission(Guid? id, string navId)
        {
            bool result = false;
            if (id != null && id.HasValue)
            {
                result = _permissionBizProcess.DeletePermission(id.Value);
            }

            string message = result ? "删除主权限成功" : "删除主权限失败，没有获取到可用的ID";
            return this.ReturnDWZOperate(navId, message, result, ReturnDWZType.ReturnOnlyRefrash);
        }

        [Description("[权限管理首页] 查找根权限lookup控件首页")]
        [ActionName("PermissionLookup")]
        [LoginAllowView]
        public ActionResult PermissionLookup(Guid id, SearchCondition searchCondition, PageInfo pageInfo)
        {
            //检测分页信息
            PageInfoHelper.CheckedPageInfo(pageInfo, "PermissionController");
            SearchPageInfo<PermissionVM> search = new SearchPageInfo<PermissionVM>()
            {
                PageInfo = pageInfo,
                SearchCondition = searchCondition
            };

            search = _permissionBizProcess.GetLookupPermissions(search);

            //设置分页信息到界面
            ViewData.SetPageInfo(search.PageInfo);
            //设置状态信息
            ViewBag.StatusList = DDLValueExtensions.EnumToSelectListItems<CommonStatus>(AppendItemType.All);
            //重新绑定查询信息到界面
            ViewData.ReBindSearchData(searchCondition);

            ViewBag.navId = id.ToString();
            return View(search.DataList);
        }
    }
}
