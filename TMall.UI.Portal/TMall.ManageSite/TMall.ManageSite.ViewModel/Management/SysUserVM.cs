using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TMall.DomainModule.Enums;

namespace TMall.ManageSite.ViewModel
{
    public class SysUserVM
    {
        public Guid SysNo { get; set; }

        [Display(Name = "用户名称")]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "手机号码")]
        public string Phone { get; set; }

        [Display(Name = "传真")]
        public string Fax { get; set; }

        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Display(Name = "QQ号码")]
        public string QQ { get; set; }

        [Display(Name = "昵称")]
        public string NickName { get; set; }

        [Display(Name = "居住地址")]
        public string Address { get; set; }

        [Display(Name = "真名")]
        public string RealName { get; set; }

        [Display(Name = "性别")]
        public int Gender { get; set; }

        [Display(Name = "状态")]
        public CommonStatus? Status { get; set; }

        public Guid? CreateUserSysNo { get; set; }

        public Guid? ModifyUserSysNo { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public string CreateUserName { get; set; }

        public string ModifyUserName { get; set; }

        /// <summary>
        /// 绑定的角色ID
        /// </summary>
        [Display(Name = "绑定角色")]
        public List<Guid> BindedRoleSysnos { get; set; }
    }
}
