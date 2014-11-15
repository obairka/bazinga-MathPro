using MathPro.Domain.Entities;

using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace MathPro.Domain.Concrete
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, MathPro.Domain.ApplicationDbContextMigrations.Configuration>("DefaultConnection"));
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
    }
     
}