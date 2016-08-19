using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DHGCDB.Models
{
  public class Fund
  {
    public Fund()
    {
      Allocations = new List<FundATRAllocation>();
    }

    public int ID { get; set; }

    public string Name { get; set; }

    public virtual Sector Sector { get; set; }

    [Display(Name = "Market Commentary")]
    public string Description { get; set; }

    public virtual FundSelection FundSelection { get; set; }

    public virtual ICollection<FundATRAllocation> Allocations { get; set; }
  }
}