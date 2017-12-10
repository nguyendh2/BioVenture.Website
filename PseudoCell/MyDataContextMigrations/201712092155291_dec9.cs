namespace PseudoCell.MyDataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dec9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Scenarios", "IsCommentRequired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Scenarios", "IsCommentRequired");
        }
    }
}
