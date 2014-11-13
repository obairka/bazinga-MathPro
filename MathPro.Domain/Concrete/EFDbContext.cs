using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathPro.Domain.Entities;

namespace MathPro.Domain.Concrete
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
        public DbSet<MathAssignmentSubsection> MathAssignmentSubsections { get; set; }
    }

    public class EFDbInitializer : DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {

            //Complexity Initialize with test data
            Complexity easyComplexity = new Complexity() {  Name = "Новичок", DefaultPoints = 20 };
            Complexity intermediateComplexity = new Complexity() { Name = "Продвинутый", DefaultPoints = 30 };
            Complexity hardComplexity = new Complexity() { Name = "Профессионал", DefaultPoints = 40 };
            context.Complexities.Add(easyComplexity);
            context.Complexities.Add(intermediateComplexity);
            context.Complexities.Add(hardComplexity);

            //Section Initialize with test data
            Section mathematicalAnalysisSection = new Section() { Name = "Математический анализ" };
            Section differentialEquationSection = new Section() { Name = "Дифференциальный уравнения" };
            Section probabilityTheorySectoin = new Section() { Name = "Теория вероятностей" };
            Section mathematicalStatisticsSection = new Section() { Name = "Математическая статистика" };
            Section algebraSection = new Section() { Name = "Алгебра" };
            Section mathematicalLogicSection = new Section() { Name = "Математическая логика" };
            Section discreteMathematicsSection = new Section() { Name = "Дискретная математика" };
            Section numericalAnalysisSection = new Section() { Name = "Вычислительная математика" };
            context.Sections.Add(mathematicalAnalysisSection);
            context.Sections.Add(differentialEquationSection);
            context.Sections.Add(probabilityTheorySectoin);
            context.Sections.Add(mathematicalStatisticsSection);
            context.Sections.Add(algebraSection);
            context.Sections.Add(mathematicalLogicSection);
            context.Sections.Add(discreteMathematicsSection);
            context.Sections.Add(numericalAnalysisSection);

            //Subsection Initialize with test data
            Subsection subsection1 = new Subsection() { Name = "subsection1" };
            Subsection subsection2 = new Subsection() { Name = "subsection2" };
            context.Subsections.Add(subsection1);
            context.Subsections.Add(subsection2);

            //MathAssignment Initialization with test data
            MathAssignment math1 = new MathAssignment()
            {
                SectionId = 1,
                ComplexityId = 1,
                AssignmentText = "Task1 ...",
                Answer = "Answer for task 1",
                TaskComments = new List<TaskComment>()
                {
                    new TaskComment()
                    {
                        ApplicationUserId = 1, 
                        Details = "Comment 1",
                        MathAssignmentId = 1,
                        PostedTime = new DateTime(1993, 12, 3)
                    }
                }
            };
            context.MathAssignments.Add(math1);

            //MathAssignmentSubsection Initialization with test data
            MathAssignmentSubsection mathSub1 = new MathAssignmentSubsection()
            {
                MathAssignmentId = 1,
                SubsectionId = 1
            };
            MathAssignmentSubsection mathSub2 = new MathAssignmentSubsection()
            {
                MathAssignmentId = 1,
                SubsectionId = 2
            };
            context.MathAssignmentSubsections.Add(mathSub1);
            context.MathAssignmentSubsections.Add(mathSub2);

            base.Seed(context);
        }
    }
}
