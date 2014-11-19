using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathPro.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MathPro.WebUI.DbContexts
{
    public class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("DefaultConnection")
        {
            
        }

        public DbSet<Complexity> Complexities { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Subsection> Subsections { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }
        public DbSet<MathAssignment> MathAssignments { get; set; }

        public DbSet<Message> Messages { get; set; }
        public DbSet<UserAttempt> UserAttempts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MathAssignment>()
             .HasMany(c => c.Subsections).WithMany(i => i.MathAssignments)
             .Map(t => t.MapLeftKey("MathAssignmentId")
                 .MapRightKey("SubsectionId")
                 .ToTable("MathAssignmentSubsection"));

            // see http://stackoverflow.com/questions/19994590/asp-net-identity-validation-error
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            
        }
    }
}
