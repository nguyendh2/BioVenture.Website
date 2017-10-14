namespace PseudoCell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "StudentId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "StudentId");
        }
    }
}
