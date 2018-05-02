using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.Framework.Logging;
using TMall.DomainModels.Base;

namespace TMall.DomainModels.Management
{
    public class ExceptionLogInfo : BaseEntity
    {
        public string LogLevelName { get; set; }

        public string Source { get; set; }

        public string EventType { get; set; }

        public string EventMessage { get; set; }

        public string EventDetail { get; set; }

        public string EventStackTrace { get; set; }

        public string InnerException { get; set; }

        public string IpAddress { get; set; }

        public string HostName { get; set; }

        public string PageUrl { get; set; }

        public string ReferrerUrl { get; set; }
    }
}
