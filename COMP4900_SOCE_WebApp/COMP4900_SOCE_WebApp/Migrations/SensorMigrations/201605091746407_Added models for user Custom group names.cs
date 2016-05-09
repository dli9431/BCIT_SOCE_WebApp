namespace COMP4900_SOCE_WebApp.Migrations.SensorMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedmodelsforuserCustomgroupnames : DbMigration
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
                "dbo.Users",
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
            DropTable("dbo.Users");
            DropTable("dbo.CustomGroups");
        }
    }
}
