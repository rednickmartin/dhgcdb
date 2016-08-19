using DHGCDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHGCDB.DAL
{
  public class ClientDBInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ClientDBContext>
  {
    protected override void Seed(ClientDBContext context)
    {
      var person1 = new Person { Title = "Mr", FirstName = "Nick", Surname = "Martin", Gender = "M", BirthDate = DateTime.Parse("1981-07-01"), IsPrimary = true };
      var person2 = new Person { Title = "Mrs", FirstName = "Natalie", Surname = "Martin", Gender = "F", BirthDate = DateTime.Parse("1983-12-17") };
      //var person3 = new Person { Title = "Mr", FirstName = "Dermot", Surname = "Griffin", Gender = "M", BirthDate = DateTime.Parse("1960-03-01") };

      var client1 = new Client { Name = "MARTIN" };
      client1.Persons.Add(person1);
      client1.Persons.Add(person2);

      person1.Client = client1;
      person2.Client = client1;

      var address1 = new Address { FirstLine = "435 Whippendell Road", Town = "Watford", County = "Hertfordshire", PostCode = "WD18 7PS" };
      client1.Address = address1;

      context.People.Add(person1);
      context.People.Add(person2);
      context.Clients.Add(client1);
      context.Adresses.Add(address1);

      var atr50 = new AttitudeToRisk { Name = "50" };
      var atr60 = new AttitudeToRisk { Name = "60" };
      var atr70 = new AttitudeToRisk { Name = "70" };
      var atr80 = new AttitudeToRisk { Name = "80" };
      var atr90 = new AttitudeToRisk { Name = "90" };
      var atr100 = new AttitudeToRisk { Name = "100" };

      context.AttitudeToRiskSelections.Add(atr50);
      context.AttitudeToRiskSelections.Add(atr60);
      context.AttitudeToRiskSelections.Add(atr70);
      context.AttitudeToRiskSelections.Add(atr80);
      context.AttitudeToRiskSelections.Add(atr90);
      context.AttitudeToRiskSelections.Add(atr100);

      context.BusinessTypes.Add(new BusinessType { Name = "ISA" });
      context.BusinessTypes.Add(new BusinessType { Name = "Pens" });
      context.BusinessTypes.Add(new BusinessType { Name = "Bond" });
      context.BusinessTypes.Add(new BusinessType { Name = "W/P Bond" });
      context.BusinessTypes.Add(new BusinessType { Name = "CIA" });
      context.BusinessTypes.Add(new BusinessType { Name = "GIA" });

      var conductedAtHome = new ReviewtHowConducted { Name = "Home" };
      var conductedAtWork = new ReviewtHowConducted { Name = "Work" };
      var conductedByTelephone = new ReviewtHowConducted { Name = "Telephone" };

      context.ReviewHowConducted.Add(conductedAtHome);
      context.ReviewHowConducted.Add(conductedAtWork);
      context.ReviewHowConducted.Add(conductedByTelephone);

      var revTypeExistingCustomerReview = new ReviewType { Name = "Existing customer - Review" };
      var revTypeExistingCustomerNewBusiness = new ReviewType { Name = "Existing customer - New Business" };
      var revTypeExistingCustomerBoth = new ReviewType { Name = "Existing customer - Both" };
      var revTypeNewCustomer = new ReviewType { Name = "New customer" };

      context.ReviewTypes.Add(revTypeExistingCustomerReview);
      context.ReviewTypes.Add(revTypeExistingCustomerNewBusiness);
      context.ReviewTypes.Add(revTypeExistingCustomerBoth);
      context.ReviewTypes.Add(revTypeNewCustomer);

      var KIIDSGivenGiven = new KIIDSGiven { Name = "Given" };
      var KIIDSGivenSent = new KIIDSGiven { Name = "Sent" };
      var KIIDSGivenBeingSent = new KIIDSGiven { Name = "Being sent" };

      context.KIIDSGivenTypes.Add(KIIDSGivenGiven);
      context.KIIDSGivenTypes.Add(KIIDSGivenSent);
      context.KIIDSGivenTypes.Add(KIIDSGivenBeingSent);

      var review1 = new Review {
        Client = client1,
        Name = "Existing customer - New Business - 31/07/2012",
        ReviewDate = DateTime.Parse("2012-07-31"),
        HowConducted = conductedAtHome,
        ValuationDate = DateTime.Parse("2012-07-20"),
        IsJoint = true,
        PortfolioSize = 10000,
        AnnualCharges = 100,
        KIIDSGiven = KIIDSGivenBeingSent,
        NumberOfFunds = 20,
        ReviewType = revTypeNewCustomer
      };

      var review2 = new Review {
        Client = client1,
        Name = "Existing customer - Review - 30/07/2014",
        ReviewDate = DateTime.Parse("2014-07-30"),
        HowConducted = conductedByTelephone,
        ValuationDate = DateTime.Parse("2014-07-20"),
        IsJoint = true,
        PortfolioSize = 13000,
        AnnualCharges = 100,
        KIIDSGiven = KIIDSGivenGiven,
        NumberOfFunds = 23,
        ReviewType = revTypeExistingCustomerReview
      };

      client1.Reviews.Add(review1);
      client1.Reviews.Add(review2);

      context.Reviews.Add(review1);
      context.Reviews.Add(review2);

      var fund1 = new Fund { Name = "Invesco Perpetual Corporate Bond", Description = "This is an OBSR GOLD rated fund managed by Paul Read and Paul Causer and is a Citywire Selected fund. Aim of Fund – to achieve a combination of income and capital growth over the medium to long term. The Fund seeks to achieve its objective by investing primarily in investment grade corporate debt securities. Recent Fund performance is – 1yr  5.21%." };
      var fund2 = new Fund { Name = "Royal London Corporate Bond", Description = "This is an OBSR GOLD rated fund managed by Jonathan Platt and Sajiv Vaid, both rated AAA by the Citywire survey of top fund managers over 3 years and was Winner in the Investment Week Awards 2013 for the Sterling Corporate Bond sector. Aim of Fund – to achieve a combination of mainly income with some capital growth over the medium (5 years) to long term (7 years). Recent Fund performance is – 1yr  4.97%." };
      var fund3 = new Fund { Name = "M&G Optimal Income", Description = "This is an OBSR SILVER rated fund managed by Richard Woolnough – rated A by the Citywire survey of top fund managers over 3 years and is a Citywire selected fund. Aim of Fund – to provide a total return to investors based on exposure to optimal income streams in investment markets. Recent Fund performance is – 1yr  7.08%, 3yr  29.5%, 5yrs  84.25%." };
      var fund4 = new Fund { Name = "Templeton Global Total Return Bond", Description = "This is an OBSR BRONZE rated fund managed by Michael Hasenstab and Sonal Desai – rated A by the Citywire survey of top fund managers over 3 years and was a Finalist in the Investment Week Awards 2013 for the Overseas Bonds sector. Aim of Fund – to achieve a total return, over the long term, from a combination of income, capital growth and currency gains. Recent Fund performance is – 1yr       -1.84 %, 3yr  18.69 %, 5yrs  55.58%" };

      var fund1Allocation1 = new FundATRAllocation { Fund = fund1, AttitudeToRisk = atr50, Percentage = 3 };
      var fund1Allocation2 = new FundATRAllocation { Fund = fund1, AttitudeToRisk = atr60, Percentage = 4 };
      var fund1Allocation3 = new FundATRAllocation { Fund = fund1, AttitudeToRisk = atr70, Percentage = 5 };

      fund1.Allocations.Add(fund1Allocation1);
      fund1.Allocations.Add(fund1Allocation2);
      fund1.Allocations.Add(fund1Allocation3);

      var fund2Allocation1 = new FundATRAllocation { Fund = fund2, AttitudeToRisk = atr60, Percentage = 6 };
      var fund2Allocation2 = new FundATRAllocation { Fund = fund2, AttitudeToRisk = atr70, Percentage = 7 };
      var fund2Allocation3 = new FundATRAllocation { Fund = fund2, AttitudeToRisk = atr80, Percentage = 8 };

      fund2.Allocations.Add(fund2Allocation1);
      fund2.Allocations.Add(fund2Allocation2);
      fund2.Allocations.Add(fund2Allocation3);

      var fund3Allocation1 = new FundATRAllocation { Fund = fund3, AttitudeToRisk = atr50, Percentage = 4 };
      var fund3Allocation2 = new FundATRAllocation { Fund = fund3, AttitudeToRisk = atr60, Percentage = 5 };
      var fund3Allocation3 = new FundATRAllocation { Fund = fund3, AttitudeToRisk = atr70, Percentage = 6 };

      fund3.Allocations.Add(fund3Allocation1);
      fund3.Allocations.Add(fund3Allocation2);
      fund3.Allocations.Add(fund3Allocation3);

      var fund4Allocation1 = new FundATRAllocation { Fund = fund4, AttitudeToRisk = atr80, Percentage = 7 };
      var fund4Allocation2 = new FundATRAllocation { Fund = fund4, AttitudeToRisk = atr90, Percentage = 8 };
       var fund4Allocation3 = new FundATRAllocation { Fund = fund4, AttitudeToRisk = atr100, Percentage = 9 };

      fund4.Allocations.Add(fund4Allocation1);
      fund4.Allocations.Add(fund4Allocation2);
      fund4.Allocations.Add(fund4Allocation3);

      var fundSelection1 = new FundSelection { Name = "Skandia ISA Funds Winter 2014", DateCreated = DateTime.Now };
      var fundSelection2 = new FundSelection { Name = "Alternative Winter 2014", DateCreated = DateTime.Now };

      fund1.FundSelection = fundSelection1;
      fund2.FundSelection = fundSelection1;

      fundSelection1.Funds.Add(fund1);
      fundSelection1.Funds.Add(fund2);

      fund3.FundSelection = fundSelection2;
      fund4.FundSelection = fundSelection2;

      fundSelection2.Funds.Add(fund3);
      fundSelection2.Funds.Add(fund4);

      context.Funds.Add(fund1);
      context.Funds.Add(fund2);
      context.Funds.Add(fund3);
      context.Funds.Add(fund4);

      context.FundATRAllocations.Add(fund1Allocation1);
      context.FundATRAllocations.Add(fund1Allocation2);
      context.FundATRAllocations.Add(fund1Allocation3);
      context.FundATRAllocations.Add(fund2Allocation1);
      context.FundATRAllocations.Add(fund2Allocation2);
      context.FundATRAllocations.Add(fund2Allocation3);
      context.FundATRAllocations.Add(fund3Allocation1);
      context.FundATRAllocations.Add(fund3Allocation2);
      context.FundATRAllocations.Add(fund3Allocation3);
      context.FundATRAllocations.Add(fund4Allocation1);
      context.FundATRAllocations.Add(fund4Allocation2);
      context.FundATRAllocations.Add(fund4Allocation3);

      context.FundSelections.Add(fundSelection1);
      context.FundSelections.Add(fundSelection2);

      PersonReview person1review1 = new PersonReview {
        Person = person1,
        Review = review1,
        InvestmentAttitudeToRisk = atr60,
        PensionAttitudeToRisk = atr70,
        InvestmentFundSelection = fundSelection1,
        PensionFundSelection = fundSelection2,
        ATRYear = 2015,
        ATRChanged = "Same",
        ATROutput = "Above"         
      };

      PersonReview person1review2 = new PersonReview {
        Person = person1,
        Review = review2,
        InvestmentAttitudeToRisk = atr70,
        PensionAttitudeToRisk = atr70,
        InvestmentFundSelection = fundSelection1,
        PensionFundSelection = fundSelection2,
        ATRYear = 2016,
        ATRChanged = "Up",
        ATROutput = "Below"
      };

      PersonReview person2review1 = new PersonReview {
        Person = person1,
        Review = review1,
        InvestmentAttitudeToRisk = atr50,
        PensionAttitudeToRisk = atr60,
        InvestmentFundSelection = fundSelection1,
        PensionFundSelection = fundSelection2,
        ATRYear = 2015,
        ATRChanged = "Same",
        ATROutput = "Above"
      };

      PersonReview person2review2 = new PersonReview {
        Person = person1,
        Review = review2,
        InvestmentAttitudeToRisk = atr50,
        PensionAttitudeToRisk = atr80,
        InvestmentFundSelection = fundSelection1,
        PensionFundSelection = fundSelection2,
        ATRYear = 2016,
        ATRChanged = "Up",
        ATROutput = "Below"
      };

      context.PersonReviews.Add(person1review1);
      context.PersonReviews.Add(person1review2);
      context.PersonReviews.Add(person2review1);
      context.PersonReviews.Add(person2review2);

      context.SaveChanges();
    }
  }
}