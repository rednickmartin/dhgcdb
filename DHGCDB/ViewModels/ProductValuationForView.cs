using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DHGCDB.Models;

namespace DHGCDB.ViewModels
{
  public class ProductValuationForView
  {
    public ProductValuationForView()
    {
    }

    public ProductValuationForView(ProductValuation productValuation)
    {
      ID = productValuation.ID;
      ProductName = productValuation.Product.Name;
      Date = productValuation.Date;
      Value = productValuation.Value;
    }

    public int ID { get; set; }

    public string ProductName { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Valuation Date")]
    public DateTime Date { get; set; }

    [Display(Name = "Valuation")]
    public float Value { get; set; }

    public bool HasAssetMix { get; set; }

    [Display(Name = "Seasonal Asset Mix")]
    public int AssetMix { get; set; }
    [Display(Name = "Seasonal Asset Mix")]
    public string AssetMixDisplay { get; set; }

  }
}