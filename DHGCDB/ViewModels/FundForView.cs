using System;
using DHGCDB.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DHGCDB.ViewModels
{
  public class FundForView
  {
    public FundForView() {
      ATRAllocations = new List<FundATRAllocation>();
    }

    public FundForView(Fund fund)
    {
      ID = fund.ID;
      Name = fund.Name;
      Description = fund.Description;
      FundSelectionID = fund.FundSelection.ID;

      ATRAllocations = new List<FundATRAllocation>();
      foreach(var allocation in fund.Allocations) {
        ATRAllocations.Add(allocation);
      }
    }

    public int ID { get; set; }

    public string Name { get; set; }

    public int FundSelectionID { get; set; }

    public int Percentage(string atr)
    {
      foreach(var allocation in ATRAllocations) {
        if(allocation.AttitudeToRisk.Name.Equals(atr)) {
          return allocation.Percentage;
        }
      }

      return 0;
    }

    private string PercentageString(string atr)
    {
      int percentage = Percentage(atr);
      return percentage == 0 ? "" : percentage.ToString();
    }

    public string AssetSector { get; set; }

    public string ATR50
    {
      get { return PercentageString("50"); }
    }

    public string ATR60
    {
      get { return PercentageString("60"); }
    }

    public string ATR70
    {
      get { return PercentageString("70"); }
    }

    public string ATR80
    {
      get { return PercentageString("80"); }
    }

    public string ATR90
    {
      get { return PercentageString("90"); }
    }

    public string ATR100
    {
      get { return PercentageString("100"); }
    }

    [Display(Name = "Market Commentary")]
    public string Description { get; set; }

    public ICollection<FundATRAllocation> ATRAllocations { get; set; }
  }
}