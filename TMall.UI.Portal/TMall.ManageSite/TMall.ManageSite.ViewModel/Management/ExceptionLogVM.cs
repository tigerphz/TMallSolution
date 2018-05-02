using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModule.Enums;
using System.ComponentModel.DataAnnotations;

namespace TMall.ManageSite.ViewModel
{
    public class ExceptionLogVM
    {
        public Guid SysNo { get; set; }

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

        public Guid? CreateUserSysNo { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid? ModifyUserSysNo { get; set; }

        public DateTime ModifyDate { get; set; }

        public string CreateUserName { get; set; }

        public string ModifyUserName { get; set; }
    }
}
