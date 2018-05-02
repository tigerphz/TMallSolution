using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.Services.IRepository.Management;
using TMall.Infrastructure.Data;
using TMall.DomainModels.Management;
using TMall.DomainModule.Enums;

namespace TMall.Services.BizServices.Management
{
    public class RolePermissionFace
    {
        private IRoleRepository _roleRepository;
        private IRolePermissionRepository _rolePermissionRepository;
        private IPermissionRepository _permissionRepository;
        private ISysUserRoleRepository _sysUserRoleRepository;

        private readonly IUnitOfWork _unitOfWork;

        public RolePermissionFace(IRoleRepository roleRepository,
                              IRolePermissionRepository rolePermissionRepository,
                              IPermissionRepository permissionRepository,
                              ISysUserRoleRepository sysUserRoleRepository)
        {
            _roleRepository = roleRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _permissionRepository = permissionRepository;
            _sysUserRoleRepository = sysUserRoleRepository;

            _unitOfWork = _roleRepository.UnitOfWork;
        }

        /// <summary>
        /// 获取角色已经绑定的权限列表
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<PermissionInfo> GetBindedPermission(Guid roleID)
        {
            return (from rp in _rolePermissionRepository.Entities.Where(x => x.RoleID.Value == roleID)
                    join p in _permissionRepository.Entities
                    on rp.PermissionID equals p.SysNo
                    orderby p.PermissionController, p.PermissionAction
                    select p).ToList();
        }


        /// <summary>
        /// 获取用户的权限
        /// </summary>
        /// <param name="userSysno"></param>
        /// <returns></returns>
        public List<PermissionInfo> GetPermissionByUserSysno(Guid userSysno)
        {
            List<PermissionInfo> permissionList = (from a in _sysUserRoleRepository.Entities
                                                   join b in _rolePermissionRepository.Entities
                                                   on a.RoleID.Value equals b.RoleID.Value
                                                   join c in _permissionRepository.Entities
                                                   on b.PermissionID.Value equals c.SysNo
                                                   where a.UserID.Value == userSysno 
                                                   orderby c.Sort ascending
                                                   select c).ToList();

            return permissionList;
        }
    }
}
