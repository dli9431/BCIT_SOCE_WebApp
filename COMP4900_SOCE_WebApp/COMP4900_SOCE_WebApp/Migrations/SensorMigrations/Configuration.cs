namespace COMP4900_SOCE_WebApp.Migrations.SensorMigrations
{
    using SensorDataModel.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SensorDataModel.Models.SensorContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
            MigrationsDirectory = @"Migrations\SensorMigrations";
        }

        protected override void Seed(SensorDataModel.Models.SensorContext context)
        {

            context.Projects.AddOrUpdate(
                  p => p.ProjectId,
                  new Project { Name = "Sensor Project 1" },
                  new Project { Name = "Sensor Project 2" },
                  new Project { Name = "Sensor Project 3" },
                  new Project { Name = "Sensor Project 4" },
                  new Project { Name = "Sensor Project 5" },
                  new Project { Name = "Sensor Project 6" },
                  new Project { Name = "Sensor Project 7" }
                );
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
