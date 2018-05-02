using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMall.DomainModels.Management
{
    /// <summary>
    /// 菜单层级关系
    /// </summary>
    public class MenuTier
    {
        public MenuTier()
        {
            ChildMenus = new List<MenuTier>();
        }

        public Guid SysNo { get; set; }

        public string MenuName { get; set; }

        public string MenuLinkUrl { get; set; }

        public string MenuIcon { get; set; }

        public bool IsSelected { get; set; }

        public int Sort { get; set; }

        public List<MenuTier> ChildMenus { get; set; }
    }
}
