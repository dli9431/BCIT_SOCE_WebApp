namespace COMP4900_SOCE_WebApp.Migrations.SensorMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedusernameinprojectsmodeltobenullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Projects", "UserName", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("Projects", "UserName", c => c.String(nullable: false, unicode: false));
        }
    }
}
