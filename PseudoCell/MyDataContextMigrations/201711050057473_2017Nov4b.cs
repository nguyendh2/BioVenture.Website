namespace PseudoCell.MyDataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017Nov4b : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameResults", "StudentId", c => c.String());
            AddColumn("dbo.GameResults", "StudentName", c => c.String());
            AlterColumn("dbo.GameResults", "GradePercent", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GameResults", "GradePercent", c => c.Double(nullable: false));
            DropColumn("dbo.GameResults", "StudentName");
            DropColumn("dbo.GameResults", "StudentId");
        }
    }
}
