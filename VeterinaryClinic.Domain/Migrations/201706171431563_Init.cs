namespace VeterinaryClinic.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointment",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        AppointmentDate = c.DateTime(nullable: false),
                        Age = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Temperature = c.Int(nullable: false),
                        PetId = c.Int(nullable: false),
                        Complaints = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.AppointmentId)
                .ForeignKey("dbo.Pet", t => t.PetId)
                .Index(t => t.PetId);
            
            CreateTable(
                "dbo.PetOperation",
                c => new
                    {
                        PetOperationId = c.Int(nullable: false, identity: true),
                        AppointmentId = c.Int(nullable: false),
                        OperationId = c.Int(nullable: false),
                        OperationDate = c.DateTime(),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PetOperationId)
                .ForeignKey("dbo.Appointment", t => t.AppointmentId)
                .ForeignKey("dbo.Employee", t => t.EmployeeId)
                .ForeignKey("dict.Operation", t => t.OperationId)
                .Index(t => t.AppointmentId)
                .Index(t => t.OperationId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        HireDate = c.DateTime(nullable: false),
                        PositionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .ForeignKey("dict.Position", t => t.PositionId)
                .Index(t => t.PersonId)
                .Index(t => t.PositionId);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        LastName = c.String(maxLength: 500),
                        FirstName = c.String(nullable: false, maxLength: 500),
                        MiddleName = c.String(maxLength: 500),
                        Birthday = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.PersonId);
            
            CreateTable(
                "dict.Position",
                c => new
                    {
                        PositionId = c.Int(nullable: false, identity: true),
                        PositionName = c.String(),
                    })
                .PrimaryKey(t => t.PositionId);
            
            CreateTable(
                "dbo.OperationPosition",
                c => new
                    {
                        OperationPositionId = c.Int(nullable: false, identity: true),
                        OperationId = c.Int(nullable: false),
                        PositionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OperationPositionId)
                .ForeignKey("dict.Operation", t => t.OperationId)
                .ForeignKey("dict.Position", t => t.PositionId)
                .Index(t => t.OperationId)
                .Index(t => t.PositionId);
            
            CreateTable(
                "dict.Operation",
                c => new
                    {
                        OperationId = c.Int(nullable: false, identity: true),
                        OperationName = c.String(),
                        OperationTypeId = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        MaterialCosts = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OperationId)
                .ForeignKey("dict.OperationType", t => t.OperationTypeId)
                .Index(t => t.OperationTypeId);
            
            CreateTable(
                "dict.OperationType",
                c => new
                    {
                        OperationTypeId = c.Int(nullable: false, identity: true),
                        OperationName = c.String(),
                    })
                .PrimaryKey(t => t.OperationTypeId);
            
            CreateTable(
                "dbo.Pet",
                c => new
                    {
                        PetId = c.Int(nullable: false, identity: true),
                        PetTypeId = c.Int(nullable: false),
                        PetName = c.String(),
                    })
                .PrimaryKey(t => t.PetId)
                .ForeignKey("dbo.PetType", t => t.PetTypeId)
                .Index(t => t.PetTypeId);
            
            CreateTable(
                "dbo.PetType",
                c => new
                    {
                        PetTypeId = c.Int(nullable: false, identity: true),
                        PetTypeName = c.String(),
                    })
                .PrimaryKey(t => t.PetTypeId);
            
            CreateTable(
                "serv.LogEntry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Level = c.String(),
                        Logger = c.String(),
                        ClassMethod = c.String(),
                        Message = c.String(),
                        Username = c.String(),
                        RequestUri = c.String(),
                        RemoteAddress = c.String(),
                        UserAgent = c.String(),
                        Exception = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        FullName = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
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
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Pet", "PetTypeId", "dbo.PetType");
            DropForeignKey("dbo.Appointment", "PetId", "dbo.Pet");
            DropForeignKey("dbo.OperationPosition", "PositionId", "dict.Position");
            DropForeignKey("dbo.OperationPosition", "OperationId", "dict.Operation");
            DropForeignKey("dict.Operation", "OperationTypeId", "dict.OperationType");
            DropForeignKey("dbo.PetOperation", "OperationId", "dict.Operation");
            DropForeignKey("dbo.Employee", "PositionId", "dict.Position");
            DropForeignKey("dbo.Employee", "PersonId", "dbo.Person");
            DropForeignKey("dbo.PetOperation", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.PetOperation", "AppointmentId", "dbo.Appointment");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Pet", new[] { "PetTypeId" });
            DropIndex("dict.Operation", new[] { "OperationTypeId" });
            DropIndex("dbo.OperationPosition", new[] { "PositionId" });
            DropIndex("dbo.OperationPosition", new[] { "OperationId" });
            DropIndex("dbo.Employee", new[] { "PositionId" });
            DropIndex("dbo.Employee", new[] { "PersonId" });
            DropIndex("dbo.PetOperation", new[] { "EmployeeId" });
            DropIndex("dbo.PetOperation", new[] { "OperationId" });
            DropIndex("dbo.PetOperation", new[] { "AppointmentId" });
            DropIndex("dbo.Appointment", new[] { "PetId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("serv.LogEntry");
            DropTable("dbo.PetType");
            DropTable("dbo.Pet");
            DropTable("dict.OperationType");
            DropTable("dict.Operation");
            DropTable("dbo.OperationPosition");
            DropTable("dict.Position");
            DropTable("dbo.Person");
            DropTable("dbo.Employee");
            DropTable("dbo.PetOperation");
            DropTable("dbo.Appointment");
        }
    }
}
