namespace PseudoCell.MyDataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017Nov4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActionChoiceId = c.Int(nullable: false),
                        Comments = c.String(),
                        AspNetUserId = c.String(nullable: false),
                        GradeLetter = c.String(),
                        GradePercent = c.Double(nullable: false),
                        GameName = c.String(),
                        ScenarioName = c.String(),
                        ActionChoiceName = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GameResults");
        }
    }
}
