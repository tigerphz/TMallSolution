using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TMall.ManageSite.ViewModel
{
    public class LoginVM
    {
        [Display(Name = "用户名")]
        public string SysUserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string PasswordHash { get; set; }

        [Display(Name = "记住我")]
        public bool RememberMe { get; set; }

        [Display(Name = "验证码")]        
        public string ValidateCode { get; set; }
    }
}
