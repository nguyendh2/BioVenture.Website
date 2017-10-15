namespace PseudoCell.MyDataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class myDataContext_m6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionChoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        NextScenarioId = c.Int(nullable: false),
                        Scenario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Scenarios", t => t.Scenario_Id)
                .Index(t => t.Scenario_Id);
            
            CreateTable(
                "dbo.Scenarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActionChoices", "Scenario_Id", "dbo.Scenarios");
            DropIndex("dbo.ActionChoices", new[] { "Scenario_Id" });
            DropTable("dbo.Scenarios");
            DropTable("dbo.ActionChoices");
        }
    }
}
