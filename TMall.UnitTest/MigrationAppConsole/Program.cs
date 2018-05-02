using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Management;
using System.Data.Entity;
using MigrationAppConsole.Initializes;
using TMall.DomainModule.Enums;
using TMall.Infrastructure.Utility;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;

namespace MigrationAppConsole
{
    class Program
    {
        private static List<Guid> permissionSysnoList = new List<Guid>();

        static void Main(string[] args)
        {
            //Database.SetInitializer(new DropCreateDBIfModelChanges());

            Console.WriteLine("----------开始初始化基础数据数据-----------");
            Console.WriteLine();

            using (MigrationDbContext context = new MigrationDbContext())
            {
                ObjectContext objContext = ((IObjectContextAdapter)context).ObjectContext;
                if (objContext.DatabaseExists())
                {
                    Console.WriteLine("数据库已经存在存在,是否需要删除：y/n");
                    if (Console.ReadLine() != "y")
                        return;

                    objContext.DeleteDatabase();
                    objContext.CreateDatabase();
                }

                #region 初始化菜单

                Console.WriteLine("----------开始初始化菜单-----------");

                MenuInfo mainMenu = new MenuInfo()
                {
                    SysNo = EntityGuid.NewComb(),
                    IsLeaf = false,
                    MenuName = "系统设置",
                    Sort = 10,
                    Status = CommonStatus.Valid,
                    IsSelected = true
                };

                MenuInfo childMenu = new MenuInfo()
                {
                    SysNo = EntityGuid.NewComb(),
                    IsLeaf = false,
                    MenuName = "系统管理",
                    Sort = 10,
                    Status = CommonStatus.Valid,
                    ParentNo = mainMenu.SysNo
                };

                MenuInfo menuLeaf = new MenuInfo()
                {
                    SysNo = EntityGuid.NewComb(),
                    IsLeaf = true,
                    MenuName = "菜单管理",
                    Sort = 1,
                    Status = CommonStatus.Valid,
                    ParentNo = childMenu.SysNo,
                    MenuLinkUrl = "/Management/Menu/Index",
                    ModuleController = "Menu"
                };

                MenuInfo permissionLeaf = new MenuInfo()
                {
                    SysNo = EntityGuid.NewComb(),
                    IsLeaf = true,
                    MenuName = "权限管理",
                    Sort = 2,
                    Status = CommonStatus.Valid,
                    ParentNo = childMenu.SysNo,
                    MenuLinkUrl = "/Management/Permission/Index",
                    ModuleController = "Permission"
                };

                MenuInfo roleLeaf = new MenuInfo()
                {
                    SysNo = EntityGuid.NewComb(),
                    IsLeaf = true,
                    MenuName = "角色管理",
                    Sort = 3,
                    Status = CommonStatus.Valid,
                    ParentNo = childMenu.SysNo,
                    MenuLinkUrl = "/Management/Role/Index",
                    ModuleController = "Role"
                };
                MenuInfo userLeaf = new MenuInfo()
                {
                    SysNo = EntityGuid.NewComb(),
                    IsLeaf = true,
                    MenuName = "用户管理",
                    Sort = 4,
                    Status = CommonStatus.Valid,
                    ParentNo = childMenu.SysNo,
                    MenuLinkUrl = "/Management/Account/Index",
                    ModuleController = "Account"
                };

                context.Set<MenuInfo>().Add(mainMenu);
                context.Set<MenuInfo>().Add(childMenu);
                context.Set<MenuInfo>().Add(menuLeaf);
                context.Set<MenuInfo>().Add(permissionLeaf);
                context.Set<MenuInfo>().Add(roleLeaf);
                context.Set<MenuInfo>().Add(userLeaf);

                Console.WriteLine("----------结束初始化菜单-----------");

                #endregion

                #region 初始化权限

                #region 菜单管理权限

                Console.WriteLine("----------开始初始化菜单管理权限-----------");

                PermissionInfo menuIndex = new PermissionInfo()
                {
                    Description = "菜单管理首页",
                    IsButton = false,
                    PermissionAction = "Index",
                    PermissionController = "Menu",
                    PermissionName = "菜单管理首页",
                    Sort = 1,
                    Status = CommonStatus.Valid,
                    SysNo = EntityGuid.NewComb()
                };

                PermissionInfo menuAdd = new PermissionInfo()
                {
                    Description = "添加菜单",
                    IsButton = true,
                    PermissionAction = "Add",
                    PermissionController = "Menu",
                    PermissionName = "添加菜单",
                    Sort = 2,
                    Status = CommonStatus.Valid,
                    SysNo = EntityGuid.NewComb(),
                    ParentID = menuIndex.SysNo
                };

                PermissionInfo menuUpdate = new PermissionInfo()
                {
                    Description = "修改菜单",
                    IsButton = true,
                    PermissionAction = "Update",
                    PermissionController = "Menu",
                    PermissionName = "修改菜单",
                    Sort = 3,
                    Status = CommonStatus.Valid,
                    ParentID = menuIndex.SysNo,
                    SysNo = EntityGuid.NewComb()
                };

                PermissionInfo menuDelete = new PermissionInfo()
                {
                    Description = "删除菜单",
                    IsButton = true,
                    PermissionAction = "Delete",
                    PermissionController = "Menu",
                    PermissionName = "删除菜单",
                    Sort = 4,
                    Status = CommonStatus.Valid,
                    ParentID = menuIndex.SysNo,
                    SysNo = EntityGuid.NewComb()
                };

                permissionSysnoList.Add(menuIndex.SysNo);
                permissionSysnoList.Add(menuAdd.SysNo);
                permissionSysnoList.Add(menuUpdate.SysNo);
                permissionSysnoList.Add(menuDelete.SysNo);

                context.Set<PermissionInfo>().Add(menuIndex);
                context.Set<PermissionInfo>().Add(menuAdd);
                context.Set<PermissionInfo>().Add(menuUpdate);
                context.Set<PermissionInfo>().Add(menuDelete);

                Console.WriteLine("----------结束初始化菜单管理权限-----------");

                #endregion

                #region 权限管理权限

                Console.WriteLine("----------开始初始化权限管理权限-----------");

                PermissionInfo permissionIndex = new PermissionInfo()
                {
                    Description = "权限管理首页",
                    IsButton = false,
                    PermissionAction = "Index",
                    PermissionController = "Permission",
                    PermissionName = "权限管理首页",
                    Sort = 5,
                    Status = CommonStatus.Valid,
                    SysNo = EntityGuid.NewComb()
                };

                PermissionInfo permissionAdd = new PermissionInfo()
                {
                    Description = "添加权限",
                    IsButton = true,
                    PermissionAction = "Add",
                    PermissionController = "Permission",
                    PermissionName = "添加权限",
                    Sort = 6,
                    Status = CommonStatus.Valid,
                    ParentID = permissionIndex.SysNo,
                    SysNo = EntityGuid.NewComb()
                };

                PermissionInfo permissionUpdate = new PermissionInfo()
                {
                    Description = "修改权限",
                    IsButton = true,
                    PermissionAction = "Update",
                    PermissionController = "Permission",
                    PermissionName = "修改权限",
                    Sort = 7,
                    ParentID = permissionIndex.SysNo,
                    Status = CommonStatus.Valid,
                    SysNo = EntityGuid.NewComb()
                };

                PermissionInfo permissionDelete = new PermissionInfo()
                {
                    Description = "删除权限",
                    IsButton = true,
                    PermissionAction = "Delete",
                    PermissionController = "Permission",
                    PermissionName = "删除权限",
                    Sort = 8,
                    Status = CommonStatus.Valid,
                    ParentID = permissionIndex.SysNo,
                    SysNo = EntityGuid.NewComb()
                };

                permissionSysnoList.Add(permissionIndex.SysNo);
                permissionSysnoList.Add(permissionAdd.SysNo);
                permissionSysnoList.Add(permissionUpdate.SysNo);
                permissionSysnoList.Add(permissionDelete.SysNo);

                context.Set<PermissionInfo>().Add(permissionIndex);
                context.Set<PermissionInfo>().Add(permissionAdd);
                context.Set<PermissionInfo>().Add(permissionUpdate);
                context.Set<PermissionInfo>().Add(permissionDelete);

                Console.WriteLine("----------结束初始化权限管理权限-----------");

                #endregion

                #region 角色管理权限

                Console.WriteLine("----------结束初始化角色管理权限-----------");

                PermissionInfo roleIndex = new PermissionInfo()
                {
                    Description = "角色管理首页",
                    IsButton = false,
                    PermissionAction = "Index",
                    PermissionController = "Role",
                    PermissionName = "角色管理首页",
                    Sort = 9,
                    Status = CommonStatus.Valid,
                    SysNo = EntityGuid.NewComb()
                };

                PermissionInfo roleAdd = new PermissionInfo()
                {
                    Description = "添加角色",
                    IsButton = true,
                    PermissionAction = "Add",
                    PermissionController = "Role",
                    PermissionName = "添加角色",
                    Sort = 10,
                    Status = CommonStatus.Valid,
                    ParentID = roleIndex.SysNo,
                    SysNo = EntityGuid.NewComb()
                };

                PermissionInfo roleUpdate = new PermissionInfo()
                {
                    Description = "修改角色",
                    IsButton = true,
                    PermissionAction = "Update",
                    PermissionController = "Role",
                    PermissionName = "修改角色",
                    Sort = 11,
                    Status = CommonStatus.Valid,
                    ParentID = roleIndex.SysNo,
                    SysNo = EntityGuid.NewComb()
                };

                PermissionInfo roleDelete = new PermissionInfo()
                {
                    Description = "删除角色",
                    IsButton = true,
                    PermissionAction = "Delete",
                    PermissionController = "Role",
                    PermissionName = "删除角色",
                    Sort = 12,
                    Status = CommonStatus.Valid,
                    ParentID = roleIndex.SysNo,
                    SysNo = EntityGuid.NewComb()
                };

                PermissionInfo roleBindIndex = new PermissionInfo()
                {
                    Description = "角色列表绑定权限页面",
                    IsButton = true,
                    PermissionAction = "BindIndex",
                    PermissionController = "Role",
                    PermissionName = "角色列表绑定权限页面",
                    Sort = 13,
                    Status = CommonStatus.Valid,
                    ParentID = roleIndex.SysNo,
                    SysNo = EntityGuid.NewComb()
                };

                PermissionInfo roleBindPermission = new PermissionInfo()
                {
                    Description = "角色绑定权限设置",
                    IsButton = true,
                    PermissionAction = "BindPermission",
                    PermissionController = "Role",
                    PermissionName = "角色绑定权限设置",
                    Sort = 14,
                    Status = CommonStatus.Valid,
                    ParentID = roleIndex.SysNo,
                    SysNo = EntityGuid.NewComb()
                };

                permissionSysnoList.Add(roleIndex.SysNo);
                permissionSysnoList.Add(roleAdd.SysNo);
                permissionSysnoList.Add(roleUpdate.SysNo);
                permissionSysnoList.Add(roleDelete.SysNo);
                permissionSysnoList.Add(roleBindIndex.SysNo);
                permissionSysnoList.Add(roleBindPermission.SysNo);

                context.Set<PermissionInfo>().Add(roleIndex);
                context.Set<PermissionInfo>().Add(roleAdd);
                context.Set<PermissionInfo>().Add(roleUpdate);
                context.Set<PermissionInfo>().Add(roleDelete);
                context.Set<PermissionInfo>().Add(roleBindIndex);
                context.Set<PermissionInfo>().Add(roleBindPermission);

                Console.WriteLine("----------结算初始化角色管理权限-----------");

                #endregion

                #region 系统用户管理权限

                Console.WriteLine("----------开始初始化系统用户管理权限-----------");

                PermissionInfo accountIndex = new PermissionInfo()
                {
                    Description = "系统用户管理首页",
                    IsButton = false,
                    PermissionAction = "Index",
                    PermissionController = "Account",
                    PermissionName = "系统用户管理首页",
                    Sort = 15,
                    Status = CommonStatus.Valid,
                    SysNo = EntityGuid.NewComb()
                };

                PermissionInfo accountAdd = new PermissionInfo()
                {
                    Description = "添加系统用户",
                    IsButton = true,
                    PermissionAction = "Add",
                    PermissionController = "Account",
                    PermissionName = "添加系统用户",
                    Sort = 16,
                    ParentID = accountIndex.SysNo,
                    Status = CommonStatus.Valid,
                    SysNo = EntityGuid.NewComb()
                };

                PermissionInfo accountUpdate = new PermissionInfo()
                {
                    Description = "修改系统用户",
                    IsButton = true,
                    PermissionAction = "Update",
                    PermissionController = "Account",
                    PermissionName = "修改系统用户",
                    Sort = 17,
                    ParentID = accountIndex.SysNo,
                    Status = CommonStatus.Valid,
                    SysNo = EntityGuid.NewComb()
                };

                PermissionInfo accountDelete = new PermissionInfo()
                {
                    Description = "删除系统用户",
                    IsButton = true,
                    PermissionAction = "Delete",
                    PermissionController = "Account",
                    PermissionName = "删除系统用户",
                    Sort = 18,
                    ParentID = accountIndex.SysNo,
                    Status = CommonStatus.Valid,
                    SysNo = EntityGuid.NewComb()
                };

                PermissionInfo accountBindRole = new PermissionInfo()
                {
                    Description = "用户绑定角色",
                    IsButton = true,
                    PermissionAction = "BindRole",
                    PermissionController = "Account",
                    PermissionName = "用户绑定角色",
                    Sort = 18,
                    ParentID = accountIndex.SysNo,
                    Status = CommonStatus.Valid,
                    SysNo = EntityGuid.NewComb()
                };

                permissionSysnoList.Add(accountIndex.SysNo);
                permissionSysnoList.Add(accountAdd.SysNo);
                permissionSysnoList.Add(accountUpdate.SysNo);
                permissionSysnoList.Add(accountDelete.SysNo);
                permissionSysnoList.Add(accountBindRole.SysNo);

                context.Set<PermissionInfo>().Add(accountIndex);
                context.Set<PermissionInfo>().Add(accountAdd);
                context.Set<PermissionInfo>().Add(accountUpdate);
                context.Set<PermissionInfo>().Add(accountDelete);
                context.Set<PermissionInfo>().Add(accountBindRole);

                Console.WriteLine("----------结束初始化系统用户管理权限-----------");

                #endregion

                #endregion

                #region 初始化角色

                Console.WriteLine("----------开始初始化角色-----------");
                RoleInfo roleAdmin = new RoleInfo()
                {
                    SysNo = EntityGuid.NewComb(),
                    Description = "系统管理员admin",
                    RoleName = "系统管理员",
                    Sort = 1,
                    Status = CommonStatus.Valid,
                };

                context.Set<RoleInfo>().Add(roleAdmin);

                Console.WriteLine("----------结束初始化角色-----------");

                #endregion

                #region 初始化系统管理员

                Console.WriteLine("----------开始初始化系统管理员-----------");

                SysUserInfo userInfo = new SysUserInfo()
                {
                    SysNo = EntityGuid.NewComb(),
                    NickName = "系统管理员",
                    PasswordHash = "yhf+iz6cXSs1ecvZKcwjyxmVsNE+H1Hio46n1pBhjt0=",
                    PasswordSalt = "QAIbqWP7XreVILUQHJ3n8w==",
                    UserName = "admin",
                    RealName = "系统管理员",
                    Status = CommonStatus.Valid

                };

                context.Set<SysUserInfo>().Add(userInfo);

                Console.WriteLine("----------结束初始化系统管理员-----------");

                #endregion

                #region 初始化角色权限关系

                Console.WriteLine("----------开始初始化角色权限关系-----------");

                permissionSysnoList.ForEach(x =>
                {
                    context.Set<RolePermissionInfo>().Add(
                        new RolePermissionInfo()
                        {
                            SysNo = EntityGuid.NewComb(),
                            Status = CommonStatus.Valid,
                            RoleID = roleAdmin.SysNo,
                            PermissionID = x
                        });
                });

                Console.WriteLine("----------结束初始化角色权限关系-----------");

                #endregion

                #region 初始化用户角色关系

                Console.WriteLine("----------开始初始化用户角色关系-----------");

                SysUserRoleInfo userRole = new SysUserRoleInfo()
                {
                    SysNo = EntityGuid.NewComb(),
                    RoleID = roleAdmin.SysNo,
                    Status = CommonStatus.Valid,
                    UserID = userInfo.SysNo
                };

                context.Set<SysUserRoleInfo>().Add(userRole);

                Console.WriteLine("----------结束初始化用户角色关系-----------");

                #endregion

                context.SaveChanges();
                
                Console.WriteLine("**********初始化数据成功！***********");
                Console.Read();
            }
        }
    }
}
