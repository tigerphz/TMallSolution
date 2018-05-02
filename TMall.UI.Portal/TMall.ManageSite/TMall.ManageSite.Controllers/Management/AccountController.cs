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
using TMall.Infrastructure.Core;
using TMall.ManageSite.Controllers.Attributes;

namespace TMall.ManageSite.Controllers.Management
{
    public class AccountController : BaseController
    {
        private IAccountBizProcess _accountBizProcess;

        public AccountController(IAccountBizProcess accountBizProcess)
        {
            _accountBizProcess = accountBizProcess;
        }

        [Description("[系统用户管理首页]")]
        [ViewPage]
        public ActionResult Index(Guid id, SearchCondition searchCondition, PageInfo pageInfo)
        {
            //检测分页信息
            PageInfoHelper.CheckedPageInfo(pageInfo, "CreateDate", SortOrder.desc);
            SearchPageInfo<SysUserVM> search = new SearchPageInfo<SysUserVM>()
            {
                PageInfo = pageInfo,
                SearchCondition = searchCondition
            };

            search = _accountBizProcess.GetSysUsers(search);

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
        [Description("[系统用户管理首页]添加系统用户页面")]
        [ActionName("Add")]
        public ActionResult AddSysUser(string id)
        {
            ViewBag.navId = id;
            return View();
        }

        [HttpPost]
        [Description("[系统用户管理首页]添加系统用户动作")]
        [ActionName("Add")]
        public ActionResult AddSysUser(SysUserVM account, string navId)
        {
            if (ModelState.IsValid)
            {
                account.CreateDate = account.ModifyDate = DateTime.Now;
                account.Status = CommonStatus.Valid;
                account.CreateUserSysNo = AccountManager.Current.CurrentUser.SysNo;

                bool status = _accountBizProcess.AddSysUser(account);
                string message = status ? "添加系统用户成功" : "添加系统用户失败";
                return this.ReturnDWZOperate(navId, message, status, ReturnDWZType.ReturnRefrash);
            }

            return this.ReturnDWZError("验证信息失败");
        }

        [HttpGet]
        [Description("[系统用户管理首页]更新系统用户页面")]
        [ActionName("Update")]
        public ActionResult UpdateSysUser(Guid? id, string navId)
        {
            if (id != null && id.HasValue)
            {
                ViewBag.navId = navId;
                SysUserVM menu = _accountBizProcess.GetSysUser(id.Value);
                Dictionary<string, string> dicStatus = EnumHelper.GetEnumDescValue<CommonStatus>();
                ViewBag.StatusList = dicStatus.ToSelectListItems();
                ViewBag.Status = (int)menu.Status.Value;

                return View(menu);
            }
            else
            {
                return this.ReturnDWZError("获取系统用户失败，没有获取到可用的ID");
            }
        }

        [HttpPost]
        [Description("[系统用户管理首页]更新系统用户动作")]
        [ActionName("Update")]
        public JsonResult UpdateSysUser(SysUserVM account, string navId)
        {
            if (ModelState.IsValid)
            {
                UpdateInfo updateInfo = new UpdateInfo()
                {
                    dynamicData = new
                    {
                        SysNo = account.SysNo,
                        Phone = account.Phone,
                        QQ = account.QQ,
                        NickName = account.NickName,
                        Email = account.Email,
                        Gender = account.Gender,
                        Address = account.Address,
                        StatusDB = (int)account.Status,
                        ModifyDate = DateTime.Now,
                        ModifyUserSysNo = AccountManager.Current.CurrentUser.SysNo
                    }
                };

                bool status = _accountBizProcess.UpdateSysUser(updateInfo);
                string message = status ? "修改系统用户成功" : "修改系统用户失败";
                return this.ReturnDWZOperate(navId, message, status, ReturnDWZType.ReturnRefrash);
            }

            return this.ReturnDWZError("验证系统用户信息失败");
        }

        [HttpPost]
        [Description("[系统用户管理首页]删除系统用户动作（真删除）")]
        [ActionName("Delete")]
        public ActionResult DeleteSysUser(Guid? id, string navId)
        {
            bool result = false;
            if (id != null && id.HasValue)
            {
                result = _accountBizProcess.DeleteSysUser(id.Value);
            }

            string message = result ? "删除主系统用户成功" : "删除主系统用户失败，没有获取到可用的ID";
            return this.ReturnDWZOperate(navId, message, result, ReturnDWZType.ReturnOnlyRefrash);
        }

        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [ActionName("ValidatName")]
        [Description("[系统用户管理首页]检测用户是否存在")]
        [HttpGet]
        [LoginAllowView]
        public JsonResult IsExistSysUser(string UserName)
        {
            bool isValidate = false;
            isValidate = !_accountBizProcess.IsExistSysUser(UserName);
            return Json(isValidate, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用户绑定角色
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("[系统用户管理首页]用户绑定角色页面")]
        [ActionName("BindRole")]
        public ActionResult BindRole(Guid? id, string navId)
        {
            if (id.HasValue)
            {
                ViewBag.userId = id.Value;
                ViewBag.navId = navId;

                List<RoleVM> roleList = _accountBizProcess.GetBindRole(id.Value);
                return View(roleList);
            }

            return this.ReturnDWZError("传入的参数无效");
        }

        /// <summary>
        /// 用户绑定角色
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("[系统用户管理首页]用户绑定角色设置")]
        [ActionName("BindRole")]
        public ActionResult SetBindRole(Guid? userId, string navId, List<Guid> roleSysnos)
        {
            if (userId.HasValue)
            {
                ViewBag.UserId = userId.Value;
                ViewBag.NavId = navId;

                List<SysUserRoleVM> userRoleList = new List<SysUserRoleVM>();
                if (roleSysnos != null && roleSysnos.Count > 0)
                {
                    roleSysnos.ForEach(x =>
                    {
                        userRoleList.Add(new SysUserRoleVM()
                        {
                            RoleID = x,
                            UserID = userId.Value,
                            CreateDate = DateTime.Now,
                            ModifyDate = DateTime.Now,
                            Status = CommonStatus.Valid,
                            CreateUserSysNo = AccountManager.Current.CurrentUser.SysNo,
                            ModifyUserSysNo = AccountManager.Current.CurrentUser.SysNo
                        });
                    });
                }

                bool result = _accountBizProcess.BindRole(userId.Value, userRoleList);
                string message = result ? "绑定角色成功" : "绑定角色失败";
                return this.ReturnDWZOperate(navId, message, result, ReturnDWZType.ReturnSuccAndClose);
            }

            return this.ReturnDWZError("没有提交可用的权限信息");
        }
    }
}
