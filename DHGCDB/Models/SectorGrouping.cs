using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHGCDB.Models
{
  public class SectorGrouping
  {
    public SectorGrouping()
    {
      Sectors = new List<Sector>();
    }

    public int ID { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Sector> Sectors { get; set; }
  }
}