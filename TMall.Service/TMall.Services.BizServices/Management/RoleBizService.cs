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
using TMall.Infrastructure.Core.InterceptionBehaviors;
using TMall.Framework.Caching;

namespace TMall.Services.BizServices.Management
{
    public class RoleBizService : IRoleBizService
    {
        private IRoleRepository _roleRepository;
        private IRolePermissionRepository _rolePermissionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoleBizService(IRoleRepository roleRepository,
                              IRolePermissionRepository rolePermissionRepository)
        {
            _roleRepository = roleRepository;
            _rolePermissionRepository = rolePermissionRepository;

            _unitOfWork = _roleRepository.UnitOfWork;
        }

        #region QuerySerivce

        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public SearchPageInfo<RoleInfo> GetRoles(SearchPageInfo<RoleInfo> search)
        {
            Check.NotNull(search, "search");
            Check.NotNull(search.PageInfo, "search.PageInfo");
            if (string.IsNullOrEmpty(search.PageInfo.OrderField))
            {
                throw ExceptionHelper.ThrowBusinessException("分页获取数据必须指定排序列");
            }

            search = _roleRepository.GetPageEntities(search);

            return search;
        }

        /// <summary>
        /// 获取全部可以的角色
        /// </summary>
        /// <returns></returns>
        [Caching(CachingMethod.Get, ExpiryType = ExpirationType.Never)] //永久缓存
        public List<RoleInfo> GetRoles()
        {
            return _roleRepository.Entities
                        .Where(x => x.StatusDB == (int)CommonStatus.Valid)
                        .OrderBy(x => x.Sort)
                        .ToList();
        }

        #endregion

        #region ActionSerivce

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="RoleInfo"></param>
        /// <returns></returns>
        [Caching(CachingMethod.Remove, CorrespondingMethodNames = new[] { "GetRoles" })]
        public bool AddRole(RoleInfo roleInfo)
        {
            roleInfo.SysNo = EntityGuid.NewComb();
            return _roleRepository.Insert(roleInfo) > 0;
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="updateInfo"></param>
        /// <returns></returns>
        [Caching(CachingMethod.Remove, CorrespondingMethodNames = new[] { "GetRoles" })]
        public bool UpdateRole(UpdateInfo updateInfo)
        {
            var propertyList = updateInfo.GetPropertyNames();
            if (propertyList == null || propertyList.Length == 0)
                return false;

            _roleRepository.UpdateEntity(updateInfo.GetData<RoleInfo>(), propertyList);

            return _unitOfWork.Commit() > 0;
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="sysno"></param>
        /// <returns></returns>
        public RoleInfo GetRole(Guid sysno)
        {
            return _roleRepository.GetByKey(sysno);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <returns></returns>
        [Caching(CachingMethod.Remove, CorrespondingMethodNames = new[] { "GetRoles" })]
        public bool DeleteRole(RoleInfo roleInfo)
        {
            return _roleRepository.Delete(roleInfo) > 0;
        }


        /// <summary>
        /// 角色绑定权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="rolePermissionList"></param>
        /// <returns></returns>
        public bool BindPermission(Guid roleID, List<RolePermissionInfo> rolePermissionList)
        {
            return _rolePermissionRepository.BindPermission(roleID, rolePermissionList);
        }

        #endregion
    }
}