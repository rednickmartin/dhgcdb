using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DHGCDB.Models
{
  public class Product
  {
    public int ID { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    [Display(Name = "Business Type")]
    public virtual BusinessType BusinessType { get; set; }

    public virtual Client Client { get; set; }

    public virtual Person Person { get; set; }
  }
}