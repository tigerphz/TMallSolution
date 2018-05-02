using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Base;
using TMall.DomainModule.Enums;

namespace TMall.DomainModels.Management
{
    public class MenuInfo : BaseEntity
    {
        public string MenuName { get; set; }

        public string MenuLinkUrl { get; set; }

        public string MenuIcon { get; set; }

        public Guid? ParentNo { get; set; }

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

        public bool IsLeaf { get; set; }

        public bool IsSelected { get; set; }

        public string ModuleController { get; set; }

        public virtual ICollection<MenuInfo> MenuInfos { get; set; }
    }
}
