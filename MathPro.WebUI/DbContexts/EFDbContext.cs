using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
            base.OnModelCreating(modelBuilder);
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

     public class EFDbInitializer : DropCreateDatabaseAlways<EFDbContext>
    {
         protected override void Seed(EFDbContext context)
         {
             //Complexity Initialize with test data
             Complexity easyComplexity = new Complexity() { Name = "Новичок", DefaultPoints = 20 };
             Complexity intermediateComplexity = new Complexity() { Name = "Продвинутый", DefaultPoints = 30 };
             Complexity hardComplexity = new Complexity() { Name = "Профессионал", DefaultPoints = 40 };
             context.Complexities.AddOrUpdate(p => p.Name, easyComplexity);
             context.Complexities.AddOrUpdate(p => p.Name, intermediateComplexity);
             context.Complexities.AddOrUpdate(p => p.Name, hardComplexity);
             //context.SaveChanges();

             //Section Initialize with test data
             Section mathematicalAnalysisSection = new Section() { Name = "Математический анализ" };
             Section differentialEquationSection = new Section() { Name = "Дифференцаиальный уравнения" };
             Section probabilityTheorySectoin = new Section() { Name = "Теория вероятности" };
             Section mathematicalStatisticsSection = new Section() { Name = "Математическая статистика" };
             Section algebraSection = new Section() { Name = "Алгебра" };
             Section mathematicalLogicSection = new Section() { Name = "Математическая логика" };
             Section discreteMathematicsSection = new Section() { Name = "Дискретная математика" };
             Section numericalAnalysisSection = new Section() { Name = "Вычислительная математика" };
             context.Sections.AddOrUpdate(mathematicalAnalysisSection);
             context.Sections.AddOrUpdate(differentialEquationSection);
             context.Sections.AddOrUpdate(probabilityTheorySectoin);
             context.Sections.AddOrUpdate(mathematicalStatisticsSection);
             context.Sections.AddOrUpdate(algebraSection);
             context.Sections.AddOrUpdate(mathematicalLogicSection);
             context.Sections.AddOrUpdate(discreteMathematicsSection);
             context.Sections.AddOrUpdate(numericalAnalysisSection);
             //context.SaveChanges();

             //Subsection Initialize with test data
             Subsection subsection1 = new Subsection() { Name = "subsection1" };
             Subsection subsection2 = new Subsection() { Name = "subsection2" };
             context.Subsections.AddOrUpdate(subsection1);
             context.Subsections.AddOrUpdate(subsection2);
             context.SaveChanges();

             //MathAssignment Initialization with test data
             //MathAssignment math1 = new MathAssignment()
             //{
             //    SectionId = 1,
             //    ComplexityId = 1,
             //    AssignmentText = "Task1 ...",
             //    Answer = "Answer for task 1",
             //    TaskComments = new List<TaskComment>()
             //    {
             //        new TaskComment()
             //        {
             //            ApplicationUserId = 1, 
             //            Details = "Comment 1",
             //            MathAssignmentId = 1,
             //            PostedTime = new DateTime(1993, 12, 3)
             //        }
             //    },
             //    Subsections = new List<Subsection>()
             //    {
             //        new Subsection()
             //        {
             //           Name = "subsection3",
             //        },
             //        new Subsection()
             //        {
             //            Name = "subsection4",
             //        }
             //    }
             //};
             //context.MathAssignments.AddOrUpdate(math1);
             //context.SaveChanges();

             base.Seed(context);
         }
    }
}
