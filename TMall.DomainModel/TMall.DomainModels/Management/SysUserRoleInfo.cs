using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Base;
using TMall.DomainModule.Enums;

namespace TMall.DomainModels.Management
{
    public class SysUserRoleInfo : BaseEntity
    {
        public Guid? UserID { get; set; }

        public Guid? RoleID { get; set; }

        public int StatusDB { get; set; }

        public CommonStatus? Status
        {
            get
            {
                return (CommonStatus)Enum.Parse(typeof(CommonStatus), StatusDB.ToString());
            }
            set { StatusDB = (int)value; }
        }

        public virtual RoleInfo RoleInfo { get; set; }

        public virtual SysUserInfo SysUserInfo { get; set; }
    }
}
