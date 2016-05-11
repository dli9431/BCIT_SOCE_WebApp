namespace COMP4900_SOCE_WebApp.Migrations.SensorMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomGroups",
                c => new
                    {
                        CustomGroupId = c.Int(nullable: false, identity: true),
                        CustomGroupName = c.String(unicode: false),
                        SensorName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.CustomGroupId);
            
            CreateTable(
                "dbo.gvs_north",
                c => new
                    {
                        SensorProjectName = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        SensorName = c.String(unicode: false),
                        SensorValue = c.Double(nullable: false),
                        SensorDateTimeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SensorProjectName);
            
            CreateTable(
                "dbo.gvs_south",
                c => new
                    {
                        SensorProjectName = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        SensorName = c.String(unicode: false),
                        SensorValue = c.Double(nullable: false),
                        SensorDateTimeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SensorProjectName);
            
            CreateTable(
                "dbo.hpws",
                c => new
                    {
                        hpwsId = c.Int(nullable: false, identity: true),
                        SensorName = c.String(unicode: false),
                        SensorValue = c.Double(nullable: false),
                        DateTimeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.hpwsId);
            
            CreateTable(
                "dbo.hpws_rp",
                c => new
                    {
                        SensorProjectName = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        SensorName = c.String(unicode: false),
                        SensorValue = c.Double(nullable: false),
                        SensorDateTimeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SensorProjectName);
            
            CreateTable(
                "dbo.mh_north",
                c => new
                    {
                        SensorProjectName = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        SensorName = c.String(unicode: false),
                        SensorValue = c.Double(nullable: false),
                        SensorDateTimeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SensorProjectName);
            
            CreateTable(
                "dbo.mh_south",
                c => new
                    {
                        SensorProjectName = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        SensorName = c.String(unicode: false),
                        SensorValue = c.Double(nullable: false),
                        SensorDateTimeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SensorProjectName);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            CreateTable(
                "dbo.SensorDateTimes",
                c => new
                    {
                        DateTimeId = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.DateTimeId);
            
            CreateTable(
                "dbo.SensorProjects",
                c => new
                    {
                        SensorProjectId = c.Int(nullable: false, identity: true),
                        SensorProjectName = c.String(unicode: false),
                        SensorProjectType = c.String(unicode: false),
                        SensorName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.SensorProjectId);
            
            CreateTable(
                "dbo.th_ext",
                c => new
                    {
                        SensorProjectName = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        SensorName = c.String(unicode: false),
                        SensorValue = c.Double(nullable: false),
                        SensorDateTimeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SensorProjectName);
            
            CreateTable(
                "dbo.th_int",
                c => new
                    {
                        SensorProjectName = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        SensorName = c.String(unicode: false),
                        SensorValue = c.Double(nullable: false),
                        SensorDateTimeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SensorProjectName);
            
            CreateTable(
                "dbo.th_ps",
                c => new
                    {
                        SensorProjectName = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        SensorName = c.String(unicode: false),
                        SensorValue = c.Double(nullable: false),
                        SensorDateTimeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SensorProjectName);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        UserIdIndex = c.Int(nullable: false, identity: true),
                        StudentId = c.String(unicode: false),
                        CustomGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserIdIndex);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserGroups");
            DropTable("dbo.th_ps");
            DropTable("dbo.th_int");
            DropTable("dbo.th_ext");
            DropTable("dbo.SensorProjects");
            DropTable("dbo.SensorDateTimes");
            DropTable("dbo.Projects");
            DropTable("dbo.mh_south");
            DropTable("dbo.mh_north");
            DropTable("dbo.hpws_rp");
            DropTable("dbo.hpws");
            DropTable("dbo.gvs_south");
            DropTable("dbo.gvs_north");
            DropTable("dbo.CustomGroups");
        }
    }
}
