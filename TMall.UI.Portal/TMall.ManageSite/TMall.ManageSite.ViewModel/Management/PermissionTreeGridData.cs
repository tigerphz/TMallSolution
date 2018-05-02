using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TMall.ManageSite.ViewModel
{
    [Serializable]
    public class PermissionTreeGridData
    {
        public string Identifier { get; set; }

        public Guid sysno { get; set; }

        public string name { get; set; }

        public string status { get; set; }

        public string controller { get; set; }

        public string action { get; set; }

        /// <summary>
        /// 网页url 按钮
        /// </summary>
        public string type { get; set; }

        public string desc { get; set; }

        /// <summary>
        /// 该权限是否被当前角色选中
        /// </summary>
        public bool ischecked { get; set; }

        public string _parentId { get; set; }

        public PermissionTreeGridData()
        {
            _parentId = "";
        }
    }
}
