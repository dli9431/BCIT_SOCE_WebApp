namespace COMP4900_SOCE_WebApp.Migrations.SensorMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteddatetimetableandaddeddatetimevalueto9projecttables : DbMigration
    {
        public override void Up()
        {
            AddColumn("gvs_north", "DateTime", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("gvs_south", "DateTime", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("hpws", "DateTime", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("hpws_rp", "DateTime", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("mh_north", "DateTime", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("mh_south", "DateTime", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("th_ext", "DateTime", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("th_int", "DateTime", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("th_ps", "DateTime", c => c.DateTime(nullable: false, precision: 0));
            DropColumn("gvs_north", "SensorDateTimeId");
            DropColumn("gvs_south", "SensorDateTimeId");
            DropColumn("hpws", "DateTimeId");
            DropColumn("hpws_rp", "SensorDateTimeId");
            DropColumn("mh_north", "SensorDateTimeId");
            DropColumn("mh_south", "SensorDateTimeId");
            DropColumn("th_ext", "SensorDateTimeId");
            DropColumn("th_int", "SensorDateTimeId");
            DropColumn("th_ps", "SensorDateTimeId");
            DropTable("SensorDateTimes");
        }
        
        public override void Down()
        {
            CreateTable(
                "SensorDateTimes",
                c => new
                    {
                        DateTimeId = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.DateTimeId)                ;
            
            AddColumn("th_ps", "SensorDateTimeId", c => c.Int(nullable: false));
            AddColumn("th_int", "SensorDateTimeId", c => c.Int(nullable: false));
            AddColumn("th_ext", "SensorDateTimeId", c => c.Int(nullable: false));
            AddColumn("mh_south", "SensorDateTimeId", c => c.Int(nullable: false));
            AddColumn("mh_north", "SensorDateTimeId", c => c.Int(nullable: false));
            AddColumn("hpws_rp", "SensorDateTimeId", c => c.Int(nullable: false));
            AddColumn("hpws", "DateTimeId", c => c.Int(nullable: false));
            AddColumn("gvs_south", "SensorDateTimeId", c => c.Int(nullable: false));
            AddColumn("gvs_north", "SensorDateTimeId", c => c.Int(nullable: false));
            DropColumn("th_ps", "DateTime");
            DropColumn("th_int", "DateTime");
            DropColumn("th_ext", "DateTime");
            DropColumn("mh_south", "DateTime");
            DropColumn("mh_north", "DateTime");
            DropColumn("hpws_rp", "DateTime");
            DropColumn("hpws", "DateTime");
            DropColumn("gvs_south", "DateTime");
            DropColumn("gvs_north", "DateTime");
        }
    }
}
