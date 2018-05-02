using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.ManageSite.IBizProcess;
using TMall.ManageSite.ViewModel;
using TMall.ManageSite.ViewModel.Mapping;
using TMall.DomainModels.Management;
using TMall.Framework.ServiceLocation;
using TMall.Services.BizServices.Management;
using TMall.Infrastructure.Core;
using TMall.Services.IBizServices.Management;
using TMall.ManageSite.IBizProcess.Management;
using TMall.Infrastructure.Data;

namespace TMall.ManageSite.BizProcess.Management
{
    public class AccountBizProcess : IAccountBizProcess
    {
        private ISysUserBizService _sysUserBizService;

        public AccountBizProcess(ISysUserBizService sysUserBizService)
        {
            this._sysUserBizService = sysUserBizService;
        }

        #region QuerySerivce

        /// <summary>
        /// 检测用户是否存在
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public SysUserVM Login(string sysUserName, string password)
        {
            SysUserInfo sysUserInfo = _sysUserBizService.GetSysUser(sysUserName, password);
            return sysUserInfo.ToVM();
        }

        /// <summary>
        /// 用户是否存在
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsExistSysUser(string sysUserName, string password)
        {
            return this._sysUserBizService.IsExistSysUser(sysUserName, password);
        }

        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsExistSysUser(string userName)
        {
            return _sysUserBizService.IsExistSysUser(userName);
        }

        /// <summary>
        /// 分页获取权限类别
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public SearchPageInfo<SysUserVM> GetSysUsers(SearchPageInfo<SysUserVM> search)
        {
            SearchPageInfo<SysUserInfo> whereSearch = search.ToModel();
            return _sysUserBizService.GetSysUsers(whereSearch).ToVM();
        }

        /// <summary>
        /// 获取用户绑定过的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<RoleVM> GetBindRole(Guid userId)
        {
            IRoleBizService roleService = EngineContext.Current.Resolve<IRoleBizService>();
            SysUserRoleFace face = EngineContext.Current.Resolve<SysUserRoleFace>();

            List<RoleVM> roleList = roleService.GetRoles().ToVM();
            List<RoleVM> bindedRole = face.GetBindedRole(userId).ToVM();

            roleList.ForEach(x =>
            {
                x.IsSelected = bindedRole.Exists(y => { return y.SysNo == x.SysNo; });
            });

            return roleList;
        }

        #endregion

        #region ActionSerivce

        /// <summary>
        /// 创建新用户
        /// </summary>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        public bool AddSysUser(SysUserVM sysUserModel)
        {
            SysUserInfo sysUser = sysUserModel.ToModel();
            return this._sysUserBizService.AddSysUser(sysUser);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public bool UpdateSysUser(UpdateInfo updateInfo)
        {
            return _sysUserBizService.UpdateSysUser(updateInfo);
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="sysno"></param>
        /// <returns></returns>
        public SysUserVM GetSysUser(Guid sysno)
        {
            return _sysUserBizService.GetSysUser(sysno).ToVM();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <returns></returns>
        public bool DeleteSysUser(Guid sysno)
        {
            SysUserInfo permissionInfo = new SysUserInfo() { SysNo = sysno };
            return _sysUserBizService.DeleteSysUser(permissionInfo);
        }

        /// <summary>
        /// 用户绑定角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="rolePermissionList"></param>
        /// <returns></returns>
        public bool BindRole(Guid userID, List<SysUserRoleVM> userRoleList)
        {
            return _sysUserBizService.BindRole(userID, userRoleList.ToModel());
        }

        #endregion
    }
}
