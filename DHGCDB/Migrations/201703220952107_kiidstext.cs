namespace DHGCDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kiidstext : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KIIDSGiven", "ReportText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.KIIDSGiven", "ReportText");
        }
    }
}
