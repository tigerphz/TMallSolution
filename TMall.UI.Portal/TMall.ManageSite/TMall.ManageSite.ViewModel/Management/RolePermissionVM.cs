using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModule.Enums;
using System.ComponentModel.DataAnnotations;

namespace TMall.ManageSite.ViewModel
{
    public class RolePermissionVM
    {
        public Guid SysNo { get; set; }

        public Guid? RoleID { get; set; }

        public Guid? PermissionID { get; set; }

        public CommonStatus? Status { get; set; }

        public PermissionVM PermissionInfo { get; set; }

        public RoleVM RoleInfo { get; set; }

        public Guid? CreateUserSysNo { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid? ModifyUserSysNo { get; set; }

        public DateTime ModifyDate { get; set; }

        public string CreateUserName { get; set; }

        public string ModifyUserName { get; set; }
    }
}
