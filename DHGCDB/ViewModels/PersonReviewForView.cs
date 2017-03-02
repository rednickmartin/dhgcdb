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
      AboveOrBelowOutput = personReview.ATROutput;
      IsATRChanging = personReview.IsATRChanging;
    }

    public PersonReview PersonReview {
      get
      {
        return new PersonReview {
          ID = ID,
          ATROutput = AboveOrBelowOutput,
          IsATRChanging = IsATRChanging
        };
      }
    }

    public int ID { get; set; }

    public int ReviewID { get; set; }

    public string PersonName { get; set; }

    [Display(Name = "Above or below output?")]
    public string AboveOrBelowOutput { get; set; }

    [Display(Name = "Is Attitude To Risk Changing?")]
    public bool IsATRChanging { get; set; }

    public SelectList AboveOrBelowOutputSelection { get; set; }

  }
}