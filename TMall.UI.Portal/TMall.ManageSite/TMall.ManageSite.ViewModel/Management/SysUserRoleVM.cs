using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TMall.DomainModule.Enums;

namespace TMall.ManageSite.ViewModel
{
    public class SysUserRoleVM
    {
        public Guid? UserID { get; set; }

        public Guid? RoleID { get; set; }

        public CommonStatus? Status { get; set; }

        public RoleVM RoleInfo { get; set; }

        public SysUserVM SysUserInfo { get; set; }

        public Guid? CreateUserSysNo { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid? ModifyUserSysNo { get; set; }

        public DateTime ModifyDate { get; set; }
    }
}
