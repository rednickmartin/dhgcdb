using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DHGCDB.Models
{
  public class Product
  {
    public Product()
    {
      Valuations = new List<ProductValuation>();
    }

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

    public bool ProductFeeAttached { get; set; }

    public virtual ProductFee ProductFee { get; set; }

    [Required]
    public virtual AttitudeToRiskCategory AttitudeToRiskCategory { get; set; }

    [Display(Name = "Product Fee Applies?")]
    public string ProductFeeForDisplay
    {
      get
      {
        return ProductFeeAttached ? ProductFee.Percentage.ToString() + "%" : "";
      }
    }

    public virtual ICollection<ProductValuation> Valuations { get; set; }

    public ProductValuation LatestValuation
    {
      get
      {
        return Valuations.OrderByDescending(x => x.Date).FirstOrDefault();
      }
    }
  }
}