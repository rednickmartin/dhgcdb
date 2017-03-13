namespace DHGCDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstLine = c.String(),
                        SecondLine = c.String(),
                        Town = c.String(),
                        County = c.String(),
                        PostCode = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AttitudeToRiskCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AttitudeToRisk",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BusinessType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        HasAssetMix = c.Boolean(nullable: false),
                        ATRCategory_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AttitudeToRiskCategory", t => t.ATRCategory_ID)
                .Index(t => t.ATRCategory_ID);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Address", t => t.Address_ID)
                .Index(t => t.Address_ID);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        IsPrimary = c.Boolean(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Client_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Client", t => t.Client_ID)
                .Index(t => t.Client_ID);
            
            CreateTable(
                "dbo.PersonsAttitudeToRisk",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FromDate = c.DateTime(nullable: false),
                        AsPartOfReview_ID = c.Int(),
                        AttitudeToRisk_ID = c.Int(),
                        AttitudeToRiskCategory_ID = c.Int(),
                        Person_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Review", t => t.AsPartOfReview_ID)
                .ForeignKey("dbo.AttitudeToRisk", t => t.AttitudeToRisk_ID)
                .ForeignKey("dbo.AttitudeToRiskCategory", t => t.AttitudeToRiskCategory_ID)
                .ForeignKey("dbo.Person", t => t.Person_ID)
                .Index(t => t.AsPartOfReview_ID)
                .Index(t => t.AttitudeToRisk_ID)
                .Index(t => t.AttitudeToRiskCategory_ID)
                .Index(t => t.Person_ID);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ReviewDate = c.DateTime(nullable: false),
                        ValuationDate = c.DateTime(nullable: false),
                        IsJoint = c.Boolean(nullable: false),
                        PortfolioSize = c.Int(nullable: false),
                        AnnualCharges = c.Int(nullable: false),
                        NumberOfFunds = c.Int(nullable: false),
                        Client_ID = c.Int(),
                        HowConducted_ID = c.Int(),
                        KIIDSGiven_ID = c.Int(),
                        ReviewType_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Client", t => t.Client_ID)
                .ForeignKey("dbo.ReviewtHowConducted", t => t.HowConducted_ID)
                .ForeignKey("dbo.KIIDSGiven", t => t.KIIDSGiven_ID)
                .ForeignKey("dbo.ReviewType", t => t.ReviewType_ID)
                .Index(t => t.Client_ID)
                .Index(t => t.HowConducted_ID)
                .Index(t => t.KIIDSGiven_ID)
                .Index(t => t.ReviewType_ID);
            
            CreateTable(
                "dbo.ReviewtHowConducted",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.KIIDSGiven",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ReviewType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        ProductFeeAttached = c.Boolean(nullable: false),
                        AttitudeToRiskCategory_ID = c.Int(nullable: false),
                        BusinessType_ID = c.Int(nullable: false),
                        Client_ID = c.Int(),
                        Person_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AttitudeToRiskCategory", t => t.AttitudeToRiskCategory_ID, cascadeDelete: true)
                .ForeignKey("dbo.BusinessType", t => t.BusinessType_ID, cascadeDelete: true)
                .ForeignKey("dbo.Client", t => t.Client_ID)
                .ForeignKey("dbo.Person", t => t.Person_ID)
                .Index(t => t.AttitudeToRiskCategory_ID)
                .Index(t => t.BusinessType_ID)
                .Index(t => t.Client_ID)
                .Index(t => t.Person_ID);
            
            CreateTable(
                "dbo.ProductFee",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Percentage = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.ProductValuation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Value = c.Single(nullable: false),
                        AsPartOfReview_ID = c.Int(),
                        AssetMix_ID = c.Int(),
                        Product_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Review", t => t.AsPartOfReview_ID)
                .ForeignKey("dbo.FundSelection", t => t.AssetMix_ID)
                .ForeignKey("dbo.Product", t => t.Product_ID)
                .Index(t => t.AsPartOfReview_ID)
                .Index(t => t.AssetMix_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.FundSelection",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Fund",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        FundSelection_ID = c.Int(),
                        Sector_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FundSelection", t => t.FundSelection_ID)
                .ForeignKey("dbo.Sector", t => t.Sector_ID)
                .Index(t => t.FundSelection_ID)
                .Index(t => t.Sector_ID);
            
            CreateTable(
                "dbo.FundATRAllocation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Percentage = c.Int(nullable: false),
                        AttitudeToRisk_ID = c.Int(),
                        Fund_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AttitudeToRisk", t => t.AttitudeToRisk_ID)
                .ForeignKey("dbo.Fund", t => t.Fund_ID)
                .Index(t => t.AttitudeToRisk_ID)
                .Index(t => t.Fund_ID);
            
            CreateTable(
                "dbo.Sector",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SectorGrouping_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SectorGrouping", t => t.SectorGrouping_ID)
                .Index(t => t.SectorGrouping_ID);
            
            CreateTable(
                "dbo.SectorGrouping",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PersonReview",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ATROutput = c.String(),
                        IsATRChanging = c.Boolean(nullable: false),
                        AttitudeToRisk_ID = c.Int(),
                        Person_ID = c.Int(),
                        Review_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PersonsAttitudeToRisk", t => t.AttitudeToRisk_ID)
                .ForeignKey("dbo.Person", t => t.Person_ID)
                .ForeignKey("dbo.Review", t => t.Review_ID)
                .Index(t => t.AttitudeToRisk_ID)
                .Index(t => t.Person_ID)
                .Index(t => t.Review_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonReview", "Review_ID", "dbo.Review");
            DropForeignKey("dbo.PersonReview", "Person_ID", "dbo.Person");
            DropForeignKey("dbo.PersonReview", "AttitudeToRisk_ID", "dbo.PersonsAttitudeToRisk");
            DropForeignKey("dbo.ProductValuation", "Product_ID", "dbo.Product");
            DropForeignKey("dbo.ProductValuation", "AssetMix_ID", "dbo.FundSelection");
            DropForeignKey("dbo.Fund", "Sector_ID", "dbo.Sector");
            DropForeignKey("dbo.Sector", "SectorGrouping_ID", "dbo.SectorGrouping");
            DropForeignKey("dbo.Fund", "FundSelection_ID", "dbo.FundSelection");
            DropForeignKey("dbo.FundATRAllocation", "Fund_ID", "dbo.Fund");
            DropForeignKey("dbo.FundATRAllocation", "AttitudeToRisk_ID", "dbo.AttitudeToRisk");
            DropForeignKey("dbo.ProductValuation", "AsPartOfReview_ID", "dbo.Review");
            DropForeignKey("dbo.ProductFee", "ID", "dbo.Product");
            DropForeignKey("dbo.Product", "Person_ID", "dbo.Person");
            DropForeignKey("dbo.Product", "Client_ID", "dbo.Client");
            DropForeignKey("dbo.Product", "BusinessType_ID", "dbo.BusinessType");
            DropForeignKey("dbo.Product", "AttitudeToRiskCategory_ID", "dbo.AttitudeToRiskCategory");
            DropForeignKey("dbo.Person", "Client_ID", "dbo.Client");
            DropForeignKey("dbo.PersonsAttitudeToRisk", "Person_ID", "dbo.Person");
            DropForeignKey("dbo.PersonsAttitudeToRisk", "AttitudeToRiskCategory_ID", "dbo.AttitudeToRiskCategory");
            DropForeignKey("dbo.PersonsAttitudeToRisk", "AttitudeToRisk_ID", "dbo.AttitudeToRisk");
            DropForeignKey("dbo.PersonsAttitudeToRisk", "AsPartOfReview_ID", "dbo.Review");
            DropForeignKey("dbo.Review", "ReviewType_ID", "dbo.ReviewType");
            DropForeignKey("dbo.Review", "KIIDSGiven_ID", "dbo.KIIDSGiven");
            DropForeignKey("dbo.Review", "HowConducted_ID", "dbo.ReviewtHowConducted");
            DropForeignKey("dbo.Review", "Client_ID", "dbo.Client");
            DropForeignKey("dbo.Client", "Address_ID", "dbo.Address");
            DropForeignKey("dbo.BusinessType", "ATRCategory_ID", "dbo.AttitudeToRiskCategory");
            DropIndex("dbo.PersonReview", new[] { "Review_ID" });
            DropIndex("dbo.PersonReview", new[] { "Person_ID" });
            DropIndex("dbo.PersonReview", new[] { "AttitudeToRisk_ID" });
            DropIndex("dbo.Sector", new[] { "SectorGrouping_ID" });
            DropIndex("dbo.FundATRAllocation", new[] { "Fund_ID" });
            DropIndex("dbo.FundATRAllocation", new[] { "AttitudeToRisk_ID" });
            DropIndex("dbo.Fund", new[] { "Sector_ID" });
            DropIndex("dbo.Fund", new[] { "FundSelection_ID" });
            DropIndex("dbo.ProductValuation", new[] { "Product_ID" });
            DropIndex("dbo.ProductValuation", new[] { "AssetMix_ID" });
            DropIndex("dbo.ProductValuation", new[] { "AsPartOfReview_ID" });
            DropIndex("dbo.ProductFee", new[] { "ID" });
            DropIndex("dbo.Product", new[] { "Person_ID" });
            DropIndex("dbo.Product", new[] { "Client_ID" });
            DropIndex("dbo.Product", new[] { "BusinessType_ID" });
            DropIndex("dbo.Product", new[] { "AttitudeToRiskCategory_ID" });
            DropIndex("dbo.Review", new[] { "ReviewType_ID" });
            DropIndex("dbo.Review", new[] { "KIIDSGiven_ID" });
            DropIndex("dbo.Review", new[] { "HowConducted_ID" });
            DropIndex("dbo.Review", new[] { "Client_ID" });
            DropIndex("dbo.PersonsAttitudeToRisk", new[] { "Person_ID" });
            DropIndex("dbo.PersonsAttitudeToRisk", new[] { "AttitudeToRiskCategory_ID" });
            DropIndex("dbo.PersonsAttitudeToRisk", new[] { "AttitudeToRisk_ID" });
            DropIndex("dbo.PersonsAttitudeToRisk", new[] { "AsPartOfReview_ID" });
            DropIndex("dbo.Person", new[] { "Client_ID" });
            DropIndex("dbo.Client", new[] { "Address_ID" });
            DropIndex("dbo.BusinessType", new[] { "ATRCategory_ID" });
            DropTable("dbo.PersonReview");
            DropTable("dbo.SectorGrouping");
            DropTable("dbo.Sector");
            DropTable("dbo.FundATRAllocation");
            DropTable("dbo.Fund");
            DropTable("dbo.FundSelection");
            DropTable("dbo.ProductValuation");
            DropTable("dbo.ProductFee");
            DropTable("dbo.Product");
            DropTable("dbo.ReviewType");
            DropTable("dbo.KIIDSGiven");
            DropTable("dbo.ReviewtHowConducted");
            DropTable("dbo.Review");
            DropTable("dbo.PersonsAttitudeToRisk");
            DropTable("dbo.Person");
            DropTable("dbo.Client");
            DropTable("dbo.BusinessType");
            DropTable("dbo.AttitudeToRisk");
            DropTable("dbo.AttitudeToRiskCategory");
            DropTable("dbo.Address");
        }
    }
}
