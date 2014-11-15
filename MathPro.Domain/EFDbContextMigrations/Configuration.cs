using MathPro.Domain.Entities;

namespace MathPro.Domain.EFDbContextMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MathPro.Domain.Concrete.EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            MigrationsDirectory = @"EFDbContextMigrations";
            ContextKey = "MathPro.Domain.Concrete.EFDbContext";
        }

        protected override void Seed(MathPro.Domain.Concrete.EFDbContext context)
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
            //Complexity Initialize with test data
            Complexity easyComplexity = new Complexity() { Name = "Новичок", DefaultPoints = 20 };
            Complexity intermediateComplexity = new Complexity() { Name = "Продвинутый", DefaultPoints = 30 };
            Complexity hardComplexity = new Complexity() { Name = "Профессионал", DefaultPoints = 40 };
            context.Complexities.AddOrUpdate(p => p.Name, easyComplexity);
            context.Complexities.AddOrUpdate(p => p.Name, intermediateComplexity);
            context.Complexities.AddOrUpdate(p => p.Name, hardComplexity);
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
