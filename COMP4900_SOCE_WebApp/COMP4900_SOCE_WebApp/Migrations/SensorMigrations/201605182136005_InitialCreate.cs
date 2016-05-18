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
                        StudentId = c.String(unicode: false),
                        CustomGroupName = c.String(unicode: false),
                        ProjectName = c.String(unicode: false),
                        SensorName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.CustomGroupId)                ;
            
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
                "Sensors",
                c => new
                    {
                        sensorId = c.Int(nullable: false, identity: true),
                        SensorName = c.String(unicode: false),
                        SensorValue = c.Double(),
                        DateTime = c.DateTime(nullable: false, precision: 0),
                        ProjectName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.sensorId)                ;
            
        }
        
        public override void Down()
        {
            DropTable("Sensors");
            DropTable("SensorProjects");
            DropTable("Projects");
            DropTable("CustomGroups");
        }
    }
}
