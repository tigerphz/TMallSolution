using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Management;
using TMall.DomainModule.DBMapping.Base;

namespace TMall.DomainModule.DBMapping.ManageDB
{
    public class SysUserInfoMap : EntityTypeConfigurationBase<SysUserInfo>
    {
        public SysUserInfoMap()
        {
            ToTable("tbSysUser");

            Property(x => x.UserName).HasMaxLength(30);
            Property(x => x.PasswordHash).HasColumnType("char").HasMaxLength(50);
            Property(x => x.PasswordSalt).HasColumnType("char").HasMaxLength(32);
            Property(x => x.Phone).HasMaxLength(30);
            Property(x => x.Fax).HasMaxLength(30);
            Property(x => x.Email).HasMaxLength(30);
            Property(x => x.QQ).HasMaxLength(30);
            Property(x => x.NickName).HasMaxLength(30);
            Property(x => x.Address).HasMaxLength(30);
            Property(x => x.RealName).HasMaxLength(30);

            //建立一对多关系
            HasMany(x => x.UserRoles)
                .WithOptional(x => x.SysUserInfo)
                .HasForeignKey(x => x.UserID);
        }
    }
}
