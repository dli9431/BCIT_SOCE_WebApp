namespace COMP4900_SOCE_WebApp.Migrations.AccountMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editeduseridentity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "PasswordHash", c => c.String(unicode: false));
            AlterColumn("dbo.AspNetUsers", "SecurityStamp", c => c.String(unicode: false));
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String(unicode: false));
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime(precision: 0));
            AlterColumn("dbo.AspNetUserClaims", "ClaimType", c => c.String(unicode: false));
            AlterColumn("dbo.AspNetUserClaims", "ClaimValue", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUserClaims", "ClaimValue", c => c.String());
            AlterColumn("dbo.AspNetUserClaims", "ClaimType", c => c.String());
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            AlterColumn("dbo.AspNetUsers", "SecurityStamp", c => c.String());
            AlterColumn("dbo.AspNetUsers", "PasswordHash", c => c.String());
        }
    }
}
