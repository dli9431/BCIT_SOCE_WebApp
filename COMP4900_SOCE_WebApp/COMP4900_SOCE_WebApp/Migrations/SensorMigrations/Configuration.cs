namespace COMP4900_SOCE_WebApp.Migrations.SensorMigrations
{
    using SensorDataModel.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SensorDataModel.Models.SensorContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
            CodeGenerator = new MySql.Data.Entity.MySqlMigrationCodeGenerator();
            MigrationsDirectory = @"Migrations\SensorMigrations";
        }

        protected override void Seed(SensorDataModel.Models.SensorContext context)
        {

        }
    }
}
