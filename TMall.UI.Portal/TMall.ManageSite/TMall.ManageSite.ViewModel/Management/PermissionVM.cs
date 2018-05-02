using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModule.Enums;
using System.ComponentModel.DataAnnotations;

namespace TMall.ManageSite.ViewModel
{
    public class PermissionVM
    {
        public Guid SysNo { get; set; }

        [Display(Name = "权限动作")]
        public string PermissionAction { get; set; }

        [Display(Name = "权限名称")]
        public string PermissionName { get; set; }

        [Display(Name = "顺序")]
        public int Sort { get; set; }

        [Display(Name = "状态")]
        public CommonStatus? Status { get; set; }

        [Display(Name = "权限图标")]
        public string Icon { get; set; }

        [Display(Name = "权限控制器")]
        public string PermissionController { get; set; }

        [Display(Name = "描述")]
        public string Description { get; set; }

        [Display(Name = "是否按钮")]
        public bool IsButton { get; set; }

        public Guid? ParentID { get; set; }

        public Guid? CreateUserSysNo { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid? ModifyUserSysNo { get; set; }

        public DateTime ModifyDate { get; set; }

        public string CreateUserName { get; set; }

        public string ModifyUserName { get; set; }

        /// <summary>
        /// 该权限是否被当前角色选中
        /// </summary>
        public bool IsChecked { get; set; }
    }
}
