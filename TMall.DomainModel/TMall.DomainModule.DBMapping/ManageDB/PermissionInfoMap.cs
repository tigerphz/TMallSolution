using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using TMall.DomainModels.Base;
using TMall.DomainModels.Management;
using TMall.DomainModule.DBMapping.Base;

namespace TMall.DomainModule.DBMapping.ManageDB
{
    public class PermissionInfoMap : EntityTypeConfigurationBase<PermissionInfo>
    {
        public PermissionInfoMap()
        {
            ToTable("tbPermission");

            Property(x => x.PermissionName).HasMaxLength(30);
            Property(x => x.PermissionAction).HasMaxLength(30);
            Property(x => x.PermissionController).HasMaxLength(30);
            Property(x => x.Description).HasMaxLength(200);
            Property(x => x.Icon).HasMaxLength(100);
            Property(x => x.Script).HasMaxLength(100);

            HasMany(x => x.RolePermissionInfos)
                .WithOptional(x => x.PermissionInfo)
                .HasForeignKey(x => x.PermissionID);
        }
    }
}
