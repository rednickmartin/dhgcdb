using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHGCDB.Models
{
  public class PensionATR
  {
    public int ID { get; set; }

    public virtual Person Person { get; set; }
    public virtual Review Review { get; set; }
    public virtual AttitudeToRisk AttitudeToRisk { get; set; }
  }
}