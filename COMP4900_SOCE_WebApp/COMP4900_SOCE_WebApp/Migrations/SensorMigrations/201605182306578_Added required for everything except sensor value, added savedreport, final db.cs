namespace COMP4900_SOCE_WebApp.Migrations.SensorMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedrequiredforeverythingexceptsensorvalueaddedsavedreportfinaldb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SavedReports",
                c => new
                    {
                        SavedReportId = c.Int(nullable: false, identity: true),
                        ReportName = c.String(unicode: false),
                        ProjectName = c.String(unicode: false),
                        CustomGroupName = c.String(unicode: false),
                        BeginDate = c.DateTime(nullable: false, precision: 0),
                        EndDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.SavedReportId)                ;
            
            AddColumn("CustomGroups", "UserName", c => c.String(unicode: false));
            AddColumn("Projects", "ProjectName", c => c.String(nullable: false, unicode: false));
            AddColumn("Projects", "ProjectDescription", c => c.String(nullable: false, unicode: false));
            AlterColumn("Projects", "UserName", c => c.String(nullable: false, unicode: false));
            AlterColumn("SensorProjects", "SensorProjectName", c => c.String(nullable: false, unicode: false));
            AlterColumn("SensorProjects", "SensorProjectType", c => c.String(nullable: false, unicode: false));
            AlterColumn("SensorProjects", "SensorName", c => c.String(nullable: false, unicode: false));
            AlterColumn("Sensors", "SensorName", c => c.String(nullable: false, unicode: false));
            AlterColumn("Sensors", "ProjectName", c => c.String(nullable: false, unicode: false));
            DropColumn("CustomGroups", "StudentId");
            DropColumn("Projects", "Name");
            DropColumn("Projects", "Description");
        }
        
        public override void Down()
        {
            AddColumn("Projects", "Description", c => c.String(unicode: false));
            AddColumn("Projects", "Name", c => c.String(unicode: false));
            AddColumn("CustomGroups", "StudentId", c => c.String(unicode: false));
            AlterColumn("Sensors", "ProjectName", c => c.String(unicode: false));
            AlterColumn("Sensors", "SensorName", c => c.String(unicode: false));
            AlterColumn("SensorProjects", "SensorName", c => c.String(unicode: false));
            AlterColumn("SensorProjects", "SensorProjectType", c => c.String(unicode: false));
            AlterColumn("SensorProjects", "SensorProjectName", c => c.String(unicode: false));
            AlterColumn("Projects", "UserName", c => c.String(unicode: false));
            DropColumn("Projects", "ProjectDescription");
            DropColumn("Projects", "ProjectName");
            DropColumn("CustomGroups", "UserName");
            DropTable("SavedReports");
        }
    }
}
