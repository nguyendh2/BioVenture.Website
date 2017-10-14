namespace PseudoCell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialMigration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                {
                    UserId = c.Int(nullable: false, identity: true),
                    AspNetUserId = c.Int(nullable: false),
                    username = c.String(nullable: false),
                    IsManager = c.Boolean(nullable: false),
                    IsStudent = c.Boolean(nullable: false),
                    IsAdmin = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.UserId);
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
