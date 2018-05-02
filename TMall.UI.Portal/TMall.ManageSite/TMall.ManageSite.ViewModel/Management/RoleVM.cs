using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModule.Enums;
using System.ComponentModel.DataAnnotations;

namespace TMall.ManageSite.ViewModel
{
    public class RoleVM
    {
        public Guid SysNo { get; set; }

        [Display(Name = "角色名称")]
        public string RoleName { get; set; }

        [Display(Name = "顺序")]
        public int Sort { get; set; }

        [Display(Name = "状态")]
        public CommonStatus? Status { get; set; }

        [Display(Name = "描述")]
        public string Description { get; set; }

        public Guid? CreateUserSysNo { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid? ModifyUserSysNo { get; set; }

        public DateTime ModifyDate { get; set; }

        public string CreateUserName { get; set; }

        public string ModifyUserName { get; set; }

        /// <summary>
        /// 是否已经绑定
        /// </summary>
        public bool IsSelected { get; set; }
    }
}
