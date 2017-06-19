namespace VeterinaryClinic.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeletedToPetOperation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PetOperation", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.PetOperation", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.PetOperation", "DeletedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PetOperation", "DeletedAt");
            DropColumn("dbo.PetOperation", "UpdatedAt");
            DropColumn("dbo.PetOperation", "CreatedAt");
        }
    }
}
