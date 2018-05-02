using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Management;
using TMall.Infrastructure.Data;
using TMall.Services.IRepository.Management;
using System.Data.Entity;
using TMall.Infrastructure.Utility;

namespace TMall.Services.Repository.Management
{
    public class ExceptionLogRepository : EfRepositoryBase<ExceptionLogInfo>, IExceptionLogRepository
    {
        public ExceptionLogRepository(DbContext context)
            : base(context)
        {
            Check.NotNull(context, "value must not be null.");
        }
    }
}
