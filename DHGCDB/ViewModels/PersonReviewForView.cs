using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHGCDB.Models;


namespace DHGCDB.ViewModels
{
  public class PersonReviewForView
  {
    public PersonReviewForView(PersonReview personReview)
    {
      InvestmentAttitudeToRisk = personReview.InvestmentAttitudeToRisk.ID;
      InvestmentFundSelection = personReview.InvestmentFundSelection.ID;
      PensionAttitudeToRisk = personReview.PensionAttitudeToRisk.ID;
      PensionFundSelection = personReview.PensionFundSelection.ID;
      YearOfRiskScore = personReview.ATRYear;
      RiskChanged = personReview.ATRChanged;
      AboveOrBelowOutput = personReview.ATROutput;
    }

    public string PersonName { get; set; }

    public int InvestmentAttitudeToRisk { get; set; }
    public int InvestmentFundSelection { get; set; }
    public int PensionAttitudeToRisk { get; set; }
    public int PensionFundSelection { get; set; }

    public int YearOfRiskScore { get; set; }
    public string RiskChanged { get; set; }
    public string AboveOrBelowOutput { get; set; }

    public SelectList AttitudeToRiskList { get; set; }
    public SelectList FundSelection { get; set; }
    public SelectList RiskChangeSelection { get; set; }
    public SelectList AboveOrBelowOutputSelection { get; set; }


  }
}