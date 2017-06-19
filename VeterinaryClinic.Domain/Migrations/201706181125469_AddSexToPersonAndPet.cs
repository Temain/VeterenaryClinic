namespace VeterinaryClinic.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSexToPersonAndPet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dict.Sex",
                c => new
                    {
                        SexId = c.Int(nullable: false, identity: true),
                        SexName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SexId);
            
            AddColumn("dbo.Person", "SexId", c => c.Int());
            AddColumn("dbo.Pet", "SexId", c => c.Int());
            CreateIndex("dbo.Person", "SexId");
            CreateIndex("dbo.Pet", "SexId");
            AddForeignKey("dbo.Person", "SexId", "dict.Sex", "SexId");
            AddForeignKey("dbo.Pet", "SexId", "dict.Sex", "SexId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pet", "SexId", "dict.Sex");
            DropForeignKey("dbo.Person", "SexId", "dict.Sex");
            DropIndex("dbo.Pet", new[] { "SexId" });
            DropIndex("dbo.Person", new[] { "SexId" });
            DropColumn("dbo.Pet", "SexId");
            DropColumn("dbo.Person", "SexId");
            DropTable("dict.Sex");
        }
    }
}
