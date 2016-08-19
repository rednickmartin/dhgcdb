using System;
using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHGCDB.Models
{
  public class Review
  {
    public int ID { get; set; }

    public virtual Client Client { get; set; }

    public string Name { get; set; }

    public DateTime ReviewDate { get; set; }

    public virtual ReviewtHowConducted HowConducted { get; set; }

    public DateTime ValuationDate { get; set; }

    public bool IsJoint { get; set; }

    public int PortfolioSize { get; set; }

    public int AnnualCharges { get; set; }

    public bool GrowthIndividual { get; set; }

    public virtual KIIDSGiven KIIDSGiven { get; set; }

    public int NumberOfFunds { get; set; }

    public virtual ReviewType ReviewType { get; set; }
  }
}