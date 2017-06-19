namespace VeterinaryClinic.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhoneToPerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointment", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Appointment", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Appointment", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Person", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Person", "Phone");
            DropColumn("dbo.Appointment", "DeletedAt");
            DropColumn("dbo.Appointment", "UpdatedAt");
            DropColumn("dbo.Appointment", "CreatedAt");
        }
    }
}
