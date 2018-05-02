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

namespace TMall.Services.BizServices.Management
{
    public class SysUserBizService :  ISysUserBizService
    {
        private ISysUserRepository _sysUserRepository;
        private ISysUserRoleRepository _sysUserRoleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SysUserBizService(ISysUserRepository sysUserRepository, 
                                 ISysUserRoleRepository sysUserRoleRepository)
        {
            _sysUserRepository = sysUserRepository;
            _sysUserRoleRepository = sysUserRoleRepository;
            _unitOfWork = _sysUserRepository.UnitOfWork;
        }

        #region QuerySerivce

        public bool IsExistSysUser(string userName, string password)
        {
            string passwordSalt = _sysUserRepository.GetPasswordSalt(userName);
            if (string.IsNullOrEmpty(passwordSalt))
                return false;

            return _sysUserRepository.IsExistEntity(x => x.UserName.Equals(userName)
                                                  && x.PasswordHash.Equals(password));
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public SysUserInfo GetSysUser(string userName, string password)
        {
            string passwordSalt = _sysUserRepository.GetPasswordSalt(userName);
            if (string.IsNullOrEmpty(passwordSalt))
                return null;

            string passwordHash = SecurityHelper.EncodePassword(password, passwordSalt);

            return _sysUserRepository.Entities.Where(x => x.UserName.Equals(userName) &&
                x.PasswordHash.Equals(passwordHash) &&
                x.StatusDB == (int)CommonStatus.Valid).FirstOrDefault();
        }

        public bool IsExistSysUser(string userName)
        {
            return _sysUserRepository.IsExistEntity(x => x.UserName.Equals(userName));
        }

        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public SearchPageInfo<SysUserInfo> GetSysUsers(SearchPageInfo<SysUserInfo> search)
        {
            Check.NotNull(search, "search");
            Check.NotNull(search.PageInfo, "search.PageInfo");
            if (string.IsNullOrEmpty(search.PageInfo.OrderField))
            {
                throw ExceptionHelper.ThrowBusinessException("分页获取数据必须指定排序列");
            }

            search = _sysUserRepository.GetPageEntities(search);

            return search;
        }

        #endregion

        #region ActionSerivce

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="sysUserInfo"></param>
        /// <returns></returns>
        public bool AddSysUser(SysUserInfo sysUserInfo)
        {
            sysUserInfo.SysNo = EntityGuid.NewComb();
            //获取混淆码
            string passwordSalt = SecurityHelper.GenerateSalt();
            //获取混淆码加密过的密码
            string passwordHash = SecurityHelper.EncodePassword(sysUserInfo.PasswordHash, passwordSalt);
            sysUserInfo.PasswordHash = passwordHash;
            sysUserInfo.PasswordSalt = passwordSalt;

            return _sysUserRepository.Insert(sysUserInfo) > 0;
        }

        /// <summary>
        /// 更新系统用户
        /// </summary>
        /// <param name="updateInfo"></param>
        /// <returns></returns>
        public bool UpdateSysUser(UpdateInfo updateInfo)
        {
            var propertyList = updateInfo.GetPropertyNames();
            if (propertyList == null || propertyList.Length == 0)
                return false;

            _sysUserRepository.UpdateEntity(updateInfo.GetData<SysUserInfo>(), propertyList);

            return _unitOfWork.Commit() > 0;
        }

        /// <summary>
        /// 获取系统用户
        /// </summary>
        /// <param name="sysno"></param>
        /// <returns></returns>
        public SysUserInfo GetSysUser(Guid sysno)
        {
            return _sysUserRepository.GetByKey(sysno);
        }

        /// <summary>
        /// 删除系统用户
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <returns></returns>
        public bool DeleteSysUser(SysUserInfo sysUserInfo)
        {
            return _sysUserRepository.Delete(sysUserInfo) > 0;
        }

        /// <summary>
        /// 用户绑定角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="rolePermissionList"></param>
        /// <returns></returns>
        public bool BindRole(Guid userID, List<SysUserRoleInfo> userRoleList)
        {
            return _sysUserRoleRepository.BindRole(userID, userRoleList);
        }

        #endregion
    }
}
