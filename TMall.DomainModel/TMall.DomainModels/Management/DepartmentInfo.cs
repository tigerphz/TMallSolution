using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Base;
using TMall.DomainModule.Enums;

namespace TMall.DomainModels.Management
{
    public class DepartmentInfo : BaseEntity
    {
        public string DeptName { get; set; }

        public string DeptDescription { get; set; }

        public Guid? ParentID { get; set; }

        public int StatusDB { get; set; }

        public CommonStatus? Status
        {
            get
            {
                return (CommonStatus)Enum.Parse(typeof(CommonStatus), StatusDB.ToString());
            }
            set { StatusDB = (int)value; }
        }

        public virtual ICollection<SysUserInfo> Users { get; set; }
    }
}
