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
    public static class RolePermissionPermissionMapping
    {
        public static RolePermissionInfo ToModel(this RolePermissionVM roleVM)
        {
            return AutoMapper.Mapper.Map<RolePermissionInfo>(roleVM);
        }

        public static RolePermissionVM ToVM(this RolePermissionInfo roleInfo)
        {
            return AutoMapper.Mapper.Map<RolePermissionVM>(roleInfo);
        }

        public static List<RolePermissionVM> ToVM(this List<RolePermissionInfo> roleInfos)
        {
            return AutoMapper.Mapper.Map<List<RolePermissionVM>>(roleInfos);
        }

        public static List<RolePermissionInfo> ToModel(this List<RolePermissionVM> roleVMs)
        {
            return AutoMapper.Mapper.Map<List<RolePermissionInfo>>(roleVMs);
        }
    }
}
