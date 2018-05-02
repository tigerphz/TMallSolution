using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Management;
using System.Data.Entity;
using TMall.Infrastructure.Utility;
using TMall.Infrastructure.Data;
using TMall.Services.IRepository.Management;

namespace TMall.Services.Repository.Management
{
    public class SysUserRepository : EfRepositoryBase<SysUserInfo>, ISysUserRepository
    {
        public SysUserRepository(DbContext context)
            : base(context)
        {
            Check.NotNull(context, "value must not be null.");
        }

        public string GetPasswordSalt(string userName)
        {
            return this.Entities.Where(x => x.UserName.Equals(userName))
                .Select(x => x.PasswordSalt).FirstOrDefault();
        }
    }
}
