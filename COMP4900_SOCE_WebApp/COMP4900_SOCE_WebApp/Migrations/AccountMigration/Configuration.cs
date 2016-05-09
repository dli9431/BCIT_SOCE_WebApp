namespace COMP4900_SOCE_WebApp.Migrations.AccountMigration
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using SensorDataModel.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<COMP4900_SOCE_WebApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
            MigrationsDirectory = @"Migrations\AccountMigration";
        }

        protected override void Seed(COMP4900_SOCE_WebApp.Models.ApplicationDbContext context)
        {
            //Initialize the managers/stores
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            // If role does not exists, create it
            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }
            if (!roleManager.RoleExists("Supervisor"))
            {
                roleManager.Create(new IdentityRole("Supervisor"));
            }
            if (!roleManager.RoleExists("Student"))
            {
                roleManager.Create(new IdentityRole("Student"));
            }

            // Create test users
            //Create administrator test user
            var adminUser = userManager.FindByName("A00111111");
            if (adminUser == null)
            {
                var newAdminUser = new ApplicationUser()
                {
                    UserName = "A00111111",
                    Email = "a@a.a"
                };
                var result = userManager.Create(newAdminUser, "P@$$w0rd");
                if (result.Succeeded)
                {
                    userManager.SetLockoutEnabled(newAdminUser.Id, false);
                    userManager.AddToRole(newAdminUser.Id, "Admin");
                    userManager.AddToRole(newAdminUser.Id, "Student");
                }

            }
            //Create student test user
            var studentUser = userManager.FindByName("A00222222");
            if (studentUser == null)
            {
                var newStudentUser = new ApplicationUser()
                {
                    UserName = "A00222222",
                    Email = "s@s.s"
                };
                var result = userManager.Create(newStudentUser, "P@$$w0rd");
                if (result.Succeeded)
                {
                    userManager.SetLockoutEnabled(newStudentUser.Id, false);
                    userManager.AddToRole(newStudentUser.Id, "Student");
                }
            }

            // Create test users
            //Create administrator test user
            var studentUser2 = userManager.FindByName("A00333333");
            if (studentUser2 == null)
            {
                var newSupervisorUser = new ApplicationUser()
                {
                    UserName = "A00333333",
                    Email = "b@b.b"
                };
                var result = userManager.Create(newSupervisorUser, "P@$$w0rd");
                if (result.Succeeded)
                {
                    userManager.SetLockoutEnabled(newSupervisorUser.Id, true);
                    userManager.AddToRole(newSupervisorUser.Id, "Supervisor");
                }

            }
        }

    }
}