namespace PseudoCell.MyDataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCreatedBy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActionChoices", "CreatedBy", c => c.String());
            AddColumn("dbo.ActionChoices", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Scenarios", "CreatedBy", c => c.String());
            AddColumn("dbo.Scenarios", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ActionChoices", "LastChangedDate", c => c.DateTime());
            AlterColumn("dbo.Scenarios", "LastChangedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Scenarios", "LastChangedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ActionChoices", "LastChangedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Scenarios", "CreatedDate");
            DropColumn("dbo.Scenarios", "CreatedBy");
            DropColumn("dbo.ActionChoices", "CreatedDate");
            DropColumn("dbo.ActionChoices", "CreatedBy");
        }
    }
}
