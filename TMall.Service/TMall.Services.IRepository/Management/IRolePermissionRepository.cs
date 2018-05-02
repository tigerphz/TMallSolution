using System.Linq;
using TMall.DomainModels.Management;
using TMall.Infrastructure.Data;
using System;
using System.Collections.Generic;

namespace TMall.Services.IRepository.Management
{
    public interface IRolePermissionRepository : IRepository<RolePermissionInfo>
    {
        #region QuerySerivce
        

        #endregion

        #region ActionSerivce

        /// <summary>
        /// 角色绑定权限
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="rolePermissionList"></param>
        /// <returns></returns>
        bool BindPermission(Guid roleID, List<RolePermissionInfo> rolePermissionList);

        #endregion
    }
}
