using MathPro.Domain.Entities;

using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace MathPro.WebUI.DbContexts
{
    public class IdentityDb : IdentityDbContext<ApplicationUser>
    {
        public IdentityDb()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            
        }


        public static IdentityDb Create()
        {
            return new IdentityDb();
        }
        
    }
     
}