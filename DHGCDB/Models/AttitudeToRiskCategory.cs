using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHGCDB.Models
{
  public class AttitudeToRiskCategory
  {
    public int ID { get; set; }

    public string Name { get; set; }

    public override bool Equals(object obj)
    {
      var atrcat = obj as AttitudeToRiskCategory;
      if(atrcat == null)
        return false;

      return atrcat.ID.Equals(ID);
    }

    public override int GetHashCode()
    {
      return ID.GetHashCode();
    }
  }
}