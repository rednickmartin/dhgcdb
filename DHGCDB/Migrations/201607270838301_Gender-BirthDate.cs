namespace DHGCDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenderBirthDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Gender", c => c.String());
            AddColumn("dbo.People", "BirthDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "BirthDate");
            DropColumn("dbo.People", "Gender");
        }
    }
}
