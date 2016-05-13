namespace COMP4900_SOCE_WebApp.Migrations.SensorMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("gvs_north");
            DropPrimaryKey("gvs_south");
            DropPrimaryKey("hpws_rp");
            DropPrimaryKey("mh_north");
            DropPrimaryKey("mh_south");
            DropPrimaryKey("th_ext");
            DropPrimaryKey("th_int");
            DropPrimaryKey("th_ps");
            AddColumn("gvs_north", "gvs_northId", c => c.Int(nullable: false, identity: true));
            AddColumn("gvs_south", "gvs_southId", c => c.Int(nullable: false, identity: true));
            AddColumn("hpws_rp", "hpws_rpId", c => c.Int(nullable: false, identity: true));
            AddColumn("mh_north", "mh_northId", c => c.Int(nullable: false, identity: true));
            AddColumn("mh_south", "mh_southId", c => c.Int(nullable: false, identity: true));
            AddColumn("Projects", "UserName", c => c.String(unicode: false));
            AddColumn("th_ext", "th_extId", c => c.Int(nullable: false, identity: true));
            AddColumn("th_int", "th_intId", c => c.Int(nullable: false, identity: true));
            AddColumn("th_ps", "th_psId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("gvs_north", "gvs_northId");
            AddPrimaryKey("gvs_south", "gvs_southId");
            AddPrimaryKey("hpws_rp", "hpws_rpId");
            AddPrimaryKey("mh_north", "mh_northId");
            AddPrimaryKey("mh_south", "mh_southId");
            AddPrimaryKey("th_ext", "th_extId");
            AddPrimaryKey("th_int", "th_intId");
            AddPrimaryKey("th_ps", "th_psId");
            DropColumn("gvs_north", "SensorProjectName");
            DropColumn("gvs_south", "SensorProjectName");
            DropColumn("hpws_rp", "SensorProjectName");
            DropColumn("mh_north", "SensorProjectName");
            DropColumn("mh_south", "SensorProjectName");
            DropColumn("th_ext", "SensorProjectName");
            DropColumn("th_int", "SensorProjectName");
            DropColumn("th_ps", "SensorProjectName");
        }
        
        public override void Down()
        {
            AddColumn("th_ps", "SensorProjectName", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            AddColumn("th_int", "SensorProjectName", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            AddColumn("th_ext", "SensorProjectName", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            AddColumn("mh_south", "SensorProjectName", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            AddColumn("mh_north", "SensorProjectName", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            AddColumn("hpws_rp", "SensorProjectName", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            AddColumn("gvs_south", "SensorProjectName", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            AddColumn("gvs_north", "SensorProjectName", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            DropPrimaryKey("th_ps");
            DropPrimaryKey("th_int");
            DropPrimaryKey("th_ext");
            DropPrimaryKey("mh_south");
            DropPrimaryKey("mh_north");
            DropPrimaryKey("hpws_rp");
            DropPrimaryKey("gvs_south");
            DropPrimaryKey("gvs_north");
            DropColumn("th_ps", "th_psId");
            DropColumn("th_int", "th_intId");
            DropColumn("th_ext", "th_extId");
            DropColumn("Projects", "UserName");
            DropColumn("mh_south", "mh_southId");
            DropColumn("mh_north", "mh_northId");
            DropColumn("hpws_rp", "hpws_rpId");
            DropColumn("gvs_south", "gvs_southId");
            DropColumn("gvs_north", "gvs_northId");
            AddPrimaryKey("th_ps", "SensorProjectName");
            AddPrimaryKey("th_int", "SensorProjectName");
            AddPrimaryKey("th_ext", "SensorProjectName");
            AddPrimaryKey("mh_south", "SensorProjectName");
            AddPrimaryKey("mh_north", "SensorProjectName");
            AddPrimaryKey("hpws_rp", "SensorProjectName");
            AddPrimaryKey("gvs_south", "SensorProjectName");
            AddPrimaryKey("gvs_north", "SensorProjectName");
        }
    }
}
