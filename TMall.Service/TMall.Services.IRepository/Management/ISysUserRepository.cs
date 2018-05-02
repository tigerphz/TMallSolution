using System.Linq;
using TMall.DomainModels.Management;
using TMall.Infrastructure.Data;
using System;
using System.Collections.Generic;

namespace TMall.Services.IRepository.Management
{
    public interface ISysUserRepository : IRepository<SysUserInfo>
    {
        #region QuerySerivce

        /// <summary>
        /// 获取PasswordSalt
        /// </summary>
        /// <param name="ManagerName"></param>
        /// <returns></returns>
        string GetPasswordSalt(string userName);

        #endregion

        #region ActionSerivce

        #endregion
    }
}
