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
    public static class SysUserRoleMaping
    {
        public static SysUserRoleInfo ToModel(this SysUserRoleVM roleVM)
        {
            return AutoMapper.Mapper.Map<SysUserRoleInfo>(roleVM);
        }

        public static SysUserRoleVM ToVM(this SysUserRoleInfo roleInfo)
        {
            return AutoMapper.Mapper.Map<SysUserRoleVM>(roleInfo);
        }

        public static List<SysUserRoleVM> ToVM(this List<SysUserRoleInfo> roleInfos)
        {
            return AutoMapper.Mapper.Map<List<SysUserRoleVM>>(roleInfos);
        }

        public static List<SysUserRoleInfo> ToModel(this List<SysUserRoleVM> roleVMs)
        {
            return AutoMapper.Mapper.Map<List<SysUserRoleInfo>>(roleVMs);
        }
    }
}
