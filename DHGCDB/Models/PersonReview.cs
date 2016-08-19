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

    public virtual AttitudeToRisk PensionAttitudeToRisk { get; set; }
    public virtual FundSelection PensionFundSelection { get; set; }

    public virtual AttitudeToRisk InvestmentAttitudeToRisk { get; set; }
    public virtual FundSelection InvestmentFundSelection { get; set;  }

    public int ATRYear { get; set; }

    public string ATRChanged { get; set; }

    public string ATROutput { get; set; }
  }
}