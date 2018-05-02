using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.Infrastructure.Utility;
using TMall.DomainModels.Customer;
using TMall.DomainModels.Management;
using TMall.Infrastructure.Data;

namespace TMall.ManageSite.ViewModel.Mapping
{
    public static class RoleMapping
    {
        public static RoleInfo ToModel(this RoleVM roleVM)
        {
            return AutoMapper.Mapper.Map<RoleInfo>(roleVM);
        }

        public static RoleVM ToVM(this RoleInfo roleInfo)
        {
            return AutoMapper.Mapper.Map<RoleVM>(roleInfo);
        }

        public static List<RoleVM> ToVM(this List<RoleInfo> roleInfos)
        {
            return AutoMapper.Mapper.Map<List<RoleVM>>(roleInfos);
        }

        public static SearchPageInfo<RoleVM> ToVM(this SearchPageInfo<RoleInfo> searchModule)
        {
            return AutoMapper.Mapper.Map<SearchPageInfo<RoleVM>>(searchModule);
        }

        public static SearchPageInfo<RoleInfo> ToModel(this SearchPageInfo<RoleVM> searchMenu)
        {
            return AutoMapper.Mapper.Map<SearchPageInfo<RoleInfo>>(searchMenu);
        }
    }
}
