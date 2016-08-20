using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DHGCDB.Models;

namespace DHGCDB.ViewModels
{
  public class SectorGroupingForView
  {
    public SectorGroupingForView(SectorGrouping sectorGrouping)
    {
      ID = sectorGrouping.ID;
      Name = sectorGrouping.Name;
    }

    public int ID { get; set; }

    public string Name { get; set; }

    public int ATR50Sum { get; set; }
    public int ATR60Sum { get; set; }
    public int ATR70Sum { get; set; }
    public int ATR80Sum { get; set; }
    public int ATR90Sum { get; set; }
    public int ATR100Sum { get; set; }
  }
}