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
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<CustomGroup> CustomGroups { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //}
    }
}

