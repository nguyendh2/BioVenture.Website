namespace PseudoCell.MyDataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLastChangedBy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActionChoices", "LastChangedBy", c => c.String());
            AddColumn("dbo.ActionChoices", "LastChangedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Scenarios", "LastChangedBy", c => c.String());
            AddColumn("dbo.Scenarios", "LastChangedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Scenarios", "LastChangedDate");
            DropColumn("dbo.Scenarios", "LastChangedBy");
            DropColumn("dbo.ActionChoices", "LastChangedDate");
            DropColumn("dbo.ActionChoices", "LastChangedBy");
        }
    }
}
