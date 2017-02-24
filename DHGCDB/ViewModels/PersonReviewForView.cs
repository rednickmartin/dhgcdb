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

    [Display(Name = "Year of Risk Score")]
    public int YearOfRiskScore { get; set; }

    [Display(Name = "How has Risk Changed?")]
    public string RiskChanged { get; set; }

    [Display(Name = "Above or below output?")]
    public string AboveOrBelowOutput { get; set; }

    public SelectList AttitudeToRiskList { get; set; }
    public SelectList RiskChangeSelection { get; set; }
    public SelectList AboveOrBelowOutputSelection { get; set; }

  }
}