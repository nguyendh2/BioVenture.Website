namespace PseudoCell.MyDataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addGameName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ActionChoices", "Scenario_Id", "dbo.Scenarios");
            DropIndex("dbo.ActionChoices", new[] { "Scenario_Id" });
            RenameColumn(table: "dbo.ActionChoices", name: "Scenario_Id", newName: "ScenarioId");
            AddColumn("dbo.ActionChoices", "NextScenarioName", c => c.String());
            AddColumn("dbo.ActionChoices", "ScenarioName", c => c.String());
            AddColumn("dbo.Scenarios", "GameName", c => c.String());
            AlterColumn("dbo.ActionChoices", "ScenarioId", c => c.Int(nullable: false));
            CreateIndex("dbo.ActionChoices", "ScenarioId");
            AddForeignKey("dbo.ActionChoices", "ScenarioId", "dbo.Scenarios", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActionChoices", "ScenarioId", "dbo.Scenarios");
            DropIndex("dbo.ActionChoices", new[] { "ScenarioId" });
            AlterColumn("dbo.ActionChoices", "ScenarioId", c => c.Int());
            DropColumn("dbo.Scenarios", "GameName");
            DropColumn("dbo.ActionChoices", "ScenarioName");
            DropColumn("dbo.ActionChoices", "NextScenarioName");
            RenameColumn(table: "dbo.ActionChoices", name: "ScenarioId", newName: "Scenario_Id");
            CreateIndex("dbo.ActionChoices", "Scenario_Id");
            AddForeignKey("dbo.ActionChoices", "Scenario_Id", "dbo.Scenarios", "Id");
        }
    }
}
