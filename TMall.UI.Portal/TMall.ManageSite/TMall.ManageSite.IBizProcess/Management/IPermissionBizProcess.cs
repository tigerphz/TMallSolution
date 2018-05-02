using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.ManageSite.ViewModel;
using TMall.Infrastructure.Data;

namespace TMall.ManageSite.IBizProcess.Management
{
    public interface IPermissionBizProcess
    {
        #region QuerySerivce

        /// <summary>
        /// 获取顶级权限比如 Index action
        /// </summary>
        /// <returns></returns>
        List<PermissionVM> GetTopPermissionList();

        /// <summary>
        /// 分页获取权限类别
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        SearchPageInfo<PermissionVM> GetPermissions(SearchPageInfo<PermissionVM> search);

        /// <summary>
        /// PermissionLookup分页获取权限类别
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        SearchPageInfo<PermissionVM> GetLookupPermissions(SearchPageInfo<PermissionVM> search);

        /// <summary>
        /// 获取所有有效的权限
        /// </summary>
        /// <returns></returns>
        List<PermissionVM> GetAllPermissions();

        /// <summary>
        /// 获取用户的权限
        /// </summary>
        /// <param name="userSysno"></param>
        /// <returns></returns>
        List<PermissionVM> GetPermissionByUserSysno(Guid userSysno);

        #endregion

        #region ActionSerivce

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="permissionInfo"></param>
        /// <returns></returns>
        bool AddPermission(PermissionVM permissionInfo);

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        bool UpdatePermission(UpdateInfo updateInfo);

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="sysno"></param>
        /// <returns></returns>
        PermissionVM GetPermission(Guid sysno);

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <returns></returns>
        bool DeletePermission(Guid sysno);

        #endregion
    }
}
