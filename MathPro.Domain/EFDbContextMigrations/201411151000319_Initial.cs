namespace MathPro.Domain.EFDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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
                "dbo.MathAssignments",
                c => new
                    {
                        MathAssignmentId = c.Int(nullable: false, identity: true),
                        SectionId = c.Int(nullable: false),
                        ComplexityId = c.Int(nullable: false),
                        AssignmentText = c.String(),
                        PointsForAssignment = c.Int(),
                        Answer = c.String(),
                        MathAssignmentSubsection_MathAssignmentId = c.Int(),
                        MathAssignmentSubsection_SubsectionId = c.Int(),
                    })
                .PrimaryKey(t => t.MathAssignmentId)
                .ForeignKey("dbo.Complexities", t => t.ComplexityId, cascadeDelete: true)
                .ForeignKey("dbo.MathAssignmentSubsections", t => new { t.MathAssignmentSubsection_MathAssignmentId, t.MathAssignmentSubsection_SubsectionId })
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .Index(t => t.SectionId)
                .Index(t => t.ComplexityId)
                .Index(t => new { t.MathAssignmentSubsection_MathAssignmentId, t.MathAssignmentSubsection_SubsectionId });
            
            CreateTable(
                "dbo.MathAssignmentSubsections",
                c => new
                    {
                        MathAssignmentId = c.Int(nullable: false),
                        SubsectionId = c.Int(nullable: false),
                        MathAssignmentSubsectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MathAssignmentId, t.SubsectionId })
                .ForeignKey("dbo.Subsections", t => t.SubsectionId, cascadeDelete: true)
                .ForeignKey("dbo.MathAssignments", t => t.MathAssignmentId, cascadeDelete: true)
                .Index(t => t.MathAssignmentId)
                .Index(t => t.SubsectionId);
            
            CreateTable(
                "dbo.Subsections",
                c => new
                    {
                        SubsectionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MathAssignmentSubsection_MathAssignmentId = c.Int(),
                        MathAssignmentSubsection_SubsectionId = c.Int(),
                    })
                .PrimaryKey(t => t.SubsectionId)
                .ForeignKey("dbo.MathAssignmentSubsections", t => new { t.MathAssignmentSubsection_MathAssignmentId, t.MathAssignmentSubsection_SubsectionId })
                .Index(t => new { t.MathAssignmentSubsection_MathAssignmentId, t.MathAssignmentSubsection_SubsectionId });
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        SectionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SectionId);
            
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
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.MathAssignments", t => t.MathAssignmentId, cascadeDelete: true)
                .Index(t => t.MathAssignmentId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(),
                        LastVisitDate = c.DateTime(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        Rating = c.Int(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.TaskComments", "MathAssignmentId", "dbo.MathAssignments");
            DropForeignKey("dbo.TaskComments", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.MathAssignments", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.MathAssignmentSubsections", "MathAssignmentId", "dbo.MathAssignments");
            DropForeignKey("dbo.Subsections", new[] { "MathAssignmentSubsection_MathAssignmentId", "MathAssignmentSubsection_SubsectionId" }, "dbo.MathAssignmentSubsections");
            DropForeignKey("dbo.MathAssignmentSubsections", "SubsectionId", "dbo.Subsections");
            DropForeignKey("dbo.MathAssignments", new[] { "MathAssignmentSubsection_MathAssignmentId", "MathAssignmentSubsection_SubsectionId" }, "dbo.MathAssignmentSubsections");
            DropForeignKey("dbo.MathAssignments", "ComplexityId", "dbo.Complexities");
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TaskComments", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TaskComments", new[] { "MathAssignmentId" });
            DropIndex("dbo.Subsections", new[] { "MathAssignmentSubsection_MathAssignmentId", "MathAssignmentSubsection_SubsectionId" });
            DropIndex("dbo.MathAssignmentSubsections", new[] { "SubsectionId" });
            DropIndex("dbo.MathAssignmentSubsections", new[] { "MathAssignmentId" });
            DropIndex("dbo.MathAssignments", new[] { "MathAssignmentSubsection_MathAssignmentId", "MathAssignmentSubsection_SubsectionId" });
            DropIndex("dbo.MathAssignments", new[] { "ComplexityId" });
            DropIndex("dbo.MathAssignments", new[] { "SectionId" });
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.TaskComments");
            DropTable("dbo.Sections");
            DropTable("dbo.Subsections");
            DropTable("dbo.MathAssignmentSubsections");
            DropTable("dbo.MathAssignments");
            DropTable("dbo.Complexities");
        }
    }
}
