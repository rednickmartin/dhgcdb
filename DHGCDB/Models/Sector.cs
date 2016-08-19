using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHGCDB.Models
{
  public class Sector
  {
    public int ID { get; set; }

    public string Name { get; set; }

    public virtual SectorGrouping SectorGrouping { get; set; }
  }
}