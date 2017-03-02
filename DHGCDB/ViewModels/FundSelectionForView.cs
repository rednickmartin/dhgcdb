using System;
using DHGCDB.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DHGCDB.ViewModels
{
  public class FundSelectionForView
  {
    public FundSelectionForView() {
      Funds = new List<FundForView>();
    }

    public FundSelectionForView(FundSelection fundSelection)
    {
      ID = fundSelection.ID;
      Name = fundSelection.Name;

      Funds = new List<FundForView>();
    }

    public int ID { get; set; }

    public string Name { get; set; }

    private int Total(string atr)
    {
      return Funds.Select(f => f.Percentage(atr)).Sum();
    }

    public int Total50 { get { return Total("50"); } }
    public int Total60 { get { return Total("60"); } }
    public int Total70 { get { return Total("70"); } }
    public int Total80 { get { return Total("80"); } }
    public int Total90 { get { return Total("90"); } }
    public int Total100 { get { return Total("100"); } }

    public ICollection<FundForView> Funds { get; set; }

    public IList<SectorGroupingForView> AssetMix { get; set; }
  }
}