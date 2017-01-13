using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DHGCDB.Models
{
  public class PersonsAttitudeToRisk
  {
    public int ID { get; set; }

    public virtual Person Person { get; set; }

    public virtual AttitudeToRiskCategory AttitudeToRiskCategory { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime FromDate { get; set; }

    public virtual AttitudeToRisk AttitudeToRisk { get; set; }

    public override bool Equals(object obj)
    {
      var patr = obj as PersonsAttitudeToRisk;
      if(patr == null)
        return false;

      return patr.ID.Equals(ID);
    }

    public override int GetHashCode()
    {
      return ID.GetHashCode();
    }

  }
}