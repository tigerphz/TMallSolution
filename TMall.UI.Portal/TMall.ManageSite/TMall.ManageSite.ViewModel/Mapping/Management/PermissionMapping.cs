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
    public static class PermissionMapping
    {
        public static PermissionInfo ToModel(this PermissionVM permissionVM)
        {
            return AutoMapper.Mapper.Map<PermissionInfo>(permissionVM);
        }

        public static PermissionVM ToVM(this PermissionInfo permissionInfo)
        {
            return AutoMapper.Mapper.Map<PermissionVM>(permissionInfo);
        }

        public static List<PermissionVM> ToVM(this List<PermissionInfo> permissionInfos)
        {
            return AutoMapper.Mapper.Map<List<PermissionVM>>(permissionInfos);
        }

        public static SearchPageInfo<PermissionVM> ToVM(this SearchPageInfo<PermissionInfo> searchModule)
        {
            return AutoMapper.Mapper.Map<SearchPageInfo<PermissionVM>>(searchModule);
        }

        public static SearchPageInfo<PermissionInfo> ToModel(this SearchPageInfo<PermissionVM> searchMenu)
        {
            return AutoMapper.Mapper.Map<SearchPageInfo<PermissionInfo>>(searchMenu);
        }
    }
}
