namespace PseudoCell.MyDataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dec9b : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameResults", "AccumulatedComments", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameResults", "AccumulatedComments");
        }
    }
}
