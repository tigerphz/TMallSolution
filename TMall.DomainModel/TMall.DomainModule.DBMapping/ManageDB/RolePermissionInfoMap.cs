﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Management;
using TMall.DomainModule.DBMapping.Base;

namespace TMall.DomainModule.DBMapping.ManageDB
{
    public class RolePermissionInfoMap : EntityTypeConfigurationBase<RolePermissionInfo>
    {
        public RolePermissionInfoMap()
        {
            ToTable("tbRolePermission");
        }
    }
}
