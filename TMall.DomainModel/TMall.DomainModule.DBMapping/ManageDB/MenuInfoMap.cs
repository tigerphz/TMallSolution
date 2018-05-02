using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Management;
using TMall.DomainModule.DBMapping.Base;

namespace TMall.DomainModule.DBMapping.ManageDB
{
    public class MenuInfoMap : EntityTypeConfigurationBase<MenuInfo>
    {
        public MenuInfoMap()
        {
            ToTable("tbMenu");

            Property(x => x.MenuName).HasMaxLength(30);
            Property(x => x.MenuLinkUrl).HasMaxLength(100);
            Property(x => x.MenuIcon).HasMaxLength(100);
            Property(x => x.ModuleController).HasMaxLength(30);

            //HasMany(x => x.MenuInfos)
            //    .WithOptional(x => x)
            //    .HasForeignKey(x => x.ParentNo);
        }
    }
}
