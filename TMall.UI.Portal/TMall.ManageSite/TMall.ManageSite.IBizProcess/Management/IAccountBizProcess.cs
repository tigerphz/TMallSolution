using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.ManageSite.ViewModel;
using TMall.Infrastructure.Data;

namespace TMall.ManageSite.IBizProcess.Management
{
    public interface IAccountBizProcess
    {
        #region QuerySerivce

        SysUserVM Login(string sysUserName, string password);

        bool IsExistSysUser(string sysUserName, string password);

        bool IsExistSysUser(string userName);

        /// <summary>
        /// 分页获取权限类别
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        SearchPageInfo<SysUserVM> GetSysUsers(SearchPageInfo<SysUserVM> search);

        /// <summary>
        /// 获取用户绑定过的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<RoleVM> GetBindRole(Guid userId);

        #endregion

        #region ActionSerivce

        bool AddSysUser(SysUserVM sysUserModel);

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        bool UpdateSysUser(UpdateInfo updateInfo);

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="sysno"></param>
        /// <returns></returns>
        SysUserVM GetSysUser(Guid sysno);

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <returns></returns>
        bool DeleteSysUser(Guid sysno);

        /// <summary>
        /// 用户绑定角色
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userRoleList"></param>
        /// <returns></returns>
        bool BindRole(Guid userID, List<SysUserRoleVM> userRoleList);

        #endregion
    }
}
