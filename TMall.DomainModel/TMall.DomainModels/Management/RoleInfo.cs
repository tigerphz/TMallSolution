using System;
using System.Collections.Generic;
using TMall.DomainModels.Base;
using TMall.DomainModule.Enums;

namespace TMall.DomainModels.Management
{
    public class RoleInfo : BaseEntity
    {
        public string RoleName { get; set; }

        public int Sort { get; set; }

        public int StatusDB { get; set; }

        public CommonStatus? Status
        {
            get
            {
                return (CommonStatus)Enum.Parse(typeof(CommonStatus), StatusDB.ToString());
            }
            set { StatusDB = (int)value; }
        }

        public string Description { get; set; }

        public virtual ICollection<SysUserRoleInfo> UserRoles { get; set; }

        public virtual ICollection<RolePermissionInfo> RolePermissions { get; set; }
    }
}
