using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.Infrastructure.Utility;
using TMall.DomainModels.Management;
using TMall.Infrastructure.Data;

namespace TMall.ManageSite.ViewModel.Mapping
{
    public static class MenuMapping
    {
        public static MenuInfo ToModel(this MenuVM menuVM)
        {
            return AutoMapper.Mapper.Map<MenuInfo>(menuVM);
        }

        public static MenuVM ToVM(this MenuInfo menuInfo)
        {
            return AutoMapper.Mapper.Map<MenuVM>(menuInfo);
        }

        public static List<MenuVM> ToVM(this List<MenuInfo> menuInfos)
        {
            return AutoMapper.Mapper.Map<List<MenuVM>>(menuInfos);
        }

        public static SearchPageInfo<MenuVM> ToVM(this SearchPageInfo<MenuInfo> searchModule)
        {
            return AutoMapper.Mapper.Map<SearchPageInfo<MenuVM>>(searchModule);
        }

        public static SearchPageInfo<MenuInfo> ToModel(this SearchPageInfo<MenuVM> searchMenu)
        {
            return AutoMapper.Mapper.Map<SearchPageInfo<MenuInfo>>(searchMenu);
        }
    }
}
