using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using DHGCDB.Models;


namespace DHGCDB.ViewModels
{
  public class ProductForView
  {
    public ProductForView() { }

    public ProductForView(Product product)
    {
      ID = product.ID;
      Name = product.Name;
      StartDate = product.StartDate;
      BusinessType = product.BusinessType.ID;
      ClientID = product.Client.ID;

      ProductFeeApplies = product.ProductFeeAttached;
      if(ProductFeeApplies) {
        ProductFeePercentage = product.ProductFee.Percentage;
      }
    }

    public int ID { get; set; }

    public int ClientID { get; set; }

    public int PersonID { get; set; }

    [Required]
    public string Name { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime StartDate { get; set; }

    public int BusinessType { get; set; }

    public SelectList BusinessTypeList { get; set; }

    [Display(Name = "Product Fee Applies")]
    public bool ProductFeeApplies { get; set; }

    [Display(Name = "Product Fee Percentage (only applies if above box ticked)")]
    public float ProductFeePercentage { get; set; }
  }
}