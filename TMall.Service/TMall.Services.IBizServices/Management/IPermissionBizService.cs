using TMall.DomainModels.Management;
using TMall.Infrastructure.Data;
using System.Collections.Generic;
using System;

namespace TMall.Services.IBizServices.Management
{
    public interface IPermissionBizService
    {
        #region QuerySerivce

        /// <summary>
        /// 获取顶级权限比如 Index action
        /// </summary>
        /// <returns></returns>
        List<PermissionInfo> GetTopPermissionList();

        /// <summary>
        /// 分页获取权限列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        SearchPageInfo<PermissionInfo> GetPermissions(SearchPageInfo<PermissionInfo> search);

        /// <summary>
        /// PermissionLookup分页获取权限类别
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        SearchPageInfo<PermissionInfo> GetLookupPermissions(SearchPageInfo<PermissionInfo> search);

        /// <summary>
        /// 获取所有有效的权限
        /// </summary>
        /// <returns></returns>
        List<PermissionInfo> GetAllPermissions();

        #endregion

        #region ActionSerivce

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="permissionInfo"></param>
        /// <returns></returns>
        bool AddPermission(PermissionInfo permissionInfo);

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="updateInfo"></param>
        /// <returns></returns>
        bool UpdatePermission(UpdateInfo updateInfo);

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="sysno"></param>
        /// <returns></returns>
        PermissionInfo GetPermission(Guid sysno);

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <returns></returns>
        bool DeletePermission(PermissionInfo permissionInfo);

        #endregion
    }
}
