using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModule.DBMapping.Base;
using TMall.DomainModels.Management;

namespace TMall.DomainModule.DBMapping.ManageDB
{
    public class ExceptionLogInfoMap : EntityTypeConfigurationBase<ExceptionLogInfo>
    {
        public ExceptionLogInfoMap()
        {
            ToTable("tbExceptionLog");

            Property(x => x.LogLevelName).HasMaxLength(30);
            Property(x => x.Source).HasMaxLength(100);
            Property(x => x.EventType).HasMaxLength(100);
            Property(x => x.EventMessage).HasMaxLength(100);
            Property(x => x.EventDetail).HasMaxLength(300);
            Property(x => x.EventStackTrace).HasMaxLength(300);
            Property(x => x.InnerException).HasMaxLength(300);
            Property(x => x.IpAddress).HasMaxLength(30);
            Property(x => x.HostName).HasMaxLength(30);
            Property(x => x.PageUrl).HasMaxLength(100);
            Property(x => x.ReferrerUrl).HasMaxLength(100);
        }
    }
}
