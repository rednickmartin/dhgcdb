using DHGCDB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace DHGCDB.DAL
{
  public class ClientDBContext : DbContext
  {
    public DbSet<Client> Clients { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Address> Adresses { get; set; }
    public DbSet<AttitudeToRisk> AttitudeToRiskSelections { get; set; }
    public DbSet<BusinessType> BusinessTypes { get; set; }
    public DbSet<ReviewtHowConducted> ReviewHowConducted { get; set; }
    public DbSet<ReviewType> ReviewTypes { get; set; }
    public DbSet<KIIDSGiven> KIIDSGivenTypes { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Fund> Funds { get; set; }
    public DbSet<FundSelection> FundSelections { get; set; }
    public DbSet<FundATRAllocation> FundATRAllocations { get; set; }
    public DbSet<PersonReview> PersonReviews { get; set; }
    public DbSet<Sector> Sectors { get; set; }
    public DbSet<SectorGrouping> SectorGroupings { get; set; }
    public DbSet<AttitudeToRiskCategory> AttitudeToRiskCategories { get; set; }
    public DbSet<PersonsAttitudeToRisk> PeoplesAttitudeToRisks { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductFee> ProductFees { get; set; }


    // Still to do:
    // Business Type Choice
    // Business Type Frequency
    // Fixed Fee
    // Fixed Fee Rate
    // Fund Investments atr allocations
    // Fund Selection
    // ISA Allowance
    // ISA Allowance Choice
    // Portfolio Aim
    // Portfolio Aim Type
    // Product fee selection
    // Provider
    // Provider Choice
    // Reason for business
    // Reason for business Choice
    // Review Frequency
    // Review Growth Client
    // Review Growth Person
    // Trail fee
    // Trail fee rate

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
    }
  }
}