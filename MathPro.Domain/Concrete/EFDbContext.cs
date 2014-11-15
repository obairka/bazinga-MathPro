using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathPro.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MathPro.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EFDbContext, MathPro.Domain.EFDbContextMigrations.Configuration>("DefaultConnection"));
        }

        public DbSet<Complexity> Complexities { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Subsection> Subsections { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }
        public DbSet<MathAssignment> MathAssignments { get; set; }
        public DbSet<MathAssignmentSubsection> MathAssignmentSubsections { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MathAssignmentSubsection>()
                .HasKey(c => new {c.MathAssignmentId, c.SubsectionId});
            
            modelBuilder.Entity<MathAssignment>()
                .HasMany(c => c.MathAssignmentSubsections)
                .WithRequired()
                .HasForeignKey(c => c.MathAssignmentId);
            
            modelBuilder.Entity<Subsection>()
                .HasMany(c => c.MathAssignmentSubsections)
                .WithRequired()
                .HasForeignKey(c => c.SubsectionId);

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }
}
