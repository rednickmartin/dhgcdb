using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHGCDB.Models
{
  public class ProductValuation
  {
    public ProductValuation() { }

    public int ID { get; set; }

    public virtual Product Product { get; set; }

    public virtual Review AsPartOfReview { get; set; }

    public DateTime Date { get; set; }

    public float Value { get; set; }

    public virtual FundSelection AssetMix { get; set; }
  }
}