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
      SectorGroupingID = fund.Sector.SectorGrouping.ID;
      Sector = fund.Sector.ID;

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

    [Display(Name = "Sector this fund belongs to")]
    public int Sector { get; set; }
    public int SectorGroupingID { get; set; }
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

    [Display(Name = "ATR 50 Score")]
    public int ATR50Input { get; set; }
    [Display(Name = "ATR 60 Score")]
    public int ATR60Input { get; set; }
    [Display(Name = "ATR 70 Score")]
    public int ATR70Input { get; set; }
    [Display(Name = "ATR 80 Score")]
    public int ATR80Input { get; set; }
    [Display(Name = "ATR 90 Score")]
    public int ATR90Input { get; set; }
    [Display(Name = "ATR 100 Score")]
    public int ATR100Input { get; set; }

    [Display(Name = "Market Commentary")]
    public string Description { get; set; }

    public ICollection<FundATRAllocation> ATRAllocations { get; set; }
  }
}