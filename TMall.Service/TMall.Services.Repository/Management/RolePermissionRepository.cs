using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Management;
using System.Data.Entity;
using TMall.Infrastructure.Utility;
using TMall.Infrastructure.Data;
using TMall.Services.IRepository.Management;
using EntityFramework.Extensions;
using System.Transactions;

namespace TMall.Services.Repository.Management
{
    public class RolePermissionRepository : EfRepositoryBase<RolePermissionInfo>, IRolePermissionRepository
    {
        public RolePermissionRepository(DbContext context)
            : base(context)
        {
            Check.NotNull(context, "value must not be null.");
        }

        #region QuerySerivce


        #endregion

        #region ActionSerivce

        /// <summary>
        /// 角色绑定权限
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="rolePermissionList"></param>
        /// <returns></returns>
        public bool BindPermission(Guid roleID, List<RolePermissionInfo> rolePermissionList)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                //先删除之前绑定的权限
                this.Entities.Delete(x => x.RoleID.Value == roleID);
                rolePermissionList.ForEach(x => { x.SysNo = EntityGuid.NewComb(); });
                //添加新绑定的权限
                this.Insert(rolePermissionList, false);
                bool result = rolePermissionList.Count == 0 || this.UnitOfWork.Commit() > 0;
                ts.Complete();
                return result;
            }
        }

        #endregion

    }
}
