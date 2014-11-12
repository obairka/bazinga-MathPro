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
        }



        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        
    }
     
}