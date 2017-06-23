namespace VeterinaryClinic.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOperationNameToOperationTypeName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dict.OperationType", "OperationTypeName", c => c.String());
            DropColumn("dict.OperationType", "OperationName");
        }
        
        public override void Down()
        {
            AddColumn("dict.OperationType", "OperationName", c => c.String());
            DropColumn("dict.OperationType", "OperationTypeName");
        }
    }
}
