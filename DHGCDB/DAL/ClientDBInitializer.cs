using DHGCDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHGCDB.DAL
{
  public class ClientDBInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ClientDBContext>
  {
    private void AddFund(ClientDBContext context,
      FundSelection fundSelection,
      String name,
      Sector sector,
      AttitudeToRisk atr50,
      AttitudeToRisk atr60,
      AttitudeToRisk atr70,
      AttitudeToRisk atr80,
      int atr50Perc,
      int atr60Perc,
      int atr70Perc,
      int atr80Perc,
      string description
      )
    {
      var fund = new Fund { FundSelection = fundSelection, Sector = sector, Name = name, Description = description };
      fundSelection.Funds.Add(fund);
      var fundAlloc50 = new FundATRAllocation { Fund = fund, AttitudeToRisk = atr50, Percentage = atr50Perc };
      var fundAlloc60 = new FundATRAllocation { Fund = fund, AttitudeToRisk = atr60, Percentage = atr60Perc };
      var fundAlloc70 = new FundATRAllocation { Fund = fund, AttitudeToRisk = atr70, Percentage = atr70Perc };
      var fundAlloc80 = new FundATRAllocation { Fund = fund, AttitudeToRisk = atr80, Percentage = atr80Perc };
      fund.Allocations.Add(fundAlloc50);
      fund.Allocations.Add(fundAlloc60);
      fund.Allocations.Add(fundAlloc70);
      fund.Allocations.Add(fundAlloc80);

      context.Funds.Add(fund);
      context.FundATRAllocations.Add(fundAlloc50);
      context.FundATRAllocations.Add(fundAlloc60);
      context.FundATRAllocations.Add(fundAlloc70);
      context.FundATRAllocations.Add(fundAlloc80);
    }


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

      var atrCatInvestment = new AttitudeToRiskCategory { Name = "Investment" };
      var atrCatPension = new AttitudeToRiskCategory { Name = "Pension" };

      context.AttitudeToRiskCategories.Add(atrCatInvestment);
      context.AttitudeToRiskCategories.Add(atrCatPension);

      person1.AttitudeToRiskHistory.Add(new PersonsAttitudeToRisk {
        FromDate = DateTime.Parse("2012-07-31"),
        AttitudeToRiskCategory = atrCatInvestment,
        AttitudeToRisk = atr60,
        Person = person1
      });

      person1.AttitudeToRiskHistory.Add(new PersonsAttitudeToRisk {
        FromDate = DateTime.Parse("2012-07-31"),
        AttitudeToRiskCategory = atrCatPension,
        AttitudeToRisk = atr70,
        Person = person1
      });

      person1.AttitudeToRiskHistory.Add(new PersonsAttitudeToRisk {
        FromDate = DateTime.Parse("2014-07-20"),
        AttitudeToRiskCategory = atrCatInvestment,
        AttitudeToRisk = atr70,
        Person = person1
      });

      person2.AttitudeToRiskHistory.Add(new PersonsAttitudeToRisk {
        FromDate = DateTime.Parse("2012-07-31"),
        AttitudeToRiskCategory = atrCatInvestment,
        AttitudeToRisk = atr50,
        Person = person2
      });

      person2.AttitudeToRiskHistory.Add(new PersonsAttitudeToRisk {
        FromDate = DateTime.Parse("2012-07-31"),
        AttitudeToRiskCategory = atrCatPension,
        AttitudeToRisk = atr60,
        Person = person2
      });

      person2.AttitudeToRiskHistory.Add(new PersonsAttitudeToRisk {
        FromDate = DateTime.Parse("2014-07-20"),
        AttitudeToRiskCategory = atrCatPension,
        AttitudeToRisk = atr80,
        Person = person2
      });


      var businessTypeCIA = new BusinessType { Name = "CIA" };
      var businessTypePension = new BusinessType { Name = "Pension" };
      var businessTypeISA = new BusinessType { Name = "ISA" };
      var businessTypeBond = new BusinessType { Name = "Bond" };

      context.BusinessTypes.Add(businessTypeISA);
      context.BusinessTypes.Add(businessTypePension);
      context.BusinessTypes.Add(businessTypeBond);
      context.BusinessTypes.Add(new BusinessType { Name = "W/P Bond" });
      context.BusinessTypes.Add(businessTypeCIA);
      context.BusinessTypes.Add(new BusinessType { Name = "GIA" });

      var jointInvestmentClient1 = new Product { BusinessType = businessTypeCIA, Client = client1, Name = "Combined Investment", StartDate = DateTime.Parse("2012-07-31"), AttitudeToRiskCategory = atrCatInvestment };
      context.Products.Add(jointInvestmentClient1);
      client1.Products.Add(jointInvestmentClient1);


      var person1Pension = new Product { BusinessType = businessTypePension, Client = client1, Person = person1, Name = "Nicks Pension", StartDate = DateTime.Parse("2012-07-31"), AttitudeToRiskCategory = atrCatPension };
      context.Products.Add(person1Pension);
      person1.PersonProducts.Add(person1Pension);

      var person1PensionProductFee = new ProductFee { Product = person1Pension, Percentage = 0.7f };
      person1Pension.ProductFeeAttached = true;
      person1Pension.ProductFee = person1PensionProductFee;
      context.ProductFees.Add(person1PensionProductFee);

      var person1ISA = new Product { BusinessType = businessTypeISA, Client = client1, Person = person1, Name = "Nicks ISA", StartDate = DateTime.Parse("2012-07-31"), AttitudeToRiskCategory = atrCatInvestment };
      context.Products.Add(person1ISA);
      person1.PersonProducts.Add(person1ISA);

      var person2Bond = new Product { BusinessType = businessTypeBond, Client = client1, Person = person2, Name = "Natalie's Bond", StartDate = DateTime.Parse("2012-07-31"), AttitudeToRiskCategory = atrCatInvestment };
      context.Products.Add(person2Bond);
      person2.PersonProducts.Add(person2Bond);


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

      var ukGlobalBonds = new SectorGrouping { Name = "UK & Global Bonds" };
      var property = new SectorGrouping { Name = "Property" };
      var ukEquity = new SectorGrouping { Name = "UK Equity" };
      var globalEquityGroup = new SectorGrouping { Name = "Global Equity" };

      var ukBond = new Sector { Name = "UK Bond" };
      var globalBond = new Sector { Name = "Global Bond" };
      var ukRealProperty = new Sector { Name = "UK Real Property" };
      var globalProperty = new Sector { Name = "Global Property" };
      var ukEquityIncome = new Sector { Name = "UK Equity (Income)" };
      var ukEquityGrowth = new Sector { Name = "UK Equity (Growth)" };
      var ukSpecialist = new Sector { Name = "UK Specialist" };
      var globalEquity = new Sector { Name = "Global Equity" };
      var globalSpecialist = new Sector { Name = "Global Specialist" };

      ukBond.SectorGrouping = ukGlobalBonds;
      globalBond.SectorGrouping = ukGlobalBonds;
      ukGlobalBonds.Sectors.Add(ukBond);
      ukGlobalBonds.Sectors.Add(globalBond);

      ukRealProperty.SectorGrouping = property;
      globalProperty.SectorGrouping = property;
      property.Sectors.Add(ukRealProperty);
      property.Sectors.Add(globalProperty);

      ukEquityIncome.SectorGrouping = ukEquity;
      ukEquityGrowth.SectorGrouping = ukEquity;
      ukSpecialist.SectorGrouping = ukEquity;
      ukEquity.Sectors.Add(ukEquityIncome);
      ukEquity.Sectors.Add(ukEquityGrowth);
      ukEquity.Sectors.Add(ukSpecialist);

      globalEquity.SectorGrouping = globalEquityGroup;
      globalSpecialist.SectorGrouping = globalEquityGroup;
      globalEquityGroup.Sectors.Add(globalEquity);
      globalEquityGroup.Sectors.Add(globalSpecialist);

      context.SectorGroupings.Add(ukGlobalBonds);
      context.SectorGroupings.Add(property);
      context.SectorGroupings.Add(ukEquity);
      context.SectorGroupings.Add(globalEquityGroup);

      context.Sectors.Add(ukBond);
      context.Sectors.Add(globalBond);
      context.Sectors.Add(ukRealProperty);
      context.Sectors.Add(globalProperty);
      context.Sectors.Add(ukEquityIncome);
      context.Sectors.Add(ukEquityGrowth);
      context.Sectors.Add(ukSpecialist);
      context.Sectors.Add(globalEquity);
      context.Sectors.Add(globalSpecialist);


      var fund1 = new Fund { Name = "Invesco Perpetual Corporate Bond", Sector = ukBond, Description = "This is an OBSR GOLD rated fund managed by Paul Read and Paul Causer and is a Citywire Selected fund. Aim of Fund – to achieve a combination of income and capital growth over the medium to long term. The Fund seeks to achieve its objective by investing primarily in investment grade corporate debt securities. Recent Fund performance is – 1yr  5.21%." };
      var fund2 = new Fund { Name = "Royal London Corporate Bond", Sector = ukBond, Description = "This is an OBSR GOLD rated fund managed by Jonathan Platt and Sajiv Vaid, both rated AAA by the Citywire survey of top fund managers over 3 years and was Winner in the Investment Week Awards 2013 for the Sterling Corporate Bond sector. Aim of Fund – to achieve a combination of mainly income with some capital growth over the medium (5 years) to long term (7 years). Recent Fund performance is – 1yr  4.97%." };
      var fund3 = new Fund { Name = "M&G Optimal Income", Sector = ukBond, Description = "This is an OBSR SILVER rated fund managed by Richard Woolnough – rated A by the Citywire survey of top fund managers over 3 years and is a Citywire selected fund. Aim of Fund – to provide a total return to investors based on exposure to optimal income streams in investment markets. Recent Fund performance is – 1yr  7.08%, 3yr  29.5%, 5yrs  84.25%." };
      var fund4 = new Fund { Name = "Templeton Global Total Return Bond", Sector = globalBond, Description = "This is an OBSR BRONZE rated fund managed by Michael Hasenstab and Sonal Desai – rated A by the Citywire survey of top fund managers over 3 years and was a Finalist in the Investment Week Awards 2013 for the Overseas Bonds sector. Aim of Fund – to achieve a total return, over the long term, from a combination of income, capital growth and currency gains. Recent Fund performance is – 1yr       -1.84 %, 3yr  18.69 %, 5yrs  55.58%" };

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

      var fundSelection1 = new FundSelection { Name = "Skandia ISA Funds Winter 2014", DateCreated = DateTime.Today, IncludedInInvestmentFundSelections = true, IncludedInPensionFundSelections = true };
      var fundSelection2 = new FundSelection { Name = "Alternative Winter 2014", DateCreated = DateTime.Today, IncludedInInvestmentFundSelections = true, IncludedInPensionFundSelections = true };

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


      // Jan2014
      var skandiaInvestmentBond = new FundSelection { Name = "Skandia ISA Account Jarnuary 2014", DateCreated = DateTime.Today, IncludedInInvestmentFundSelections = true };
      context.FundSelections.Add(skandiaInvestmentBond);
      AddFund(context, skandiaInvestmentBond, "Invesco Perpetual Corporate Bond", ukBond, atr50, atr60, atr70, atr80, 3, 3, 2, 1, "This is an OBSR GOLD rated fund managed by Paul Read and Paul Causer and is a Citywire Selected fund. Aim of Fund – to achieve a high level of overall return, with relative security of capital. It intends to invest primarily in fixed interest securities. Recent Fund performance is – 1yr  14.15 %, 3yr  18.69 %, 5yr  34.44%.");
      AddFund(context, skandiaInvestmentBond, "Royal London Corporate Bond", ukBond, atr50, atr60, atr70, atr80, 3, 2, 1, 1, "This is an OBSR GOLD rated fund managed by Jonathan Platt and Sajiv Vaidand, both rated A by the Citywire survey of top fund managers over 3 years and was Shortlisted in the Investment Week Awards 2012 for the Sterling Corporate Bond sector. Aim of Fund – to achieve a combination of mainly income with some capital growth over the medium (5 years) to long term (7 years). Recent Fund performance is – 1yr  12.65%, 3yr  30.68%, 5yrs  34.07%.");
      AddFund(context, skandiaInvestmentBond, "M&G Optimal Income", ukBond, atr50, atr60, atr70, atr80, 8, 7, 5, 2, "This is an OBSR SILVER rated fund managed by Richard Woolnough, is a Citywire Selected fund and was Shortlisted in the Investment Week Awards 2012 for the Strategic Bond sector. Aim of Fund – to provide a total return to investors based on exposure to optimal income streams in investment markets. Recent Fund performance is – 1yr  10.75%, 3yr  28.19%, 5yrs  66.28%.");
      AddFund(context, skandiaInvestmentBond, "Invesco Perpetual Monthly Income Plus", ukBond, atr50, atr60, atr70, atr80, 6, 5, 4, 3, "");

      AddFund(context, skandiaInvestmentBond, "Invesco Perpetual Global Bond", globalBond, atr50, atr60, atr70, atr80, 6, 4, 3, 2, "This is an OBSR BRONZE rated fund managed by Michael Matthews and Stuart Edwards. Aim of Fund – to achieve a good overall investment return in the medium to long term with relative security of capital. The underlying fund intends to invest primarily in international bonds of differing yields and maturities. Recent Fund performance is – 1yr  6.93%, 3yr  18.44%, 5yrs  48.53%.");
      AddFund(context, skandiaInvestmentBond, "Templeton Global Total Return Bond", globalBond, atr50, atr60, atr70, atr80, 4, 3, 1, 1, "This is an OBSR BRONZE rated fund managed by Michael Hasenstab and Sonal Desai. Aim of Fund – to achieve a total return, over the long term, from a combination of income, capital growth and currency gains. Recent Fund performance is – 1yr  12.23%, 3yr  33.61%.");

      AddFund(context, skandiaInvestmentBond, "Henderson UK Property", ukRealProperty, atr50, atr60, atr70, atr80, 10, 10, 8, 6, "This fund is managed by Marcus Langlands Pearse and Ainslie McLennan. Aim of Fund – to achieve a high income together with some growth of both income and capital through investment primarily in commercial property and property-related assets. Recent Fund performance is – 1yr  5.79%, 3yr  14.20%, 5yrs  -5.63%.");
      AddFund(context, skandiaInvestmentBond, "Aviva Property Trust", ukRealProperty, atr50, atr60, atr70, atr80, 10, 6, 6, 4, "");
      AddFund(context, skandiaInvestmentBond, "Aberdeen Propery Share", ukRealProperty, atr50, atr60, atr70, atr80, 3, 3, 4, 6, "");

      AddFund(context, skandiaInvestmentBond, "Invesco Perpetual High Income", ukEquityIncome, atr50, atr60, atr70, atr80, 5, 6, 6, 5, "This is an OBSR GOLD rated fund managed by Neil Woodford- rated A by the Citywire survey of top fund managers over 3 years and is a Citywire selected fund. Aim of Fund – to achieve a high level of income, together with capital growth. The Fund intends to invest primarily in companies listed in the UK, with the balance invested internationally. Recent Fund performance is – 1yr  14.53%, 3yr  38.29%, 5yrs  35.31%.");
      AddFund(context, skandiaInvestmentBond, "Royal London UK Equity Income", ukEquityIncome, atr50, atr60, atr70, atr80, 5, 4, 5, 5, "");

      AddFund(context, skandiaInvestmentBond, "Cazenove UK Opportunities", ukEquityGrowth, atr50, atr60, atr70, atr80, 8, 10, 12, 15, "This is an OBSR BRONZE rated fund managed by Steve Cordell and Julie Dean - rated AAA by the Citywire survey of top fund managers over 3 years and was Shortlisted in the Investment Week Awards 2012 for the UK Growth sector. Aim of Fund – To achieve an income return, together with long term capital growth, by investing in any economic sector of the UK market. Recent Fund performance is – 1yr   25.69%.");
      AddFund(context, skandiaInvestmentBond, "AXA Framlington UK Select Opps", ukEquityGrowth, atr50, atr60, atr70, atr80, 6, 8, 9, 12, "This is an OBSR GOLD rated fund managed by Nigel Thomas- rated AAA by the Citywire survey of top fund managers over 3 years, is a Citywire Star Pick fund and was Highly Commended in the Investment Week Awards 2012 for the UK Growth sector. Aim of Fund – to achieve capital growth by investing in companies, primarily of UK origin, where the Manager believes above average returns can be realised. Recent Fund performance is – 1yr  9.71%, 3yr  39.50%, 5yrs  42.52%.");

      AddFund(context, skandiaInvestmentBond, "Cazenove UK Smaller Companies [CLOSED TO N/B]", ukSpecialist, atr50, atr60, atr70, atr80, 1, 1, 2, 1, "This is an OBSR BRONZE rated fund managed by John Warren and Paul Marriage - rated AAA by the Citywire survey of top fund managers over 3 years, is a Citywire selected fund and was Winner of the Investment Week Awards 2012 for the UK Smaller Companies sector. Aim of Fund – To achieve long term capital growth by investing primarily in UK smaller companies. The Fund will invest at least 80 per cent of its assets in the UK listed companies that form the bottom 10 percent by market capitalisation. The Fund may also invest in companies which are headquartered or have significant activities in the UK which are quoted on a stock exchange outside the UK. Recent Fund performance is – 1yr   34.23%, 3yr  90.98%, 5yrs  98.14%.");
      AddFund(context, skandiaInvestmentBond, "Liontrust UK Smaller Companies", ukSpecialist, atr50, atr60, atr70, atr80, 2, 2, 2, 3, "This is an OBSR BRONZE rated fund managed by Julian Fosh and Anthony Cross – both rated AAA by the Citywire survey of top fund managers over 3 years. Aim of Fund – to provide long-term capital growth by investing primarily in smaller UK companies displaying a high degree of Intellectual Capital and employee motivation through equity ownership in their business model. Recent Fund performance is – 1yr  22.53%, 3yr  54.03%, 5yrs  75.07%.");

      AddFund(context, skandiaInvestmentBond, "Artemis Global Growth", globalEquity, atr50, atr60, atr70, atr80, 7, 7, 8, 8, "");
      AddFund(context, skandiaInvestmentBond, "M&G Global Dividend", globalEquity, atr50, atr60, atr70, atr80, 5, 6, 7, 7, "This is an OBSR SILVER rated fund managed by Stuart Rhodes - rated A by the Citywire survey of top fund managers over 3 years and was Winner of the Investment Week Awards 2012 for the Global Equity & Income sector. Aim of Fund – to deliver a dividend yield above the market average, by investing mainly in a range of global equities. The Fund aims to grow distributions over the long term whilst also maximising total return (the combination of income and growth of capital). Recent Fund performance is – 1yr  12.06%, 3yr  34.30%, 4yrs  65.71%.");
      AddFund(context, skandiaInvestmentBond, "Invesco Perpetual Global Equity Income", globalEquity, atr50, atr60, atr70, atr80, 3, 5, 4, 5, "This fund is managed by Doug McGraw and Paul Boyne - rated AA by the Citywire survey of top fund managers over 3 years. Aim of Fund – to generate a rising level of income, together with long-term capital growth, investing primarily in global equities. Recent Fund performance is – 1yr  12.99%, 3yr  36.82%.");
      AddFund(context, skandiaInvestmentBond, "Newton Global Higher Income", globalEquity, atr50, atr60, atr70, atr80, 2, 2, 3, 3, "This is an OBSR SILVER rated fund managed by James Harries and Nick Clay and is a Citywire selected fund. Aim of Fund –to achieve increasing income and capital growth over the long term by investing in shares (i.e. equities) and similar investments of companies listed or located throughout the world. Recent Fund performance is – 1yr  13.27%, 3yr  32.98%, 5yrs  38.68%.");

      AddFund(context, skandiaInvestmentBond, "Old Mutual North American Equity", globalSpecialist, atr50, atr60, atr70, atr80, 2, 3, 3, 4, "");
      AddFund(context, skandiaInvestmentBond, "Jupiter European", globalSpecialist, atr50, atr60, atr70, atr80, 1, 1, 2, 2, "");
      AddFund(context, skandiaInvestmentBond, "First State Global Listed Infrastructure", globalSpecialist, atr50, atr60, atr70, atr80, 2, 2, 3, 4, "This is an OBSR SILVER rated fund managed by Andrew Greenup and Peter Meany, rated A by the Citywire survey of top fund managers over 3 years and is a Citywire Star Pick fund. Aim of Fund – to provide income and grow your investment. The Fund invests in shares of companies that are involved in infrastructure around the world. The infrastructure sector includes utilities (e.g. water and electricity), highways and railways, airports services, marine ports and services, and oil and gas storage and transportation. The Fund does not invest directly in infrastructure assets. Recent Fund performance is – 1yr  9.26%, 3yr  24.69%.");

      PersonReview person1review1 = new PersonReview {
        Person = person1,
        Review = review1,
        InvestmentFundSelection = fundSelection1,
        PensionFundSelection = fundSelection2,
        ATRYear = 2015,
        ATRChanged = "Same",
        ATROutput = "Above"         
      };

      PersonReview person1review2 = new PersonReview {
        Person = person1,
        Review = review2,
        InvestmentFundSelection = fundSelection1,
        PensionFundSelection = fundSelection2,
        ATRYear = 2016,
        ATRChanged = "Up",
        ATROutput = "Below"
      };

      PersonReview person2review1 = new PersonReview {
        Person = person1,
        Review = review1,
        InvestmentFundSelection = fundSelection1,
        PensionFundSelection = fundSelection2,
        ATRYear = 2015,
        ATRChanged = "Same",
        ATROutput = "Above"
      };

      PersonReview person2review2 = new PersonReview {
        Person = person1,
        Review = review2,
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