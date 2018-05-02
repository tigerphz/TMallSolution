﻿using System;
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
    public class RoleRepository : EfRepositoryBase<RoleInfo>, IRoleRepository
    {
        public RoleRepository(DbContext context)
            : base(context)
        {
            Check.NotNull(context, "value must not be null.");
        }
    }
}
