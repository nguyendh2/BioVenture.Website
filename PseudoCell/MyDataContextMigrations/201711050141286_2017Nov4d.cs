namespace PseudoCell.MyDataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017Nov4d : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameResults", "CompleteDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameResults", "CompleteDate");
        }
    }
}
