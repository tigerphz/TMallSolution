using TMall.DomainModels.Management;
using TMall.Infrastructure.Data;
using System;
using System.Collections.Generic;

namespace TMall.Services.IBizServices.Management
{
    public interface IRoleBizService
    {
        #region QuerySerivce

        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        SearchPageInfo<RoleInfo> GetRoles(SearchPageInfo<RoleInfo> search);

        /// <summary>
        /// 获取全部可以的角色
        /// </summary>
        /// <returns></returns>
        List<RoleInfo> GetRoles();

        #endregion

        #region ActionSerivce

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="RoleInfo"></param>
        /// <returns></returns>
        bool AddRole(RoleInfo roleInfo);

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="updateInfo"></param>
        /// <returns></returns>
        bool UpdateRole(UpdateInfo updateInfo);

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="sysno"></param>
        /// <returns></returns>
        RoleInfo GetRole(Guid sysno);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <returns></returns>
        bool DeleteRole(RoleInfo roleInfo);

        /// <summary>
        /// 角色绑定权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="rolePermissionList"></param>
        /// <returns></returns>
        bool BindPermission(Guid roleId, List<RolePermissionInfo> rolePermissionList);

        #endregion
    }
}
