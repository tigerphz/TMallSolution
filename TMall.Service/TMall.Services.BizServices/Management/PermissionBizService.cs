using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Management;
using TMall.Infrastructure.Utility;
using TMall.Infrastructure.Data;
using TMall.Services.IBizServices.Management;
using TMall.Services.IRepository.Management;
using TMall.Framework.ExceptionHanding;
using TMall.DomainModule.Enums;
using AutoMapper;
using TMall.Framework.Caching;
using TMall.Infrastructure.Core.InterceptionBehaviors;

namespace TMall.Services.BizServices.Management
{
    public class PermissionBizService : IPermissionBizService
    {
        private IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PermissionBizService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = _permissionRepository.UnitOfWork;
        }

        #region QuerySerivce

        /// <summary>
        /// 获取顶级权限比如 Index action
        /// </summary>
        /// <returns></returns>
        public List<PermissionInfo> GetTopPermissionList()
        {
            var data = _permissionRepository.Entities.Where(x => x.ParentID.HasValue == false &&
                x.IsButton == false &&
                x.StatusDB == (int)CommonStatus.Valid)
                .Select(x =>
                     new
                     {
                         x.SysNo,
                         x.PermissionName,
                         x.PermissionController,
                         x.PermissionAction
                     }
                ).ToList();

            return data.Select(Mapper.DynamicMap<PermissionInfo>).ToList();
        }

        /// <summary>
        /// 分页获取权限列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public SearchPageInfo<PermissionInfo> GetPermissions(SearchPageInfo<PermissionInfo> search)
        {
            Check.NotNull(search, "search");
            Check.NotNull(search.PageInfo, "search.PageInfo");
            if (string.IsNullOrEmpty(search.PageInfo.OrderField))
            {
                throw ExceptionHelper.ThrowBusinessException("分页获取数据必须指定排序列");
            }

            search = _permissionRepository.GetPageEntities(search);

            return search;
        }

        /// <summary>
        /// PermissionLookup分页获取权限类别
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public SearchPageInfo<PermissionInfo> GetLookupPermissions(SearchPageInfo<PermissionInfo> search)
        {
            Check.NotNull(search, "search");
            Check.NotNull(search.PageInfo, "search.PageInfo");
            if (string.IsNullOrEmpty(search.PageInfo.OrderField))
            {
                throw ExceptionHelper.ThrowBusinessException("分页获取数据必须指定排序列");
            }

            search = _permissionRepository.GetPageEntities(search, 
                                        x => x.ParentID.HasValue == false);

            return search;
        }

        /// <summary>
        /// 获取所有有效的权限
        /// </summary>
        /// <returns></returns>
        [Caching(CachingMethod.Get, ExpiryType = ExpirationType.Never)] //永久缓存
        public List<PermissionInfo> GetAllPermissions()
        {
            return _permissionRepository.Entities.Where(x => x.StatusDB == (int)CommonStatus.Valid).ToList();
        }

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="sysno"></param>
        /// <returns></returns>
        public PermissionInfo GetPermission(Guid sysno)
        {
            return _permissionRepository.GetByKey(sysno);
        }

        #endregion

        #region ActionSerivce

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="permissionInfo"></param>
        /// <returns></returns>
        [Caching(CachingMethod.Remove, CorrespondingMethodNames = new[] { "GetAllPermissions"})]
        public bool AddPermission(PermissionInfo permissionInfo)
        {
            permissionInfo.SysNo = EntityGuid.NewComb();
            return _permissionRepository.Insert(permissionInfo) > 0;
        }

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="updateInfo"></param>
        /// <returns></returns>
        [Caching(CachingMethod.Remove, CorrespondingMethodNames = new[] { "GetAllPermissions" })]
        public bool UpdatePermission(UpdateInfo updateInfo)
        {
            var propertyList = updateInfo.GetPropertyNames();
            if (propertyList == null || propertyList.Length == 0)
                return false;

            _permissionRepository.UpdateEntity(updateInfo.GetData<PermissionInfo>(), propertyList);

            return _unitOfWork.Commit() > 0;
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <returns></returns>
        [Caching(CachingMethod.Remove, CorrespondingMethodNames = new[] { "GetAllPermissions" })]
        public bool DeletePermission(PermissionInfo permissionInfo)
        {
            return _permissionRepository.Delete(permissionInfo) > 0;
        }

        #endregion
    }
}
