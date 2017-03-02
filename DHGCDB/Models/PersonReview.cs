using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHGCDB.Models
{
  public class PersonReview
  {
    public int ID { get; set; }

    public virtual Person Person { get; set; }

    public virtual Review Review { get; set; }

    public string ATROutput { get; set; }

    public bool IsATRChanging { get; set; }

    public virtual PersonsAttitudeToRisk AttitudeToRisk { get; set; }
  }
}