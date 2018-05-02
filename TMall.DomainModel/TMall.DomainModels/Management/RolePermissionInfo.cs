using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Base;
using TMall.DomainModule.Enums;

namespace TMall.DomainModels.Management
{
    public class RolePermissionInfo : BaseEntity
    {
        public Guid? RoleID { get; set; }

        public Guid? PermissionID { get; set; }

        public int StatusDB { get; set; }

        public CommonStatus? Status
        {
            get
            {
                return (CommonStatus)Enum.Parse(typeof(CommonStatus), StatusDB.ToString());
            }
            set { StatusDB = (int)value; }
        }

        public virtual PermissionInfo PermissionInfo { get; set; }

        public virtual RoleInfo RoleInfo { get; set; }
    }
}
