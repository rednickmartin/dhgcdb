namespace DHGCDB.Migrations
{
  using System;
  using System.Data.Entity;
  using System.Data.Entity.Migrations;
  using System.Linq;
  using DHGCDB.DAL;
  using DHGCDB.Models;

  internal sealed class Configuration : DbMigrationsConfiguration<DHGCDB.DAL.ClientDBContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

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

    protected override void Seed(DHGCDB.DAL.ClientDBContext context)
    {
      var atr50 = new AttitudeToRisk { Name = "50" };
      var atr60 = new AttitudeToRisk { Name = "60" };
      var atr70 = new AttitudeToRisk { Name = "70" };
      var atr80 = new AttitudeToRisk { Name = "80" };
      var atr90 = new AttitudeToRisk { Name = "90" };
      var atr100 = new AttitudeToRisk { Name = "100" };

      context.AttitudeToRiskSelections.AddOrUpdate(x => x.Name, atr50);
      context.AttitudeToRiskSelections.AddOrUpdate(x => x.Name, atr60);
      context.AttitudeToRiskSelections.AddOrUpdate(x => x.Name, atr70);
      context.AttitudeToRiskSelections.AddOrUpdate(x => x.Name, atr80);
      context.AttitudeToRiskSelections.AddOrUpdate(x => x.Name, atr90);
      context.AttitudeToRiskSelections.AddOrUpdate(x => x.Name, atr100);

      var atrCatInvestment = new AttitudeToRiskCategory { Name = "Investment" };
      var atrCatPension = new AttitudeToRiskCategory { Name = "Pension" };

      context.AttitudeToRiskCategories.AddOrUpdate(x => x.Name, atrCatInvestment);
      context.AttitudeToRiskCategories.AddOrUpdate(x => x.Name, atrCatPension);

      var businessTypeCIA = new BusinessType { Name = "CIA", HasAssetMix = true, ATRCategory = atrCatInvestment };
      var businessTypePension = new BusinessType { Name = "Pension", HasAssetMix = true, ATRCategory = atrCatPension };
      var businessTypeISA = new BusinessType { Name = "ISA", HasAssetMix = true, ATRCategory = atrCatInvestment };
      var businessTypeBond = new BusinessType { Name = "Bond", HasAssetMix = true, ATRCategory = atrCatInvestment };
      var businessTypeWPBond = new BusinessType { Name = "W/P Bond", HasAssetMix = false, ATRCategory = atrCatInvestment };
      var businessTypeGIA = new BusinessType { Name = "GIA", HasAssetMix = true, ATRCategory = atrCatInvestment };


      context.BusinessTypes.AddOrUpdate(x => x.Name, businessTypeISA);
      context.BusinessTypes.AddOrUpdate(x => x.Name, businessTypePension);
      context.BusinessTypes.AddOrUpdate(x => x.Name, businessTypeBond);
      context.BusinessTypes.AddOrUpdate(x => x.Name, businessTypeWPBond);
      context.BusinessTypes.AddOrUpdate(x => x.Name, businessTypeCIA);
      context.BusinessTypes.AddOrUpdate(x => x.Name, businessTypeGIA);


      var revTypeExistingCustomerReview = new ReviewType { Name = "Existing customer - Review", NewBusiness = false };
      var revTypeExistingCustomerNewBusiness = new ReviewType { Name = "Existing customer - New Business", NewBusiness = true };
      var revTypeExistingCustomerBoth = new ReviewType { Name = "Existing customer - Both", NewBusiness = true };
      var revTypeNewCustomer = new ReviewType { Name = "New customer", NewBusiness = true };

      context.ReviewTypes.AddOrUpdate(x => x.Name, revTypeExistingCustomerReview);
      context.ReviewTypes.AddOrUpdate(x => x.Name, revTypeExistingCustomerNewBusiness);
      context.ReviewTypes.AddOrUpdate(x => x.Name, revTypeExistingCustomerBoth);
      context.ReviewTypes.AddOrUpdate(x => x.Name, revTypeNewCustomer);

      var reviewFrequencyAnnual = new ReviewFrequency { Name = "Annual", ReportText = "an annual", NumberOfYears = 1 };
      context.ReviewFrequencies.AddOrUpdate(x => x.Name, reviewFrequencyAnnual);

      var KIIDSGivenGiven = new KIIDSGiven { Name = "Given", ReportText = "have been provided to you at our meeting" };
      var KIIDSGivenSent = new KIIDSGiven { Name = "Sent", ReportText = "have been sent" };
      var KIIDSGivenBeingSent = new KIIDSGiven { Name = "Being sent", ReportText = "are being sent" };

      context.KIIDSGivenTypes.AddOrUpdate(x => x.Name, KIIDSGivenGiven);
      context.KIIDSGivenTypes.AddOrUpdate(x => x.Name, KIIDSGivenSent);
      context.KIIDSGivenTypes.AddOrUpdate(x => x.Name, KIIDSGivenBeingSent);

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

      context.SectorGroupings.AddOrUpdate(x => x.Name, ukGlobalBonds);
      context.SectorGroupings.AddOrUpdate(x => x.Name, property);
      context.SectorGroupings.AddOrUpdate(x => x.Name, ukEquity);
      context.SectorGroupings.AddOrUpdate(x => x.Name, globalEquityGroup);

      context.Sectors.AddOrUpdate(x => x.Name, ukBond);
      context.Sectors.AddOrUpdate(x => x.Name, globalBond);
      context.Sectors.AddOrUpdate(x => x.Name, ukRealProperty);
      context.Sectors.AddOrUpdate(x => x.Name, globalProperty);
      context.Sectors.AddOrUpdate(x => x.Name, ukEquityIncome);
      context.Sectors.AddOrUpdate(x => x.Name, ukEquityGrowth);
      context.Sectors.AddOrUpdate(x => x.Name, ukSpecialist);
      context.Sectors.AddOrUpdate(x => x.Name, globalEquity);
      context.Sectors.AddOrUpdate(x => x.Name, globalSpecialist);

      FundSelection testFundSelection;

      var existingFundSelections = context.FundSelections.Where(x => x.Name == "Test fund selection 2017");

      if(existingFundSelections.Any()) {
        testFundSelection = existingFundSelections.First();
      }
      else {
        testFundSelection = new FundSelection { Name = "Test fund selection 2017", DateCreated = DateTime.Today };

        context.FundSelections.Add(testFundSelection);
        AddFund(context, testFundSelection, "Invesco Perpetual Corporate Bond", ukBond, atr50, atr60, atr70, atr80, 3, 3, 2, 1, "This is an OBSR GOLD rated fund managed by Paul Read and Paul Causer and is a Citywire Selected fund. Aim of Fund – to achieve a high level of overall return, with relative security of capital. It intends to invest primarily in fixed interest securities. Recent Fund performance is – 1yr  14.15 %, 3yr  18.69 %, 5yr  34.44%.");
        AddFund(context, testFundSelection, "Royal London Corporate Bond", ukBond, atr50, atr60, atr70, atr80, 3, 2, 1, 1, "This is an OBSR GOLD rated fund managed by Jonathan Platt and Sajiv Vaidand, both rated A by the Citywire survey of top fund managers over 3 years and was Shortlisted in the Investment Week Awards 2012 for the Sterling Corporate Bond sector. Aim of Fund – to achieve a combination of mainly income with some capital growth over the medium (5 years) to long term (7 years). Recent Fund performance is – 1yr  12.65%, 3yr  30.68%, 5yrs  34.07%.");
        AddFund(context, testFundSelection, "M&G Optimal Income", ukBond, atr50, atr60, atr70, atr80, 8, 7, 5, 2, "This is an OBSR SILVER rated fund managed by Richard Woolnough, is a Citywire Selected fund and was Shortlisted in the Investment Week Awards 2012 for the Strategic Bond sector. Aim of Fund – to provide a total return to investors based on exposure to optimal income streams in investment markets. Recent Fund performance is – 1yr  10.75%, 3yr  28.19%, 5yrs  66.28%.");
        AddFund(context, testFundSelection, "Invesco Perpetual Monthly Income Plus", ukBond, atr50, atr60, atr70, atr80, 6, 5, 4, 3, "");

        AddFund(context, testFundSelection, "Invesco Perpetual Global Bond", globalBond, atr50, atr60, atr70, atr80, 6, 4, 3, 2, "This is an OBSR BRONZE rated fund managed by Michael Matthews and Stuart Edwards. Aim of Fund – to achieve a good overall investment return in the medium to long term with relative security of capital. The underlying fund intends to invest primarily in international bonds of differing yields and maturities. Recent Fund performance is – 1yr  6.93%, 3yr  18.44%, 5yrs  48.53%.");
        AddFund(context, testFundSelection, "Templeton Global Total Return Bond", globalBond, atr50, atr60, atr70, atr80, 4, 3, 1, 1, "This is an OBSR BRONZE rated fund managed by Michael Hasenstab and Sonal Desai. Aim of Fund – to achieve a total return, over the long term, from a combination of income, capital growth and currency gains. Recent Fund performance is – 1yr  12.23%, 3yr  33.61%.");

        AddFund(context, testFundSelection, "Henderson UK Property", ukRealProperty, atr50, atr60, atr70, atr80, 10, 10, 8, 6, "This fund is managed by Marcus Langlands Pearse and Ainslie McLennan. Aim of Fund – to achieve a high income together with some growth of both income and capital through investment primarily in commercial property and property-related assets. Recent Fund performance is – 1yr  5.79%, 3yr  14.20%, 5yrs  -5.63%.");
        AddFund(context, testFundSelection, "Aviva Property Trust", ukRealProperty, atr50, atr60, atr70, atr80, 10, 6, 6, 4, "");
        AddFund(context, testFundSelection, "Aberdeen Propery Share", ukRealProperty, atr50, atr60, atr70, atr80, 3, 3, 4, 6, "");

        AddFund(context, testFundSelection, "Invesco Perpetual High Income", ukEquityIncome, atr50, atr60, atr70, atr80, 5, 6, 6, 5, "This is an OBSR GOLD rated fund managed by Neil Woodford- rated A by the Citywire survey of top fund managers over 3 years and is a Citywire selected fund. Aim of Fund – to achieve a high level of income, together with capital growth. The Fund intends to invest primarily in companies listed in the UK, with the balance invested internationally. Recent Fund performance is – 1yr  14.53%, 3yr  38.29%, 5yrs  35.31%.");
        AddFund(context, testFundSelection, "Royal London UK Equity Income", ukEquityIncome, atr50, atr60, atr70, atr80, 5, 4, 5, 5, "");

        AddFund(context, testFundSelection, "Cazenove UK Opportunities", ukEquityGrowth, atr50, atr60, atr70, atr80, 8, 10, 12, 15, "This is an OBSR BRONZE rated fund managed by Steve Cordell and Julie Dean - rated AAA by the Citywire survey of top fund managers over 3 years and was Shortlisted in the Investment Week Awards 2012 for the UK Growth sector. Aim of Fund – To achieve an income return, together with long term capital growth, by investing in any economic sector of the UK market. Recent Fund performance is – 1yr   25.69%.");
        AddFund(context, testFundSelection, "AXA Framlington UK Select Opps", ukEquityGrowth, atr50, atr60, atr70, atr80, 6, 8, 9, 12, "This is an OBSR GOLD rated fund managed by Nigel Thomas- rated AAA by the Citywire survey of top fund managers over 3 years, is a Citywire Star Pick fund and was Highly Commended in the Investment Week Awards 2012 for the UK Growth sector. Aim of Fund – to achieve capital growth by investing in companies, primarily of UK origin, where the Manager believes above average returns can be realised. Recent Fund performance is – 1yr  9.71%, 3yr  39.50%, 5yrs  42.52%.");

        AddFund(context, testFundSelection, "Cazenove UK Smaller Companies [CLOSED TO N/B]", ukSpecialist, atr50, atr60, atr70, atr80, 1, 1, 2, 1, "This is an OBSR BRONZE rated fund managed by John Warren and Paul Marriage - rated AAA by the Citywire survey of top fund managers over 3 years, is a Citywire selected fund and was Winner of the Investment Week Awards 2012 for the UK Smaller Companies sector. Aim of Fund – To achieve long term capital growth by investing primarily in UK smaller companies. The Fund will invest at least 80 per cent of its assets in the UK listed companies that form the bottom 10 percent by market capitalisation. The Fund may also invest in companies which are headquartered or have significant activities in the UK which are quoted on a stock exchange outside the UK. Recent Fund performance is – 1yr   34.23%, 3yr  90.98%, 5yrs  98.14%.");
        AddFund(context, testFundSelection, "Liontrust UK Smaller Companies", ukSpecialist, atr50, atr60, atr70, atr80, 2, 2, 2, 3, "This is an OBSR BRONZE rated fund managed by Julian Fosh and Anthony Cross – both rated AAA by the Citywire survey of top fund managers over 3 years. Aim of Fund – to provide long-term capital growth by investing primarily in smaller UK companies displaying a high degree of Intellectual Capital and employee motivation through equity ownership in their business model. Recent Fund performance is – 1yr  22.53%, 3yr  54.03%, 5yrs  75.07%.");

        AddFund(context, testFundSelection, "Artemis Global Growth", globalEquity, atr50, atr60, atr70, atr80, 7, 7, 8, 8, "");
        AddFund(context, testFundSelection, "M&G Global Dividend", globalEquity, atr50, atr60, atr70, atr80, 5, 6, 7, 7, "This is an OBSR SILVER rated fund managed by Stuart Rhodes - rated A by the Citywire survey of top fund managers over 3 years and was Winner of the Investment Week Awards 2012 for the Global Equity & Income sector. Aim of Fund – to deliver a dividend yield above the market average, by investing mainly in a range of global equities. The Fund aims to grow distributions over the long term whilst also maximising total return (the combination of income and growth of capital). Recent Fund performance is – 1yr  12.06%, 3yr  34.30%, 4yrs  65.71%.");
        AddFund(context, testFundSelection, "Invesco Perpetual Global Equity Income", globalEquity, atr50, atr60, atr70, atr80, 3, 5, 4, 5, "This fund is managed by Doug McGraw and Paul Boyne - rated AA by the Citywire survey of top fund managers over 3 years. Aim of Fund – to generate a rising level of income, together with long-term capital growth, investing primarily in global equities. Recent Fund performance is – 1yr  12.99%, 3yr  36.82%.");
        AddFund(context, testFundSelection, "Newton Global Higher Income", globalEquity, atr50, atr60, atr70, atr80, 2, 2, 3, 3, "This is an OBSR SILVER rated fund managed by James Harries and Nick Clay and is a Citywire selected fund. Aim of Fund –to achieve increasing income and capital growth over the long term by investing in shares (i.e. equities) and similar investments of companies listed or located throughout the world. Recent Fund performance is – 1yr  13.27%, 3yr  32.98%, 5yrs  38.68%.");

        AddFund(context, testFundSelection, "Old Mutual North American Equity", globalSpecialist, atr50, atr60, atr70, atr80, 2, 3, 3, 4, "");
        AddFund(context, testFundSelection, "Jupiter European", globalSpecialist, atr50, atr60, atr70, atr80, 1, 1, 2, 2, "");
        AddFund(context, testFundSelection, "First State Global Listed Infrastructure", globalSpecialist, atr50, atr60, atr70, atr80, 2, 2, 3, 4, "This is an OBSR SILVER rated fund managed by Andrew Greenup and Peter Meany, rated A by the Citywire survey of top fund managers over 3 years and is a Citywire Star Pick fund. Aim of Fund – to provide income and grow your investment. The Fund invests in shares of companies that are involved in infrastructure around the world. The infrastructure sector includes utilities (e.g. water and electricity), highways and railways, airports services, marine ports and services, and oil and gas storage and transportation. The Fund does not invest directly in infrastructure assets. Recent Fund performance is – 1yr  9.26%, 3yr  24.69%.");
      }

      if(!context.Clients.Where(x => x.Name == "TESTCLIENT").Any()) {
        var person1 = new Person { Title = "Mr", FirstName = "Nick", Surname = "Martin", Gender = "M", BirthDate = DateTime.Parse("1981-07-01"), IsPrimary = true };
        var person2 = new Person { Title = "Mrs", FirstName = "Natalie", Surname = "Martin", Gender = "F", BirthDate = DateTime.Parse("1983-12-17") };
        //var person3 = new Person { Title = "Mr", FirstName = "Dermot", Surname = "Griffin", Gender = "M", BirthDate = DateTime.Parse("1960-03-01") };

        var client1 = new Client { Name = "TESTCLIENT", ReviewFrequency = reviewFrequencyAnnual };
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

        var jointInvestmentClient1 = new Product { BusinessType = businessTypeCIA, Client = client1, Name = "Combined Investment", StartDate = DateTime.Parse("2012-07-31"), AttitudeToRiskCategory = atrCatInvestment };
        context.Products.Add(jointInvestmentClient1);
        client1.Products.Add(jointInvestmentClient1);
        var jointInvestmentClient1Valuation1 = new ProductValuation {
          Product = jointInvestmentClient1,
          Date = DateTime.Parse("2012-07-31"),
          Value = 4000
        };
        context.ProductValuations.Add(jointInvestmentClient1Valuation1);
        var jointInvestmentClient1Valuation2 = new ProductValuation {
          Product = jointInvestmentClient1,
          Date = DateTime.Parse("2013-07-31"),
          Value = 5000
        };
        context.ProductValuations.Add(jointInvestmentClient1Valuation2);
        var jointInvestmentClient1Valuation3 = new ProductValuation {
          Product = jointInvestmentClient1,
          Date = DateTime.Parse("2014-07-31"),
          Value = 5500
        };
        context.ProductValuations.Add(jointInvestmentClient1Valuation3);
        jointInvestmentClient1.Valuations.Add(jointInvestmentClient1Valuation1);
        jointInvestmentClient1.Valuations.Add(jointInvestmentClient1Valuation2);
        jointInvestmentClient1.Valuations.Add(jointInvestmentClient1Valuation3);


        var person1Pension = new Product { BusinessType = businessTypePension, Client = client1, Person = person1, Name = "Nicks Pension", StartDate = DateTime.Parse("2012-07-31"), AttitudeToRiskCategory = atrCatPension };
        var person1PensionValuation = new ProductValuation { Product = person1Pension, Date = DateTime.Parse("2012-07-31"), Value = 50000 };
        person1Pension.Valuations.Add(person1PensionValuation);
        context.Products.Add(person1Pension);
        context.ProductValuations.Add(person1PensionValuation);
        person1.PersonProducts.Add(person1Pension);

        var person1PensionProductFee = new ProductFee { Product = person1Pension, Percentage = 0.7f };
        person1Pension.ProductFeeAttached = true;
        person1Pension.ProductFee = person1PensionProductFee;
        context.ProductFees.Add(person1PensionProductFee);

        var person1ISA = new Product { BusinessType = businessTypeISA, Client = client1, Person = person1, Name = "Nicks ISA", StartDate = DateTime.Parse("2012-07-31"), AttitudeToRiskCategory = atrCatInvestment };
        var person1ISAValuation = new ProductValuation { Product = person1ISA, Date = DateTime.Parse("2012-07-31"), Value = 6000 };
        person1ISA.Valuations.Add(person1ISAValuation);
        context.Products.Add(person1ISA);
        context.ProductValuations.Add(person1ISAValuation);
        person1.PersonProducts.Add(person1ISA);

        var person2Bond = new Product { BusinessType = businessTypeWPBond, Client = client1, Person = person2, Name = "Natalie's Bond", StartDate = DateTime.Parse("2012-07-31"), AttitudeToRiskCategory = atrCatInvestment };
        var person2BondValuation = new ProductValuation { Product = person2Bond, Date = DateTime.Parse("2012-07-31"), Value = 10000 };
        person2Bond.Valuations.Add(person2BondValuation);
        context.Products.Add(person2Bond);
        context.ProductValuations.Add(person2BondValuation);
        person2.PersonProducts.Add(person2Bond);

        var conductedAtHome = new ReviewtHowConducted { Name = "Home", ReportText = "see you again at our recent meeting" };
        var conductedAtWork = new ReviewtHowConducted { Name = "Work", ReportText = "see you again at our recent meeting" };
        var conductedByTelephone = new ReviewtHowConducted { Name = "Telephone", ReportText = "talk to you again recently" };

        context.ReviewHowConducted.Add(conductedAtHome);
        context.ReviewHowConducted.Add(conductedAtWork);
        context.ReviewHowConducted.Add(conductedByTelephone);

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
          ReviewType = revTypeExistingCustomerReview,
          NextReviewDate = DateTime.Parse("2013-07-31"),
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
          ReviewType = revTypeExistingCustomerReview,
          NextReviewDate = DateTime.Parse("2015-07-30"),
        };

        jointInvestmentClient1Valuation2.AsPartOfReview = review1;
        jointInvestmentClient1Valuation3.AsPartOfReview = review2;
        person1PensionValuation.AsPartOfReview = review2;
        person1ISAValuation.AsPartOfReview = review2;
        person2BondValuation.AsPartOfReview = review2;

        client1.Reviews.Add(review1);
        client1.Reviews.Add(review2);

        context.Reviews.Add(review1);
        context.Reviews.Add(review2);


        PersonReview person1review1 = new PersonReview {
          Person = person1,
          Review = review1,
          IsATRChanging = false,
          ATROutput = "Above"
        };

        PersonReview person1review2 = new PersonReview {
          Person = person1,
          Review = review2,
          IsATRChanging = false,
          ATROutput = "Below"
        };

        PersonReview person2review1 = new PersonReview {
          Person = person1,
          Review = review1,
          IsATRChanging = false,
          ATROutput = "Above"
        };

        PersonReview person2review2 = new PersonReview {
          Person = person1,
          Review = review2,
          IsATRChanging = false,
          ATROutput = "Below"
        };

        jointInvestmentClient1Valuation2.AssetMix = testFundSelection;
        jointInvestmentClient1Valuation3.AssetMix = testFundSelection;
        person1PensionValuation.AssetMix = testFundSelection;
        person1ISAValuation.AssetMix = testFundSelection;

        context.PersonReviews.Add(person1review1);
        context.PersonReviews.Add(person1review2);
        context.PersonReviews.Add(person2review1);
        context.PersonReviews.Add(person2review2);
      }

      context.SaveChanges();
    }
  }
}