using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            //context.SaveChanges();

            //Section Initialize with test data
            Section mathematicalAnalysisSection = new Section() { Name = "Математический анализ" };
            Section differentialEquationSection = new Section() { Name = "Дифференциальный уравнения" };
            Section probabilityTheorySectoin = new Section() { Name = "Теория вероятностей" };
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

            ////MathAssignment Initialization with test data
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
            //};
            //context.MathAssignments.AddOrUpdate(math1);
            //context.SaveChanges();

            ////MathAssignmentSubsection Initialization with test data
            //MathAssignmentSubsection mathSub1 = new MathAssignmentSubsection()
            //{
            //    MathAssignments = new Collection<MathAssignment>()
            //    {
            //        new MathAssignment()
            //        {
            //            SectionId = 1,
            //            ComplexityId = 1,
            //            AssignmentText = "Task1 ...",
            //            Answer = "Answer for task 1",
            //            TaskComments = new List<TaskComment>()
            //            {
            //                new TaskComment()
            //                {
            //                    ApplicationUserId = 1, 
            //                    Details = "Comment 1",
            //                    MathAssignmentId = 1,
            //                    PostedTime = new DateTime(1993, 12, 3)
            //                }
            //            }
            //        }
            //    },
            //    Subsections = new Collection<Subsection>()
            //    {
            //        new Subsection()
            //        {
            //            Name = "Subsection1"
            //        },
            //        new Subsection()
            //        {
            //            Name = "Subsection2"
            //        }
            //    }

            //};
            //MathAssignmentSubsection mathSub2 = new MathAssignmentSubsection()
            //{
            //    MathAssignmentId = 1,
            //    SubsectionId = 2
            //};
            //context.MathAssignmentSubsections.AddOrUpdate(mathSub1);
            //context.MathAssignmentSubsections.AddOrUpdate(mathSub2);
            //context.SaveChanges();

            base.Seed(context);
        }
    }
}
