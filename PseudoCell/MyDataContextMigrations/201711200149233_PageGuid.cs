namespace PseudoCell.MyDataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PageGuid : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PageTokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PageGuid = c.Guid(nullable: false),
                        AspNetUserId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PageTokens");
        }
    }
}
