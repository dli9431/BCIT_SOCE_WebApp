namespace COMP4900_SOCE_WebApp.Migrations.AccountMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedinactiveuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("AspNetUsers", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("AspNetUsers", "IsActive");
        }
    }
}
