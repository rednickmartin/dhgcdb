using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHGCDB.Models;
using System.ComponentModel.DataAnnotations;


namespace DHGCDB.ViewModels
{
  public class PersonReviewForView
  {
    public PersonReviewForView() { }

    public PersonReviewForView(PersonReview personReview)
    {
      ID = personReview.ID;
      InvestmentAttitudeToRisk = personReview.InvestmentAttitudeToRisk.ID;
      InvestmentFundSelection = personReview.InvestmentFundSelection.ID;
      PensionAttitudeToRisk = personReview.PensionAttitudeToRisk.ID;
      PensionFundSelection = personReview.PensionFundSelection.ID;
      YearOfRiskScore = personReview.ATRYear;
      RiskChanged = personReview.ATRChanged;
      AboveOrBelowOutput = personReview.ATROutput;
    }

    public PersonReview PersonReview {
      get
      {
        return new PersonReview {
          ID = ID,
          ATRYear = YearOfRiskScore,
          ATRChanged = RiskChanged,
          ATROutput = AboveOrBelowOutput
        };
      }
    }

    public int ID { get; set; }

    public int ReviewID { get; set; }

    public string PersonName { get; set; }

    [Display(Name = "Investment - Attitude To Risk")]
    public int InvestmentAttitudeToRisk { get; set; }
    [Display(Name = "Investment - Attitude To Risk")]
    public string InvestmentAttitudeToRiskDisplay { get; set; }

    [Display(Name = "Investment - Fund Selection")]
    public int InvestmentFundSelection { get; set; }
    [Display(Name = "Investment - Fund Selection")]
    public string InvestmentFundSelectionDisplay { get; set; }

    [Display(Name = "Pension - Attitude To Risk")]
    public int PensionAttitudeToRisk { get; set; }
    [Display(Name = "Pension - Attitude To Risk")]
    public string PensionAttitudeToRiskDisplay { get; set; }

    [Display(Name = "Pension - Fund Selection")]
    public int PensionFundSelection { get; set; }
    [Display(Name = "Pension - Fund Selection")]
    public string PensionFundSelectionDisplay { get; set; }

    [Display(Name = "Year of Risk Score")]
    public int YearOfRiskScore { get; set; }

    [Display(Name = "How has Risk Changed?")]
    public string RiskChanged { get; set; }

    [Display(Name = "Above or below output?")]
    public string AboveOrBelowOutput { get; set; }

    public SelectList AttitudeToRiskList { get; set; }
    public SelectList FundSelection { get; set; }
    public SelectList RiskChangeSelection { get; set; }
    public SelectList AboveOrBelowOutputSelection { get; set; }

  }
}