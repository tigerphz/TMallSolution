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
    public class SysUserRoleRepository : EfRepositoryBase<SysUserRoleInfo>, ISysUserRoleRepository
    {
        public SysUserRoleRepository(DbContext context)
            : base(context)
        {
            Check.NotNull(context, "value must not be null.");
        }

        #region QuerySerivce


        #endregion

        #region ActionSerivce

        /// <summary>
        /// 用户绑定角色
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="rolePermissionList"></param>
        /// <returns></returns>
        public bool BindRole(Guid userID, List<SysUserRoleInfo> userRoleList)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //先删除之前绑定的角色
                this.Entities.Delete(x => x.UserID.Value == userID);
                userRoleList.ForEach(x => { x.SysNo = EntityGuid.NewComb(); });
                //添加新绑定的角色
                this.Insert(userRoleList, false);
                bool result = userRoleList.Count == 0 || this.UnitOfWork.Commit() > 0;
                scope.Complete();
                return result;
            }
        }

        #endregion

    }
}
