using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.Infrastructure.Utility;
using TMall.DomainModels.Customer;
using TMall.DomainModels.Management;
using TMall.Infrastructure.Data;

namespace TMall.ManageSite.ViewModel.Mapping
{
    public static class ExceptionLogMapping
    {
        public static ExceptionLogInfo ToModel(this ExceptionLogVM exceptionLogVM)
        {
            return AutoMapper.Mapper.Map<ExceptionLogInfo>(exceptionLogVM);
        }

        public static ExceptionLogVM ToVM(this ExceptionLogInfo exceptionLog)
        {
            return AutoMapper.Mapper.Map<ExceptionLogVM>(exceptionLog);
        }

        public static List<ExceptionLogVM> ToVM(this List<ExceptionLogInfo> exceptionLogs)
        {
            return AutoMapper.Mapper.Map<List<ExceptionLogVM>>(exceptionLogs);
        }

        public static SearchPageInfo<ExceptionLogVM> ToVM(this SearchPageInfo<ExceptionLogInfo> searchModule)
        {
            return AutoMapper.Mapper.Map<SearchPageInfo<ExceptionLogVM>>(searchModule);
        }

        public static SearchPageInfo<ExceptionLogInfo> ToModel(this SearchPageInfo<ExceptionLogVM> searchMenu)
        {
            return AutoMapper.Mapper.Map<SearchPageInfo<ExceptionLogInfo>>(searchMenu);
        }
    }
}
