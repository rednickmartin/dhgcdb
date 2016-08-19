namespace DHGCDB.Migrations
{
  using Models;
  using System;
  using System.Data.Entity;
  using System.Data.Entity.Migrations;
  using System.Linq;

  internal sealed class Configuration : DbMigrationsConfiguration<DHGCDB.Models.PersonDBContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(DHGCDB.Models.PersonDBContext context)
    {
      //  This method will be called after migrating to the latest version.

      //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
      //  to avoid creating duplicate seed data. E.g.
      //
      context.People.AddOrUpdate(
        p => new { p.Title, p.FirstName, p.Surname },
        new Person { Title = "Mr", FirstName = "Nick", Surname = "Martin", Gender = "M", BirthDate = DateTime.Parse("1981-07-01") },
        new Person { Title = "Mrs", FirstName = "Natalie", Surname = "Martin", Gender = "F", BirthDate = DateTime.Parse("1983-12-17") },
        new Person { Title = "Mr", FirstName = "Dermot", Surname = "Griffin", Gender = "M", BirthDate = DateTime.Parse("1960-03-01") }
      );
    }
  }
}
