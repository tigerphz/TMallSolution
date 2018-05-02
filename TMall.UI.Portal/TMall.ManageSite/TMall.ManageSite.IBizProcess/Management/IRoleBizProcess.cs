using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.ManageSite.ViewModel;
using TMall.Infrastructure.Data;

namespace TMall.ManageSite.IBizProcess.Management
{
    public interface IRoleBizProcess
    {
        #region QuerySerivce

        /// <summary>
        /// 分页获取角色类别
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        SearchPageInfo<RoleVM> GetRoles(SearchPageInfo<RoleVM> search);

        /// <summary>
        /// 获取角色绑定过的权限集合
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        string GetTreeGridJsonData(Guid roleID);

        /// <summary>
        /// 获取全部可以的角色
        /// </summary>
        /// <returns></returns>
        List<RoleVM> GetRoles();

        #endregion

        #region ActionSerivce

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="RoleInfo"></param>
        /// <returns></returns>
        bool AddRole(RoleVM roleInfo);

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        bool UpdateRole(UpdateInfo updateInfo);

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="sysno"></param>
        /// <returns></returns>
        RoleVM GetRole(Guid sysno);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <returns></returns>
        bool DeleteRole(Guid sysno);

        /// <summary>
        /// 角色绑定权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="rolePermissionList"></param>
        /// <returns></returns>
        bool BindPermission(Guid roleID, List<RolePermissionVM> rolePermissionList);

        #endregion
    }
}
