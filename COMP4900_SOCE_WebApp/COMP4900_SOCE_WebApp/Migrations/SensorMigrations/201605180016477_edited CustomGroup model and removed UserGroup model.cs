namespace COMP4900_SOCE_WebApp.Migrations.SensorMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedCustomGroupmodelandremovedUserGroupmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("CustomGroups", "StudentId", c => c.String(unicode: false));
            AddColumn("CustomGroups", "ProjectName", c => c.String(unicode: false));
            DropTable("UserGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "UserGroups",
                c => new
                    {
                        UserIdIndex = c.Int(nullable: false, identity: true),
                        StudentId = c.String(unicode: false),
                        CustomGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserIdIndex)                ;
            
            DropColumn("CustomGroups", "ProjectName");
            DropColumn("CustomGroups", "StudentId");
        }
    }
}
