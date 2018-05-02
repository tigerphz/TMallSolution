using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModule.Enums;
using System.ComponentModel.DataAnnotations;

namespace TMall.ManageSite.ViewModel
{
    public class MenuVM
    {
        public Guid SysNo { get; set; }

        [Display(Name = "菜单名称")]
        public string MenuName { get; set; }

        [Display(Name = "菜单图标")]
        public string MenuIcon { get; set; }

        [Display(Name = "链接地址")]
        public string MenuLinkUrl { get; set; }

        [Display(Name = "控制器名称")]
        public string ModuleController { get; set; }

        public Guid? ParentNo { get; set; }

        [Display(Name = "菜单顺序")]
        public int Sort { get; set; }

        [Display(Name = "状态")]
        public CommonStatus? Status { get; set; }

        public bool IsLeaf { get; set; }

        [Display(Name = "默认选中")]
        public bool IsSelected { get; set; }

        public Guid? CreateUserSysNo { get; set; }

        public Guid? ModifyUserSysNo { get; set; }

        public string CreateUserName { get; set; }

        public string ModifyUserName { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public Guid? MenuAddLevel1 { get; set; }

        public Guid? MenuAddLeve12 { get; set; }

        public Guid? MenuUpdateLevel1 { get; set; }

        public Guid? MenuUpdateLeve12 { get; set; }
    }
}
