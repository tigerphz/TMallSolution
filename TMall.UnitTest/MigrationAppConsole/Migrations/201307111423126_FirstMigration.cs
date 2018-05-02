namespace MigrationAppConsole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbPermission",
                c => new
                    {
                        SysNo = c.Guid(nullable: false),
                        PermissionAction = c.String(maxLength: 30, unicode: false, storeType: "nvarchar"),
                        PermissionName = c.String(maxLength: 30, unicode: false, storeType: "nvarchar"),
                        Sort = c.Int(nullable: false),
                        StatusDB = c.Int(nullable: false),
                        Script = c.String(maxLength: 100, unicode: false, storeType: "nvarchar"),
                        Icon = c.String(maxLength: 100, unicode: false, storeType: "nvarchar"),
                        PermissionController = c.String(maxLength: 30, unicode: false, storeType: "nvarchar"),
                        Description = c.String(maxLength: 200, unicode: false, storeType: "nvarchar"),
                        IsButton = c.Boolean(nullable: false),
                        ParentID = c.Guid(),
                        CreateUserSysNo = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyUserSysNo = c.Guid(),
                        ModifyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SysNo);
            
            CreateTable(
                "dbo.tbModulePermission",
                c => new
                    {
                        SysNo = c.Guid(nullable: false),
                        ModuleID = c.Guid(),
                        PermissionID = c.Guid(),
                        StatusDB = c.Int(nullable: false),
                        CreateUserSysNo = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyUserSysNo = c.Guid(),
                        ModifyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SysNo)
                .ForeignKey("dbo.tbModule", t => t.ModuleID)
                .ForeignKey("dbo.tbPermission", t => t.PermissionID)
                .Index(t => t.ModuleID)
                .Index(t => t.PermissionID);
            
            CreateTable(
                "dbo.tbModule",
                c => new
                    {
                        SysNo = c.Guid(nullable: false),
                        ModuleName = c.String(maxLength: 30, unicode: false, storeType: "nvarchar"),
                        ModuleLinkUrl = c.String(maxLength: 100, unicode: false, storeType: "nvarchar"),
                        ModuleIcon = c.String(maxLength: 100, unicode: false, storeType: "nvarchar"),
                        ParentNo = c.Guid(),
                        Sort = c.Int(nullable: false),
                        StatusDB = c.Int(nullable: false),
                        IsLeaf = c.Boolean(nullable: false),
                        IsMenu = c.Boolean(nullable: false),
                        IsSelected = c.Boolean(nullable: false),
                        ModuleController = c.Boolean(nullable: false),
                        NavId = c.String(maxLength: 30, unicode: false, storeType: "nvarchar"),
                        CreateUserSysNo = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyUserSysNo = c.Guid(),
                        ModifyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SysNo);
            
            CreateTable(
                "dbo.tbRolePermission",
                c => new
                    {
                        SysNo = c.Guid(nullable: false),
                        RoleID = c.Guid(),
                        ModulePermissionID = c.Guid(),
                        StatusDB = c.Int(nullable: false),
                        CreateUserSysNo = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyUserSysNo = c.Guid(),
                        ModifyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SysNo)
                .ForeignKey("dbo.tbRole", t => t.RoleID)
                .ForeignKey("dbo.tbModulePermission", t => t.ModulePermissionID)
                .Index(t => t.RoleID)
                .Index(t => t.ModulePermissionID);
            
            CreateTable(
                "dbo.tbRole",
                c => new
                    {
                        SysNo = c.Guid(nullable: false),
                        RoleNo = c.String(maxLength: 30, unicode: false, storeType: "nvarchar"),
                        RoleName = c.String(maxLength: 30, unicode: false, storeType: "nvarchar"),
                        Sort = c.Int(nullable: false),
                        StatusDB = c.Int(nullable: false),
                        Description = c.String(maxLength: 200, unicode: false, storeType: "nvarchar"),
                        CreateUserSysNo = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyUserSysNo = c.Guid(),
                        ModifyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SysNo);
            
            CreateTable(
                "dbo.tbUserRoleInfo",
                c => new
                    {
                        SysNo = c.Guid(nullable: false),
                        UserID = c.Guid(),
                        RoleID = c.Guid(),
                        StatusDB = c.Int(nullable: false),
                        CreateUserSysNo = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyUserSysNo = c.Guid(),
                        ModifyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SysNo)
                .ForeignKey("dbo.tbUser", t => t.UserID)
                .ForeignKey("dbo.tbRole", t => t.RoleID)
                .Index(t => t.UserID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.tbUser",
                c => new
                    {
                        SysNo = c.Guid(nullable: false),
                        ManagerName = c.String(maxLength: 30, unicode: false, storeType: "nvarchar"),
                        PasswordHash = c.String(nullable: false, maxLength: 50, unicode: false, storeType: "char"),
                        PasswordSalt = c.String(nullable: false, maxLength: 32, unicode: false, storeType: "char"),
                        RoleID = c.Guid(),
                        DeptID = c.Guid(),
                        Phone = c.String(maxLength: 30, unicode: false, storeType: "nvarchar"),
                        Fax = c.String(maxLength: 30, unicode: false, storeType: "nvarchar"),
                        Email = c.String(maxLength: 30, unicode: false, storeType: "nvarchar"),
                        QQ = c.String(maxLength: 30, unicode: false, storeType: "nvarchar"),
                        NickName = c.String(maxLength: 30, unicode: false, storeType: "nvarchar"),
                        Address = c.String(maxLength: 30, unicode: false, storeType: "nvarchar"),
                        RealName = c.String(maxLength: 30, unicode: false, storeType: "nvarchar"),
                        Gender = c.Int(nullable: false),
                        StatusDB = c.Int(nullable: false),
                        CreateUserSysNo = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyUserSysNo = c.Guid(),
                        ModifyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SysNo)
                .ForeignKey("dbo.tbDepartment", t => t.DeptID)
                .Index(t => t.DeptID);
            
            CreateTable(
                "dbo.tbDepartment",
                c => new
                    {
                        SysNo = c.Guid(nullable: false),
                        DeptName = c.String(maxLength: 30, unicode: false, storeType: "nvarchar"),
                        DeptDescription = c.String(maxLength: 200, unicode: false, storeType: "nvarchar"),
                        ParentID = c.Guid(),
                        StatusDB = c.Int(nullable: false),
                        CreateUserSysNo = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyUserSysNo = c.Guid(),
                        ModifyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SysNo);
            
            CreateTable(
                "dbo.tbEventLog",
                c => new
                    {
                        SysNo = c.Guid(nullable: false),
                        LogLevelName = c.String(maxLength: 30, unicode: false, storeType: "nvarchar"),
                        Source = c.String(maxLength: 100, unicode: false, storeType: "nvarchar"),
                        EventType = c.String(maxLength: 100, unicode: false, storeType: "nvarchar"),
                        EventMessage = c.String(maxLength: 100, unicode: false, storeType: "nvarchar"),
                        EventDetail = c.String(maxLength: 300, unicode: false, storeType: "nvarchar"),
                        EventStackTrace = c.String(maxLength: 300, unicode: false, storeType: "nvarchar"),
                        InnerException = c.String(maxLength: 300, unicode: false, storeType: "nvarchar"),
                        IpAddress = c.String(maxLength: 30, unicode: false, storeType: "nvarchar"),
                        HostName = c.String(maxLength: 30, unicode: false, storeType: "nvarchar"),
                        PageUrl = c.String(maxLength: 100, unicode: false, storeType: "nvarchar"),
                        ReferrerUrl = c.String(maxLength: 100, unicode: false, storeType: "nvarchar"),
                        CreateUserSysNo = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyUserSysNo = c.Guid(),
                        ModifyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SysNo);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.tbUser", new[] { "DeptID" });
            DropIndex("dbo.tbUserRoleInfo", new[] { "RoleID" });
            DropIndex("dbo.tbUserRoleInfo", new[] { "UserID" });
            DropIndex("dbo.tbRolePermission", new[] { "ModulePermissionID" });
            DropIndex("dbo.tbRolePermission", new[] { "RoleID" });
            DropIndex("dbo.tbModulePermission", new[] { "PermissionID" });
            DropIndex("dbo.tbModulePermission", new[] { "ModuleID" });
            DropForeignKey("dbo.tbUser", "DeptID", "dbo.tbDepartment");
            DropForeignKey("dbo.tbUserRoleInfo", "RoleID", "dbo.tbRole");
            DropForeignKey("dbo.tbUserRoleInfo", "UserID", "dbo.tbUser");
            DropForeignKey("dbo.tbRolePermission", "ModulePermissionID", "dbo.tbModulePermission");
            DropForeignKey("dbo.tbRolePermission", "RoleID", "dbo.tbRole");
            DropForeignKey("dbo.tbModulePermission", "PermissionID", "dbo.tbPermission");
            DropForeignKey("dbo.tbModulePermission", "ModuleID", "dbo.tbModule");
            DropTable("dbo.tbEventLog");
            DropTable("dbo.tbDepartment");
            DropTable("dbo.tbUser");
            DropTable("dbo.tbUserRoleInfo");
            DropTable("dbo.tbRole");
            DropTable("dbo.tbRolePermission");
            DropTable("dbo.tbModule");
            DropTable("dbo.tbModulePermission");
            DropTable("dbo.tbPermission");
        }
    }
}
