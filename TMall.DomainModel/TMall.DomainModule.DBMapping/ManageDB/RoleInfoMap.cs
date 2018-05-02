using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using TMall.DomainModels.Management;
using TMall.DomainModule.DBMapping.Base;

namespace TMall.DomainModule.DBMapping.ManageDB
{
    public class RoleInfoMap : EntityTypeConfigurationBase<RoleInfo>
    {
        public RoleInfoMap()
        {
            ToTable("tbRole");

            Property(x => x.RoleName).HasMaxLength(30);
            Property(x => x.Description).HasMaxLength(200);

            HasMany(x => x.UserRoles)
                .WithOptional(x => x.RoleInfo)
                .HasForeignKey(x => x.RoleID);

            HasMany(x => x.RolePermissions)
                .WithOptional(x => x.RoleInfo)
                .HasForeignKey(x => x.RoleID);
        }
    }
}