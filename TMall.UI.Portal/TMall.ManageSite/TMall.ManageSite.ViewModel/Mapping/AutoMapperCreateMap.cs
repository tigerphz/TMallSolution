using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using TMall.DomainModels.Management;
using TMall.Infrastructure.Data;

namespace TMall.ManageSite.ViewModel.Mapping
{
    /// <summary>
    /// 初始化所有需要映射的类的映射关系
    /// </summary>
    public class AutoMapperCreateMap
    {
        /// <summary>
        /// 初始化映射
        /// </summary>
        public void CreateMap()
        {
            #region 管理模块

            #region SysUserVM SysUserInfo

            Mapper.CreateMap<SysUserInfo, SysUserVM>();
            Mapper.CreateMap<SysUserVM, SysUserInfo>()
                .ForMember(x => x.CreateDate, x => x.Ignore())
                .ForMember(x => x.PasswordHash, x => x.MapFrom(y => y.Password));

            Mapper.CreateMap<SearchPageInfo<SysUserInfo>, SearchPageInfo<SysUserVM>>();
            Mapper.CreateMap<SearchPageInfo<SysUserVM>, SearchPageInfo<SysUserInfo>>();

            Mapper.CreateMap<SysUserVM, UserInfo>();

            #endregion

            #region SysUserRoleVM SysUserRoleInfo

            Mapper.CreateMap<SysUserRoleInfo, SysUserRoleVM>();
            Mapper.CreateMap<SysUserRoleVM, SysUserRoleInfo>()
                .ForMember(x => x.RoleInfo, x => x.Ignore())
                .ForMember(x => x.SysUserInfo, x => x.Ignore());

            #endregion

            #region MenuVM MenuInfo

            Mapper.CreateMap<MenuInfo, MenuVM>();
            Mapper.CreateMap<MenuVM, MenuInfo>()
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore());

            Mapper.CreateMap<SearchPageInfo<MenuInfo>, SearchPageInfo<MenuVM>>();
            Mapper.CreateMap<SearchPageInfo<MenuVM>, SearchPageInfo<MenuInfo>>();

            #endregion

            #region MenuTierVM MenuTier

            Mapper.CreateMap<MenuTier, MenuTierVM>();
            Mapper.CreateMap<MenuTierVM, MenuTier>();

            #endregion

            #region RoleVM RoleInfo

            Mapper.CreateMap<RoleInfo, RoleVM>();
            Mapper.CreateMap<RoleVM, RoleInfo>()
                .ForMember(x => x.CreateDate, x => x.Ignore());

            Mapper.CreateMap<SearchPageInfo<RoleInfo>, SearchPageInfo<RoleVM>>();
            Mapper.CreateMap<SearchPageInfo<RoleVM>, SearchPageInfo<RoleInfo>>();

            #endregion

            #region ExceptionLogVM ExceptionLogInfo

            Mapper.CreateMap<ExceptionLogInfo, ExceptionLogVM>();
            Mapper.CreateMap<ExceptionLogVM, ExceptionLogInfo>()
                .ForMember(x => x.CreateDate, x => x.Ignore());

            Mapper.CreateMap<SearchPageInfo<ExceptionLogInfo>, SearchPageInfo<ExceptionLogVM>>();
            Mapper.CreateMap<SearchPageInfo<ExceptionLogVM>, SearchPageInfo<ExceptionLogInfo>>();

            #endregion

            #region PermissionVM PermissionInfo

            Mapper.CreateMap<PermissionInfo, PermissionVM>();
            Mapper.CreateMap<PermissionVM, PermissionInfo>()
                .ForMember(x => x.CreateDate, x => x.Ignore());

            Mapper.CreateMap<SearchPageInfo<PermissionInfo>, SearchPageInfo<PermissionVM>>();
            Mapper.CreateMap<SearchPageInfo<PermissionVM>, SearchPageInfo<PermissionInfo>>();

            #endregion

            #region RolePermissionVM RolePermissionInfo

            Mapper.CreateMap<RolePermissionInfo, RolePermissionVM>();
            Mapper.CreateMap<RolePermissionVM, RolePermissionInfo>()
                .ForMember(x => x.PermissionInfo, x => x.Ignore())
                .ForMember(x => x.RoleInfo, x => x.Ignore());

            #endregion


            #endregion

            #region

            #endregion
        }
    }
}
