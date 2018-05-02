using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.ManageSite.IBizProcess;
using TMall.ManageSite.ViewModel;
using TMall.ManageSite.ViewModel.Mapping;
using TMall.DomainModels.Management;
using TMall.Framework.ServiceLocation;
using TMall.DomainModule.Enums;
using TMall.Infrastructure.Data;
using TMall.Services.BizServices.Management;
using TMall.Infrastructure.Core;
using TMall.Services.IBizServices.Management;
using System.Web.Mvc;
using TMall.ManageSite.IBizProcess.Management;

namespace TMall.ManageSite.BizProcess.Management
{
    public class ExceptionLogBizProcess : IExceptionLogBizProcess
    {
        private IExceptionLogBizService _exceptionLogBizService;

        public ExceptionLogBizProcess(IExceptionLogBizService exceptionLogBizService)
        {
            this._exceptionLogBizService = exceptionLogBizService;
        }
    }
}
