using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHGCDB.Models
{
  public class BusinessType
  {
    public int ID { get; set; }

    public string Name { get; set; }

    public bool HasAssetMix { get; set; }

    public virtual AttitudeToRiskCategory ATRCategory { get; set; }
  }
}