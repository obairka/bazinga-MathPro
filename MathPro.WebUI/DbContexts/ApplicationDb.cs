using MathPro.Domain.Entities;
using System.Collections.Generic;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;

namespace MathPro.WebUI.DbContexts
{
    public class ApplicationDb : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDb()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            
        }


        public static ApplicationDb Create()
        {
            return new ApplicationDb();
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
            // modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            // modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            // modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }


        static ApplicationDb()
        {
            Database.SetInitializer<ApplicationDb>(new ApplicationDbInitializer());
        }
        
    }

    public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDb>
    {
        protected override void Seed(ApplicationDb context)
        {
            InitializeIdentity(context);
            //InitializeComplexity(context);
            //InitializeSection(context);
            //InitializeSubsection(context);
            InitializeMathAssignment(context);
            context.SaveChanges();
            base.Seed(context);
        }

        public static void InitializeComplexity(ApplicationDb context)
        {
            //Complexity Initialize with test data
            Complexity easyComplexity = new Complexity() { Name = "Новичок", DefaultPoints = 20 };
            Complexity intermediateComplexity = new Complexity() { Name = "Продвинутый", DefaultPoints = 30 };
            Complexity hardComplexity = new Complexity() { Name = "Профессионал", DefaultPoints = 40 };
            context.Complexities.AddOrUpdate(p => p.Name, easyComplexity);
            context.Complexities.AddOrUpdate(p => p.Name, intermediateComplexity);
            context.Complexities.AddOrUpdate(p => p.Name, hardComplexity);
        }

        public static void InitializeSection(ApplicationDb context)
        {
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
        }

        public static void InitializeSubsection(ApplicationDb context)
        {
            //Complexity Initialize with test data
            Subsection subsection1 = new Subsection() { Name = "Подраздел1" };
            Subsection subsection2 = new Subsection() { Name = "Подраздел2" };
            
            context.Subsections.AddOrUpdate(p => p.Name, subsection1);
            context.Subsections.AddOrUpdate(p => p.Name, subsection2);
        }

        public static void InitializeMathAssignment(ApplicationDb context)
        {
            Complexity easy = new Complexity()
            {
                Name = "Новичок",
                DefaultPoints = 20
            };
            Complexity intermediate = new Complexity()
            {
                Name = "Продвинутый",
                DefaultPoints = 30
            };
            Complexity pro = new Complexity()
            {
                Name = "Профессионал",
                DefaultPoints = 40
            };

            Section mathematicalAnalysisSection = new Section() { Name = "Математический анализ" };
            Section differentialEquationSection = new Section() { Name = "Дифференцаиальные уравнения" };
            Section probabilityTheorySectoin = new Section() { Name = "Теория вероятности" };
            Section mathematicalStatisticsSection = new Section() { Name = "Математическая статистика" };
            Section algebraSection = new Section() { Name = "Алгебра" };
            Section mathematicalLogicSection = new Section() { Name = "Математическая логика" };
            Section discreteMathematicsSection = new Section() { Name = "Дискретная математика" };
            Section numericalAnalysisSection = new Section() { Name = "Вычислительная математика" };

            Subsection subsection1 = new Subsection() { Name = "Вычисление производных" };
            Subsection subsection2 = new Subsection() { Name = "Частные производные неявно заданной функции" };
            Subsection subsection3 = new Subsection() { Name = "Производная параметрически заданной функции" };

            MathAssignment math1 = new MathAssignment
            {
                Section = mathematicalAnalysisSection,
                Complexity = easy,
                AssignmentText = "Найти\\ производную \\ от \\ следующей \\ функции \\\\ {y=\\left[\\sqrt[3]{\\frac{1}{7+x^2}}+\\frac{\\sqrt{x}}{\\sqrt{x}+1} \\right]\\cdot 24 \\\\ y'(1) = ?}",
                Answer = "{y' =24 \\left[((7 + x^2)^{-\\frac{1}{3}})' + \\left(\\frac{\\sqrt{x}}{\\sqrt{x} + 1}\\right)'\\ \\right] = \\\\24\\left[-\\frac{1}{3}(7 + x^2)^{-\\frac{4}{3}}2x + \\frac{1/2x^{-\\frac{1}{2}}(x^\\frac{1}{2})1/2x^{-\\frac{1}{2}}}{(\\sqrt{x} + 1)^2}\\right]} = \\\\ -\\frac{16x}{(7 + x^2)^\\frac{4}{3}} + \\frac{12}{\\sqrt{x} (\\sqrt{x} + 1)^2};\\\\ y'(1) = -\\frac{16}{8^\\frac{4}{3}} + \\frac{12}{4} = 2.",
                Subsections = new Collection<Subsection>()
                {
                    subsection1
                },
                PointsForAssignment = easy.DefaultPoints
            };
            var t = context.Subsections.Where(c => c.Name == "Вычисление производных");
            MathAssignment math2 = new MathAssignment
            {
                Section = differentialEquationSection,
                Complexity = intermediate,
                AssignmentText = "Найти \\ y_{xx}^{''}, \\ если \\\\\\left\\{\\begin{array}{l}x=t^4-t^2+1,\\\\y=t^4+t^2+1.\\end{array}\\right.",
                Answer = "Answer",
                Subsections = new Collection<Subsection>()
                {
                    subsection1, subsection2
                },
                PointsForAssignment = intermediate.DefaultPoints
            };
            MathAssignment math3 = new MathAssignment
            {
                Section = mathematicalAnalysisSection,
                Complexity = pro,
                AssignmentText = "Функция z = z(x, y) задана \\ неявно \\ уравнением \\\\2x^2+2y^2+z^2-8xz-z+8=0 \\\\Вычислить \\\\\\frac{\\partial z}{\\partial x}(2,0,1), \\frac{\\partial z}{\\partial y}(2,0,1)",
                Answer = "Answer",
                Subsections = new Collection<Subsection>()
                {
                    subsection1, subsection2, subsection3
                },
                PointsForAssignment = pro.DefaultPoints
            };
            context.MathAssignments.AddOrUpdate(math1);
            context.MathAssignments.AddOrUpdate(math2);
            context.MathAssignments.AddOrUpdate(math3);

        }



        public static void InitializeIdentity(ApplicationDb context)
        {
            // Add Admin user 
            
            //var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new ApplicationUserManager(userStore);
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new ApplicationRoleManager(roleStore);
            const string name = "admin";
            const string password = "Admin@123456";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser 
                {
                    UserName = name, 
                    Email = "mathpro@gmail.com",
                    FirstName = name,
                    LastName = name,
                    LastVisitDate = DateTime.Today.ToUniversalTime(),
                    RegistrationDate = DateTime.Today.ToUniversalTime(),

                };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }

    }
}