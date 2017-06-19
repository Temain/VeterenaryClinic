namespace VeterinaryClinic.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeletedToPet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pet", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Pet", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Pet", "DeletedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pet", "DeletedAt");
            DropColumn("dbo.Pet", "UpdatedAt");
            DropColumn("dbo.Pet", "CreatedAt");
        }
    }
}
