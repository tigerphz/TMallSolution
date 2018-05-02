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
using TMall.Infrastructure.Utility;

namespace TMall.ManageSite.BizProcess.Management
{
    public class RoleBizProcess : IRoleBizProcess
    {
        private IRoleBizService _roleBizService;

        public RoleBizProcess(IRoleBizService roleBizService)
        {
            this._roleBizService = roleBizService;
        }

        #region QuerySerivce

        /// <summary>
        /// 分页获取角色类别
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public SearchPageInfo<RoleVM> GetRoles(SearchPageInfo<RoleVM> search)
        {
            SearchPageInfo<RoleInfo> whereSearch = search.ToModel();
            return _roleBizService.GetRoles(whereSearch).ToVM();
        }

        /// <summary>
        /// 获取角色绑定过的权限集合
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<PermissionVM> GetBindedPermission(Guid roleID)
        {
            RolePermissionFace face = EngineContext.Current.Resolve<RolePermissionFace>();
            return face.GetBindedPermission(roleID).ToVM();
        }

        /// <summary>
        /// 返回easyUI 的treeGrid控件需要的数据
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public string GetTreeGridJsonData(Guid roleID)
        {
            List<PermissionVM> bindedList = GetBindedPermission(roleID);

            IPermissionBizProcess perBP = EngineContext.Current.Resolve<IPermissionBizProcess>();
            List<PermissionVM> permissions = perBP.GetAllPermissions();

            List<PermissionTreeGridData> root = permissions.Where(x => x.ParentID.HasValue == false)
                                                        .Select(y =>
                                                        {
                                                            PermissionTreeGridData gd = toPermissionTreeGridData(y);
                                                            gd.ischecked = bindedList.Exists(e => e.SysNo == y.SysNo);
                                                            return gd;
                                                        }).ToList();

            List<PermissionTreeGridData> resultData = new List<PermissionTreeGridData>();

            int identifier = 0;
            root.ForEach(x =>
            {
                x.Identifier = (++identifier).ToString();
                x.ischecked = bindedList.Exists(y => y.SysNo == x.sysno);
                resultData.Add(x);

                List<PermissionTreeGridData> children = permissions.FindAll(y => y.ParentID.HasValue && y.ParentID.Value == x.sysno)
                                       .Select(z =>
                                       {
                                           PermissionTreeGridData gd = toPermissionTreeGridData(z);
                                           gd.ischecked = bindedList.Exists(e => e.SysNo == z.SysNo);
                                           gd.Identifier = (++identifier).ToString();
                                           gd._parentId = x.Identifier.ToString();
                                           return gd;
                                       }).ToList();

                resultData.AddRange(children);
            });

            return "{\"rows\":" + JsonHelper.Serialize<List<PermissionTreeGridData>>(resultData) + "}";
        }

        private PermissionTreeGridData toPermissionTreeGridData(PermissionVM y)
        {
            return new PermissionTreeGridData()
            {
                sysno = y.SysNo,
                action = y.PermissionAction,
                controller = y.PermissionController,
                desc = y.Description,
                name = y.PermissionName,
                status = y.Status.Value.GetDescription(),
                type = y.IsButton ? "按钮" : "页面url"
            };
        }

        /// <summary>
        /// 获取全部可以的角色
        /// </summary>
        /// <returns></returns>
        public List<RoleVM> GetRoles()
        {
            return _roleBizService.GetRoles().ToVM();
        }

        #endregion

        #region ActionSerivce

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="RoleInfo"></param>
        /// <returns></returns>
        public bool AddRole(RoleVM roleInfo)
        {
            return _roleBizService.AddRole(roleInfo.ToModel());
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public bool UpdateRole(UpdateInfo updateInfo)
        {
            return _roleBizService.UpdateRole(updateInfo);
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="sysno"></param>
        /// <returns></returns>
        public RoleVM GetRole(Guid sysno)
        {
            return _roleBizService.GetRole(sysno).ToVM();
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <returns></returns>
        public bool DeleteRole(Guid sysno)
        {
            RoleInfo roleInfo = new RoleInfo() { SysNo = sysno };
            return _roleBizService.DeleteRole(roleInfo);
        }

        /// <summary>
        /// 角色绑定权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="rolePermissionList"></param>
        /// <returns></returns>
        public bool BindPermission(Guid roleID, List<RolePermissionVM> rolePermissionList)
        {
            return _roleBizService.BindPermission(roleID, rolePermissionList.ToModel());
        }

        #endregion
    }
}
