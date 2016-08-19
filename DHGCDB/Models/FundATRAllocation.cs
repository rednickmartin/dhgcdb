using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHGCDB.Models
{
  public class FundATRAllocation
  {
    public int ID { get; set; }

    public virtual Fund Fund { get; set; }

    public virtual AttitudeToRisk AttitudeToRisk { get; set; }

    public int Percentage { get; set; }
  }
}