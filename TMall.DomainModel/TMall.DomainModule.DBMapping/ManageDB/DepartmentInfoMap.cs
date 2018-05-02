using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Management;
using TMall.DomainModule.DBMapping.Base;

namespace TMall.DomainModule.DBMapping.ManageDB
{
    public class DepartmentInfoMap : EntityTypeConfigurationBase<DepartmentInfo>
    {
        public DepartmentInfoMap()
        {
            ToTable("tbDepartment");

            Property(x => x.DeptDescription).HasMaxLength(200);
            Property(x => x.DeptName).HasMaxLength(30);

            //建立一对多关系
            HasMany(x => x.Users)
                .WithOptional(x => x.DepartmentInfo)
                .HasForeignKey(x => x.DeptID)
                .WillCascadeOnDelete(false);
        }
    }
}
