namespace MathPro.Domain.ApplicationDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(),
                        LastVisitDate = c.DateTime(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        Rating = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TaskComments",
                c => new
                    {
                        TaskCommentId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.Int(nullable: false),
                        MathAssignmentId = c.Int(nullable: false),
                        Details = c.String(),
                        PostedTime = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TaskCommentId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.MathAssignments", t => t.MathAssignmentId, cascadeDelete: true)
                .Index(t => t.MathAssignmentId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.MathAssignments",
                c => new
                    {
                        MathAssignmentId = c.Int(nullable: false, identity: true),
                        SectionId = c.Int(nullable: false),
                        ComplexityId = c.Int(nullable: false),
                        AssignmentText = c.String(),
                        PointsForAssignment = c.Int(),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => t.MathAssignmentId)
                .ForeignKey("dbo.Complexities", t => t.ComplexityId, cascadeDelete: true)
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .Index(t => t.SectionId)
                .Index(t => t.ComplexityId);
            
            CreateTable(
                "dbo.Complexities",
                c => new
                    {
                        ComplexityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DefaultPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ComplexityId);
            
            CreateTable(
                "dbo.MathAssignmentSubsections",
                c => new
                    {
                        MathAssignmentSubsectionId = c.Int(nullable: false, identity: true),
                        MathAssignmentId = c.Int(nullable: false),
                        SubsectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MathAssignmentSubsectionId);
            
            CreateTable(
                "dbo.Subsections",
                c => new
                    {
                        SubsectionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SubsectionId);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        SectionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SectionId);
            
            CreateTable(
                "dbo.MathAssignmentSubsectionMathAssignments",
                c => new
                    {
                        MathAssignmentSubsection_MathAssignmentSubsectionId = c.Int(nullable: false),
                        MathAssignment_MathAssignmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MathAssignmentSubsection_MathAssignmentSubsectionId, t.MathAssignment_MathAssignmentId })
                .ForeignKey("dbo.MathAssignmentSubsections", t => t.MathAssignmentSubsection_MathAssignmentSubsectionId, cascadeDelete: true)
                .ForeignKey("dbo.MathAssignments", t => t.MathAssignment_MathAssignmentId, cascadeDelete: true)
                .Index(t => t.MathAssignmentSubsection_MathAssignmentSubsectionId)
                .Index(t => t.MathAssignment_MathAssignmentId);
            
            CreateTable(
                "dbo.SubsectionMathAssignmentSubsections",
                c => new
                    {
                        Subsection_SubsectionId = c.Int(nullable: false),
                        MathAssignmentSubsection_MathAssignmentSubsectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Subsection_SubsectionId, t.MathAssignmentSubsection_MathAssignmentSubsectionId })
                .ForeignKey("dbo.Subsections", t => t.Subsection_SubsectionId, cascadeDelete: true)
                .ForeignKey("dbo.MathAssignmentSubsections", t => t.MathAssignmentSubsection_MathAssignmentSubsectionId, cascadeDelete: true)
                .Index(t => t.Subsection_SubsectionId)
                .Index(t => t.MathAssignmentSubsection_MathAssignmentSubsectionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskComments", "MathAssignmentId", "dbo.MathAssignments");
            DropForeignKey("dbo.MathAssignments", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.SubsectionMathAssignmentSubsections", "MathAssignmentSubsection_MathAssignmentSubsectionId", "dbo.MathAssignmentSubsections");
            DropForeignKey("dbo.SubsectionMathAssignmentSubsections", "Subsection_SubsectionId", "dbo.Subsections");
            DropForeignKey("dbo.MathAssignmentSubsectionMathAssignments", "MathAssignment_MathAssignmentId", "dbo.MathAssignments");
            DropForeignKey("dbo.MathAssignmentSubsectionMathAssignments", "MathAssignmentSubsection_MathAssignmentSubsectionId", "dbo.MathAssignmentSubsections");
            DropForeignKey("dbo.MathAssignments", "ComplexityId", "dbo.Complexities");
            DropForeignKey("dbo.TaskComments", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.SubsectionMathAssignmentSubsections", new[] { "MathAssignmentSubsection_MathAssignmentSubsectionId" });
            DropIndex("dbo.SubsectionMathAssignmentSubsections", new[] { "Subsection_SubsectionId" });
            DropIndex("dbo.MathAssignmentSubsectionMathAssignments", new[] { "MathAssignment_MathAssignmentId" });
            DropIndex("dbo.MathAssignmentSubsectionMathAssignments", new[] { "MathAssignmentSubsection_MathAssignmentSubsectionId" });
            DropIndex("dbo.MathAssignments", new[] { "ComplexityId" });
            DropIndex("dbo.MathAssignments", new[] { "SectionId" });
            DropIndex("dbo.TaskComments", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TaskComments", new[] { "MathAssignmentId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.SubsectionMathAssignmentSubsections");
            DropTable("dbo.MathAssignmentSubsectionMathAssignments");
            DropTable("dbo.Sections");
            DropTable("dbo.Subsections");
            DropTable("dbo.MathAssignmentSubsections");
            DropTable("dbo.Complexities");
            DropTable("dbo.MathAssignments");
            DropTable("dbo.TaskComments");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
