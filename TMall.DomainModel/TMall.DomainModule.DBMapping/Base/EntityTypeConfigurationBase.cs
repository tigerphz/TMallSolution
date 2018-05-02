using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using TMall.DomainModels.Base;

namespace TMall.DomainModule.DBMapping.Base
{
    public class EntityTypeConfigurationBase<TEntityType> :
        EntityTypeConfiguration<TEntityType> where TEntityType : BaseEntity, new()
    {
        public EntityTypeConfigurationBase()
        {
            HasKey(x => x.SysNo)
                .Property(x => x.SysNo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //这两个属性主要为了读取显示用
            Ignore(x => x.CreateUserName);
            Ignore(x => x.ModifyUserName);
        }
    }
}
