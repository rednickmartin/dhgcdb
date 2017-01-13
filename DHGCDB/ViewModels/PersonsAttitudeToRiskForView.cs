using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using DHGCDB.Models;
using System.ComponentModel.DataAnnotations;

namespace DHGCDB.ViewModels
{
  public class PersonsAttitudeToRiskForView
  {
    public PersonsAttitudeToRiskForView() { }

    public PersonsAttitudeToRiskForView(PersonsAttitudeToRisk patr)
    {
      ID = patr.ID;
      FromDate = patr.FromDate;
      AttitudeToRiskCategory = patr.AttitudeToRiskCategory.ID;
      AttitudeToRisk = patr.AttitudeToRisk.ID;
    }

    public int ID { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime FromDate { get; set; }

    [Display(Name = "ATR Category")]
    public int AttitudeToRiskCategory { get; set; }
    [Display(Name = "ATR Category")]
    public string AttitudeToRiskCategoryDisplay { get; set; }

    [Display(Name = "Attitude To Risk")]
    public int AttitudeToRisk { get; set; }
    [Display(Name = "Attitude To Risk")]
    public string AttitudeToRiskDisplay { get; set; }


    public SelectList AttitudeToRiskCategoryList { get; set; }

    public SelectList AttitudeToRiskList { get; set; }

  }
}