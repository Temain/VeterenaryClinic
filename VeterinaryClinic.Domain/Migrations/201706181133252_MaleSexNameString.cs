namespace VeterinaryClinic.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaleSexNameString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dict.Sex", "SexName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dict.Sex", "SexName", c => c.Int(nullable: false));
        }
    }
}
