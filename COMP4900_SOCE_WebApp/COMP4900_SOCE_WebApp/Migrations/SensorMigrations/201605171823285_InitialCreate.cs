namespace COMP4900_SOCE_WebApp.Migrations.SensorMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "CustomGroups",
                c => new
                    {
                        CustomGroupId = c.Int(nullable: false, identity: true),
                        CustomGroupName = c.String(unicode: false),
                        SensorName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.CustomGroupId)                ;
            
            CreateTable(
                "gvs_north",
                c => new
                    {
                        gvs_northId = c.Int(nullable: false, identity: true),
                        SensorName = c.String(unicode: false),
                        SensorValue = c.Double(),
                        DateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.gvs_northId)                ;
            
            CreateTable(
                "gvs_south",
                c => new
                    {
                        gvs_southId = c.Int(nullable: false, identity: true),
                        SensorName = c.String(unicode: false),
                        SensorValue = c.Double(),
                        DateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.gvs_southId)                ;
            
            CreateTable(
                "hpws",
                c => new
                    {
                        hpwsId = c.Int(nullable: false, identity: true),
                        SensorName = c.String(unicode: false),
                        SensorValue = c.Double(),
                        DateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.hpwsId)                ;
            
            CreateTable(
                "hpws_rp",
                c => new
                    {
                        hpws_rpId = c.Int(nullable: false, identity: true),
                        SensorName = c.String(unicode: false),
                        SensorValue = c.Double(),
                        DateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.hpws_rpId)                ;
            
            CreateTable(
                "mh_north",
                c => new
                    {
                        mh_northId = c.Int(nullable: false, identity: true),
                        SensorName = c.String(unicode: false),
                        SensorValue = c.Double(),
                        DateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.mh_northId)                ;
            
            CreateTable(
                "mh_south",
                c => new
                    {
                        mh_southId = c.Int(nullable: false, identity: true),
                        SensorName = c.String(unicode: false),
                        SensorValue = c.Double(),
                        DateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.mh_southId)                ;
            
            CreateTable(
                "Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        UserName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ProjectId)                ;
            
            CreateTable(
                "SensorProjects",
                c => new
                    {
                        SensorProjectId = c.Int(nullable: false, identity: true),
                        SensorProjectName = c.String(unicode: false),
                        SensorProjectType = c.String(unicode: false),
                        SensorName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.SensorProjectId)                ;
            
            CreateTable(
                "th_ext",
                c => new
                    {
                        th_extId = c.Int(nullable: false, identity: true),
                        SensorName = c.String(unicode: false),
                        SensorValue = c.Double(),
                        DateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.th_extId)                ;
            
            CreateTable(
                "th_int",
                c => new
                    {
                        th_intId = c.Int(nullable: false, identity: true),
                        SensorName = c.String(unicode: false),
                        SensorValue = c.Double(),
                        DateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.th_intId)                ;
            
            CreateTable(
                "th_ps",
                c => new
                    {
                        th_psId = c.Int(nullable: false, identity: true),
                        SensorName = c.String(unicode: false),
                        SensorValue = c.Double(),
                        DateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.th_psId)                ;
            
            CreateTable(
                "UserGroups",
                c => new
                    {
                        UserIdIndex = c.Int(nullable: false, identity: true),
                        StudentId = c.String(unicode: false),
                        CustomGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserIdIndex)                ;
            
        }
        
        public override void Down()
        {
            DropTable("UserGroups");
            DropTable("th_ps");
            DropTable("th_int");
            DropTable("th_ext");
            DropTable("SensorProjects");
            DropTable("Projects");
            DropTable("mh_south");
            DropTable("mh_north");
            DropTable("hpws_rp");
            DropTable("hpws");
            DropTable("gvs_south");
            DropTable("gvs_north");
            DropTable("CustomGroups");
        }
    }
}
