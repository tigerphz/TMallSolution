using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Management;

namespace TMall.ManageSite.ViewModel.Mapping
{
    public static class MenuTierMapping
    {
        public static List<MenuTierVM> ToVM(this List<MenuTier> menuInfo)
        {
            return AutoMapper.Mapper.Map<List<MenuTierVM>>(menuInfo);
        }
    }
}
