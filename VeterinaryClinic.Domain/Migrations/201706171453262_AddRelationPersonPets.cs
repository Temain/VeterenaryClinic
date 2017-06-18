namespace VeterinaryClinic.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationPersonPets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pet", "PersonId", c => c.Int(nullable: false));
            CreateIndex("dbo.Pet", "PersonId");
            AddForeignKey("dbo.Pet", "PersonId", "dbo.Person", "PersonId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pet", "PersonId", "dbo.Person");
            DropIndex("dbo.Pet", new[] { "PersonId" });
            DropColumn("dbo.Pet", "PersonId");
        }
    }
}
