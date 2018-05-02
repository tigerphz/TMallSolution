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
    public static class ManagerMapping
    {
        public static SysUserInfo ToModel(this SysUserVM sysUserModel)
        {
            return AutoMapper.Mapper.Map<SysUserInfo>(sysUserModel);
        }

        public static SysUserVM ToVM(this SysUserInfo sysUserInfo)
        {
            return AutoMapper.Mapper.Map<SysUserVM>(sysUserInfo);
        }

        public static List<SysUserVM> ToVM(this List<SysUserInfo> roleInfos)
        {
            return AutoMapper.Mapper.Map<List<SysUserVM>>(roleInfos);
        }

        public static SearchPageInfo<SysUserVM> ToVM(this SearchPageInfo<SysUserInfo> searchModule)
        {
            return AutoMapper.Mapper.Map<SearchPageInfo<SysUserVM>>(searchModule);
        }

        public static SearchPageInfo<SysUserInfo> ToModel(this SearchPageInfo<SysUserVM> searchMenu)
        {
            return AutoMapper.Mapper.Map<SearchPageInfo<SysUserInfo>>(searchMenu);
        }

        public static UserInfo ToUserInfo(this SysUserVM sysUserModel)
        {
            return AutoMapper.Mapper.Map<UserInfo>(sysUserModel);
        }
    }
}
