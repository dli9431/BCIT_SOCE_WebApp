namespace COMP4900_SOCE_WebApp.Migrations.SensorMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedrequiredfromnametypesensornameinsensorprojectsanddeletedgroupprojectsmodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("SensorProjects", "SensorProjectName", c => c.String(unicode: false));
            AlterColumn("SensorProjects", "SensorProjectType", c => c.String(unicode: false));
            AlterColumn("SensorProjects", "SensorName", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("SensorProjects", "SensorName", c => c.String(nullable: false, unicode: false));
            AlterColumn("SensorProjects", "SensorProjectType", c => c.String(nullable: false, unicode: false));
            AlterColumn("SensorProjects", "SensorProjectName", c => c.String(nullable: false, unicode: false));
        }
    }
}
