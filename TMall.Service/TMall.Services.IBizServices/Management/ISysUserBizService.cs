using TMall.DomainModels.Management;
using TMall.Infrastructure.Data;
using System;
using System.Collections.Generic;

namespace TMall.Services.IBizServices.Management
{
    public interface ISysUserBizService
    {
        #region QuerySerivce

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="managerName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool IsExistSysUser(string userName, string password);

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="managerName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        SysUserInfo GetSysUser(string userName, string password);

        /// <summary>
        /// 检测用户名是否存在
        /// </summary>
        /// <param name="managerName"></param>
        /// <returns></returns>
        bool IsExistSysUser(string userName);

        /// <summary>
        /// 分页获取系统用户列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        SearchPageInfo<SysUserInfo> GetSysUsers(SearchPageInfo<SysUserInfo> search);

        #endregion

        #region ActionSerivce

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="managerInfo"></param>
        /// <returns></returns>
        bool AddSysUser(SysUserInfo sysUserInfo);

        /// <summary>
        /// 更新系统用户
        /// </summary>
        /// <param name="updateInfo"></param>
        /// <returns></returns>
        bool UpdateSysUser(UpdateInfo updateInfo);

        /// <summary>
        /// 获取系统用户
        /// </summary>
        /// <param name="sysno"></param>
        /// <returns></returns>
        SysUserInfo GetSysUser(Guid sysno);

        /// <summary>
        /// 删除系统用户
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <returns></returns>
        bool DeleteSysUser(SysUserInfo sysUserInfo);

        /// <summary>
        /// 用户绑定角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="rolePermissionList"></param>
        /// <returns></returns>
        bool BindRole(Guid userID, List<SysUserRoleInfo> userRoleList);

        #endregion
    }
}
