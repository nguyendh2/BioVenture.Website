namespace PseudoCell.MyDataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        AspNetUserId = c.String(nullable: false, maxLength: 450),
                        username = c.String(nullable: false, maxLength: 30),
                        IsManager = c.Boolean(nullable: false),
                        IsStudent = c.Boolean(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        StudentId = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
