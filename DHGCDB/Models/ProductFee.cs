using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations;

namespace DHGCDB.Models
{
  public class ProductFee
  {
    public int ID { get; set; }

    [Required]
    public virtual Product Product { get; set; }

    public float Percentage { get; set; }
  }
}