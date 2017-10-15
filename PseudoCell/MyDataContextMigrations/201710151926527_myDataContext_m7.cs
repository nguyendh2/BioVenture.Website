namespace PseudoCell.MyDataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class myDataContext_m7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "LastChangedDate", c => c.DateTime());
            AddColumn("dbo.Games", "LastChangedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "LastChangedBy");
            DropColumn("dbo.Games", "LastChangedDate");
        }
    }
}
