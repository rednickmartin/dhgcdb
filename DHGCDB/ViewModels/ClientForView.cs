using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DHGCDB.ViewModels
{
  public class ClientForView
  {
    [Display(Name = "Client Account Name")]
    public string ClientName { get; set; }

    [Display(Name = "Address: First Line")]
    public string FirstLine { get; set; }

    [Display(Name = "Address: Second Line")]
    public string SecondLine { get; set; }

    [Display(Name = "Address: Town")]
    public string Town { get; set; }

    [Display(Name = "Address: County")]
    public string County { get; set; }

    [Display(Name = "Address: Postcode")]
    public string PostCode { get; set; }

    [Display(Name = "Review Frequency")]
    public int ReviewFrequency { get; set; }

    public SelectList ReviewFrequencyList { get; set; }


  }
}