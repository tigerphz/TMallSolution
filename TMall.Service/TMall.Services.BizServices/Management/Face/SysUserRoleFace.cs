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
    public class SysUserRoleFace
    {
        private IRoleRepository _roleRepository;
        private ISysUserRoleRepository _sysUserRoleRepository;

        private readonly IUnitOfWork _unitOfWork;

        public SysUserRoleFace(IRoleRepository roleRepository,
                              ISysUserRoleRepository sysUserRoleRepository)
        {
            _roleRepository = roleRepository;
            _sysUserRoleRepository = sysUserRoleRepository;

            _unitOfWork = _roleRepository.UnitOfWork;
        }

        /// <summary>
        /// 获取用户已经绑定的角色列表
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<RoleInfo> GetBindedRole(Guid userSysno)
        {

            List<RoleInfo> bindedRoleList = (from a in _sysUserRoleRepository.Entities.Where(x => x.UserID.Value == userSysno)
                                             join b in _roleRepository.Entities
                                             on a.RoleID equals b.SysNo
                                             where a.StatusDB == (int)CommonStatus.Valid &&
                                             b.StatusDB == (int)CommonStatus.Valid
                                             orderby b.SysNo
                                             select b).ToList();

            return bindedRoleList;
        }
    }
}
