namespace COMP4900_SOCE_WebApp.Migrations.SensorMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sensors", "SensorProject_SensorProjectId", "dbo.SensorProjects");
            DropIndex("dbo.Sensors", new[] { "SensorProject_SensorProjectId" });
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
                "dbo.SensorDateTimes",
                c => new
                    {
                        DateTimeId = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.DateTimeId);
            
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
            
            AddColumn("dbo.SensorProjects", "SensorProjectType", c => c.String(unicode: false));
            AddColumn("dbo.SensorProjects", "SensorName", c => c.String(unicode: false));
            AlterColumn("dbo.Projects", "Name", c => c.String(unicode: false));
            AlterColumn("dbo.Projects", "Description", c => c.String(unicode: false));
            AlterColumn("dbo.SensorProjects", "SensorProjectName", c => c.String(unicode: false));
            DropColumn("dbo.SensorProjects", "SensorType");
            DropColumn("dbo.SensorProjects", "SensorAcronym");
            DropTable("dbo.Sensors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sensors",
                c => new
                    {
                        SensorId = c.Int(nullable: false, identity: true),
                        SensorName = c.String(nullable: false, unicode: false),
                        SensorValue = c.Double(nullable: false),
                        DateTime = c.DateTime(nullable: false, precision: 0),
                        SensorProject_SensorProjectId = c.Int(),
                    })
                .PrimaryKey(t => t.SensorId);
            
            AddColumn("dbo.SensorProjects", "SensorAcronym", c => c.String(nullable: false, unicode: false));
            AddColumn("dbo.SensorProjects", "SensorType", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.SensorProjects", "SensorProjectName", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Projects", "Description", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Projects", "Name", c => c.String(nullable: false, unicode: false));
            DropColumn("dbo.SensorProjects", "SensorName");
            DropColumn("dbo.SensorProjects", "SensorProjectType");
            DropTable("dbo.th_ps");
            DropTable("dbo.th_int");
            DropTable("dbo.th_ext");
            DropTable("dbo.SensorDateTimes");
            DropTable("dbo.mh_south");
            DropTable("dbo.mh_north");
            DropTable("dbo.hpws_rp");
            DropTable("dbo.hpws");
            DropTable("dbo.gvs_south");
            DropTable("dbo.gvs_north");
            CreateIndex("dbo.Sensors", "SensorProject_SensorProjectId");
            AddForeignKey("dbo.Sensors", "SensorProject_SensorProjectId", "dbo.SensorProjects", "SensorProjectId");
        }
    }
}
