using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMall.ManageSite.ViewModel
{
    public class UserInfo
    {
        public Guid SysNo { get; set; }

        public string UserName { get; set; }

        public string NickName { get; set; }

        public string RealName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string QQ { get; set; }

        public string Address { get; set; }

        public int Gender { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public string CreateUserName { get; set; }

        public string ModifyUserName { get; set; }
    }
}