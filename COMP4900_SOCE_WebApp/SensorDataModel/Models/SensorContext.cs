using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataModel.Models
{
    public class SensorContext : DbContext
    {
        public SensorContext() : base("MySQLConnection") { }
        public DbSet<Project> Projects { get; set; }

        public DbSet<SensorProject> SensorProjects { get; set; }
        public DbSet<SensorDateTime> SensorDateTimes { get; set; }
        public DbSet<hpws> hpws { get; set; }
        public DbSet<hpws_rp> hpws_rp { get; set; }
        public DbSet<mh_north> mh_north { get; set; }
        public DbSet<mh_south> mh_south { get; set; }
        public DbSet<th_int> th_int { get; set; }
        public DbSet<th_ext> th_ext { get; set; }
        public DbSet<th_ps> th_ps { get; set; }
        public DbSet<gvs_south> gvs_south { get; set; }
        public DbSet<gvs_north> gvs_north { get; set; }
        public DbSet<CustomGroup> CustomGroups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //}
    }
}

