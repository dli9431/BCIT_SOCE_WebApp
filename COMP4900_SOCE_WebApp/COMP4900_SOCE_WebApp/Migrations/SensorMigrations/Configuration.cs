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
            MigrationsDirectory = @"Migrations\SensorMigrations";
        }
        
        protected override void Seed(SensorDataModel.Models.SensorContext context)
        {
            context.SensorDateTimes.AddOrUpdate(
               p => p.DateTimeId,
               new SensorDateTime
               {
                    DateTime = DateTime.ParseExact("03/14/2016 13:15", "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture)
               },
               new SensorDateTime
               {
                    DateTime = DateTime.ParseExact("03/14/2016 13:20", "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture)
               },
               new SensorDateTime
               {
                    DateTime = DateTime.ParseExact("03/14/2016 13:25", "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture)
               }
           );


            context.hpws.AddOrUpdate(
                p => p.hpwsId,
                new hpws
                {
                    SensorName = "W1GsTcUI",
                    SensorValue = 1,
                    DateTimeId = 1
                },
                new hpws
                {
                    SensorName = "W1TpTcBC",
                    SensorValue = 2,
                    DateTimeId = 2
                },
                new hpws
                {
                    SensorName = "W1SdTcUC",
                    SensorValue = 3,
                    DateTimeId = 3
                }
            );

            context.SensorProjects.AddOrUpdate(
                  p => p.SensorProjectId,
                  new SensorProject
                  {
                      SensorProjectId = 1,
                      SensorProjectName = "hpws",
                      SensorProjectType = "Thermocouple",
                      SensorName = "W1GsTcUI"
                  },
                  new SensorProject
                  {
                      SensorProjectId = 2,
                      SensorProjectName = "hpws",
                      SensorProjectType = "Thermocouple",
                      SensorName = "W1TpTcBC"
                  },
                  new SensorProject
                  {
                      SensorProjectId = 3,
                      SensorProjectName = "hpws",
                      SensorProjectType = "Thermocouple",
                      SensorName = "W1SdTcUC"
                  }
                );
        }
    }
}
