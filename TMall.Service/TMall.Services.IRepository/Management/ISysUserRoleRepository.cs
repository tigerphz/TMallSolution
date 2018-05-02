using System.Linq;
using TMall.DomainModels.Management;
using TMall.Infrastructure.Data;
using System;
using System.Collections.Generic;

namespace TMall.Services.IRepository.Management
{
    public interface ISysUserRoleRepository : IRepository<SysUserRoleInfo>
    {
        #region QuerySerivce


        #endregion

        #region ActionSerivce

        /// <summary>
        /// 用户绑定角色
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="rolePermissionList"></param>
        /// <returns></returns>
        bool BindRole(Guid userID, List<SysUserRoleInfo> userRoleList);

        #endregion
    }
}
