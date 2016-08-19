using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHGCDB.Models
{
  public class FundSelection
  {
    public FundSelection()
    {
      Funds = new List<Fund>();
    }

    public int ID { get; set; }

    public string Name { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual ICollection<Fund> Funds { get; set; }
  }
}