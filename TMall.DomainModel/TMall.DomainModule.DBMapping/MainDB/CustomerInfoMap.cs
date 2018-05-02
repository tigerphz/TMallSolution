using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using TMall.DomainModels.Customer;
using TMall.DomainModule.DBMapping.Base;

namespace TMall.DomainModule.DBMapping.MainDB
{
    public class CustomerInfoMap : EntityTypeConfigurationBase<CustomerInfo>
    {
        public CustomerInfoMap()
        {
            ToTable("tbCustomer");
            HasEntitySetName("tbCustomer");

            Property(x => x.CustomerName).IsRequired().HasMaxLength(32);
            Property(x => x.PasswordHash).IsRequired().HasColumnType("char").HasMaxLength(50);
            Property(x => x.PasswordSalt).IsRequired().HasColumnType("char").HasMaxLength(32);
            Property(x => x.RealName).HasMaxLength(50);
            Property(x => x.NickName).HasMaxLength(50);

            Property(x => x.Gender).IsRequired().HasColumnType("TINYINT");
            Property(x => x.CellPhone).HasMaxLength(50);

            Property(x => x.Phone).HasMaxLength(50);
            Property(x => x.CellPhone).HasMaxLength(50);
            Property(x => x.IdentityCard).HasMaxLength(50);
            Property(x => x.Fax).HasMaxLength(20);
            Property(x => x.Email).HasMaxLength(25);
            Property(x => x.QQ).HasMaxLength(25);

            Property(x => x.DwellAddress).HasMaxLength(200);
            Property(x => x.DwellZip).HasMaxLength(50);
            Property(x => x.ReceiveName).HasMaxLength(50);
            Property(x => x.ReceiveContact).HasMaxLength(50);

            Property(x => x.ReceivePhone).HasMaxLength(50);
            Property(x => x.ReceiveCellPhone).HasMaxLength(50);
            Property(x => x.ReceiveFax).HasMaxLength(50);
            Property(x => x.ReceiveAddress).HasMaxLength(50);
            Property(x => x.ReceiveZip).HasMaxLength(50);

            Property(x => x.FromLinkSource).HasMaxLength(100);
            Property(x => x.Note).HasMaxLength(500);
        }
    }
}
