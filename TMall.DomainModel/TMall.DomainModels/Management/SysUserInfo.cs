using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Base;
using TMall.DomainModule.Enums;

namespace TMall.DomainModels.Management
{
    public class SysUserInfo : BaseEntity
    {
        public string UserName { get; set; }

        /// <summary>
        /// 通过PasswordSalt混淆加密后的密码散列码
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// 混淆码
        /// </summary>
        public string PasswordSalt { get; set; }

        public Guid? DeptID { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string QQ { get; set; }

        public string NickName { get; set; }

        public string Address { get; set; }

        public string RealName { get; set; }

        public int Gender { get; set; }

        public int StatusDB { get; set; }

        public CommonStatus? Status
        {
            get
            {
                return (CommonStatus)Enum.Parse(typeof(CommonStatus), StatusDB.ToString());
            }
            set { StatusDB = (int)value; }
        }

        public virtual DepartmentInfo DepartmentInfo { get; set; }

        public virtual ICollection<SysUserRoleInfo> UserRoles { get; set; }
    }
}