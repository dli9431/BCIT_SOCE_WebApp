namespace COMP4900_SOCE_WebApp.Migrations.SensorMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class SensorConfiguration : DbMigrationsConfiguration<SensorDataModel.Models.SensorContext>
    {
        public SensorConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
            CodeGenerator = new MySql.Data.Entity.MySqlMigrationCodeGenerator();
            MigrationsDirectory = @"Migrations\SensorMigrations";
        }

        protected override void Seed(SensorDataModel.Models.SensorContext context)
        {
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
