namespace VeterinaryClinic.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeSexIdNotNull : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Person", new[] { "SexId" });
            DropIndex("dbo.Pet", new[] { "SexId" });
            AlterColumn("dbo.Person", "SexId", c => c.Int(nullable: false));
            AlterColumn("dbo.Pet", "SexId", c => c.Int(nullable: false));
            CreateIndex("dbo.Person", "SexId");
            CreateIndex("dbo.Pet", "SexId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Pet", new[] { "SexId" });
            DropIndex("dbo.Person", new[] { "SexId" });
            AlterColumn("dbo.Pet", "SexId", c => c.Int());
            AlterColumn("dbo.Person", "SexId", c => c.Int());
            CreateIndex("dbo.Pet", "SexId");
            CreateIndex("dbo.Person", "SexId");
        }
    }
}
