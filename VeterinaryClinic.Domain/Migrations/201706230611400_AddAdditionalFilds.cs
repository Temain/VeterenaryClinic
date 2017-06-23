namespace VeterinaryClinic.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdditionalFilds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointment", "ParasiteTreatmentDate", c => c.DateTime());
            AddColumn("dbo.Appointment", "Anamnesis", c => c.String());
            AddColumn("dbo.Appointment", "Therapy", c => c.String());
            AddColumn("dbo.Pet", "HaveOperations", c => c.String());
            AddColumn("dbo.Pet", "Allergies", c => c.String());
            AddColumn("dbo.Pet", "ChronicDiseases", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pet", "ChronicDiseases");
            DropColumn("dbo.Pet", "Allergies");
            DropColumn("dbo.Pet", "HaveOperations");
            DropColumn("dbo.Appointment", "Therapy");
            DropColumn("dbo.Appointment", "Anamnesis");
            DropColumn("dbo.Appointment", "ParasiteTreatmentDate");
        }
    }
}
