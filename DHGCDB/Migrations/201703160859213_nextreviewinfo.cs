namespace DHGCDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nextreviewinfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReviewFrequency",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ReportText = c.String(),
                        NumberOfYears = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Client", "ReviewFrequency_ID", c => c.Int());
            AddColumn("dbo.Review", "NextReviewDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ReviewType", "NewBusiness", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReviewtHowConducted", "ReportText", c => c.String());
            CreateIndex("dbo.Client", "ReviewFrequency_ID");
            AddForeignKey("dbo.Client", "ReviewFrequency_ID", "dbo.ReviewFrequency", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Client", "ReviewFrequency_ID", "dbo.ReviewFrequency");
            DropIndex("dbo.Client", new[] { "ReviewFrequency_ID" });
            DropColumn("dbo.ReviewtHowConducted", "ReportText");
            DropColumn("dbo.ReviewType", "NewBusiness");
            DropColumn("dbo.Review", "NextReviewDate");
            DropColumn("dbo.Client", "ReviewFrequency_ID");
            DropTable("dbo.ReviewFrequency");
        }
    }
}
