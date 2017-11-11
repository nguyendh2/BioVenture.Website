namespace PseudoCell.MyDataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nov11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameResults", "LastChangedDateTime", c => c.DateTime());
            AddColumn("dbo.GameResults", "LastChangedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameResults", "LastChangedBy");
            DropColumn("dbo.GameResults", "LastChangedDateTime");
        }
    }
}
