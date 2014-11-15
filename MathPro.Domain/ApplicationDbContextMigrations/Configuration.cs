using Microsoft.AspNet.Identity;

namespace MathPro.Domain.ApplicationDbContextMigrations
{
    using MathPro.Domain.Entities;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MathPro.Domain.Concrete.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            MigrationsDirectory = @"ApplicationDbContextMigrations";
            ContextKey = "MathPro.Domain.Concrete.ApplicationDbContext";
        }

        protected override void Seed(MathPro.Domain.Concrete.ApplicationDbContext context)
        {
            const string roleName = "Admin";

            if (!context.Roles.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole
                {
                    Name = roleName
                };
                roleManager.Create(role);
                const string name = "admin";
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                if (userManager.FindByName(name) == null)
                {
                    var user = new ApplicationUser
                    {
                        Email = "Admin@123",
                        UserName = name,
                        FirstName = name,
                        LastName = name,
                        Rating = 0,
                        // TODO: 
                        RegistrationDate = DateTime.Now,
                        LastVisitDate = DateTime.Now,
                    };
                    var result = userManager.Create(user, "Admin@123");
                    result = userManager.SetLockoutEnabled(user.Id, false);

                    var rolesForUser = userManager.GetRoles(user.Id);
                    if (!rolesForUser.Contains(role.Name))
                    {
                        result = userManager.AddToRole(user.Id, role.Name);
                    }
                }
            }
            base.Seed(context);
        }
    }
}
