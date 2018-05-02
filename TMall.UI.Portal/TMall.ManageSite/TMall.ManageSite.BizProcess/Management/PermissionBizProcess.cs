using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.ManageSite.IBizProcess;
using TMall.ManageSite.ViewModel;
using TMall.ManageSite.ViewModel.Mapping;
using TMall.DomainModels.Management;
using TMall.Framework.ServiceLocation;
using TMall.DomainModule.Enums;
using TMall.Infrastructure.Data;
using TMall.Services.BizServices.Management;
using TMall.Infrastructure.Core;
using TMall.Services.IBizServices.Management;
using System.Web.Mvc;
using TMall.ManageSite.IBizProcess.Management;

namespace TMall.ManageSite.BizProcess.Management
{
    public class PermissionBizProcess : IPermissionBizProcess
    {
        private IPermissionBizService _permissionBizService;

        public PermissionBizProcess(IPermissionBizService permissionBizService)
        {
            this._permissionBizService = permissionBizService;
        }

        #region QuerySerivce

        /// <summary>
        /// 获取顶级权限比如 Index action
        /// </summary>
        /// <returns></returns>
        public List<PermissionVM> GetTopPermissionList()
        {
            return _permissionBizService.GetTopPermissionList().ToVM();
        }

        /// <summary>
        /// 分页获取权限类别
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public SearchPageInfo<PermissionVM> GetPermissions(SearchPageInfo<PermissionVM> search)
        {
            SearchPageInfo<PermissionInfo> whereSearch = search.ToModel();
            return _permissionBizService.GetPermissions(whereSearch).ToVM();
        }

        /// <summary>
        /// PermissionLookup分页获取权限类别
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public SearchPageInfo<PermissionVM> GetLookupPermissions(SearchPageInfo<PermissionVM> search)
        {
            SearchPageInfo<PermissionInfo> whereSearch = search.ToModel();
            return _permissionBizService.GetLookupPermissions(whereSearch).ToVM();
        }

        /// <summary>
        /// 获取所有有效的权限
        /// </summary>
        /// <returns></returns>
        public List<PermissionVM> GetAllPermissions()
        {
            return _permissionBizService.GetAllPermissions().ToVM();
        }

        /// <summary>
        /// 获取用户的权限
        /// </summary>
        /// <param name="userSysno"></param>
        /// <returns></returns>
        public List<PermissionVM> GetPermissionByUserSysno(Guid userSysno)
        {
            RolePermissionFace face = EngineContext.Current.Resolve<RolePermissionFace>();
            return face.GetPermissionByUserSysno(userSysno).ToVM();
        }

        #endregion

        #region ActionSerivce

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="permissionInfo"></param>
        /// <returns></returns>
        public bool AddPermission(PermissionVM permissionInfo)
        {
            return _permissionBizService.AddPermission(permissionInfo.ToModel());
        }

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public bool UpdatePermission(UpdateInfo updateInfo)
        {
            return _permissionBizService.UpdatePermission(updateInfo);
        }

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="sysno"></param>
        /// <returns></returns>
        public PermissionVM GetPermission(Guid sysno)
        {
            return _permissionBizService.GetPermission(sysno).ToVM();
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <returns></returns>
        public bool DeletePermission(Guid sysno)
        {
            PermissionInfo permissionInfo = new PermissionInfo() { SysNo = sysno };
            return _permissionBizService.DeletePermission(permissionInfo);
        }

        #endregion
    }
}
