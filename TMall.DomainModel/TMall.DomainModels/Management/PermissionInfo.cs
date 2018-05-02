using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Base;
using TMall.DomainModule.Enums;

namespace TMall.DomainModels.Management
{
    public class PermissionInfo : BaseEntity
    {
        public string PermissionAction { get; set; }

        public string PermissionName { get; set; }

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

        public string Script { get; set; }

        public string Icon { get; set; }

        public string PermissionController { get; set; }

        public string Description { get; set; }

        public bool IsButton { get; set; }

        public Guid? ParentID { get; set; }

        public virtual ICollection<RolePermissionInfo> RolePermissionInfos { get; set; }
    }
}
