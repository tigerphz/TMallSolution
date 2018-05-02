using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMall.ManageSite.ViewModel
{
    public class MenuTierVM
    {
        public Guid SysNo { get; set; }

        public string MenuName { get; set; }

        public string MenuLinkUrl { get; set; }

        public string MenuIcon { get; set; }

        public string NavId { get; set; }

        public int Sort { get; set; }

        public bool IsSelected { get; set; }

        public List<MenuTierVM> ChildMenus { get; set; }
    }
}
