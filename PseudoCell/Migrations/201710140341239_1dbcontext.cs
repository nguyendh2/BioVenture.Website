namespace PseudoCell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1dbcontext : DbMigration
    {
        public override void Up()
        {
            
        }

        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
