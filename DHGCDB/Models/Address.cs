using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DHGCDB.Models
{
  public class Address
  {
    public int ID { get; set; }

    public string FirstLine { get; set; }

    public string SecondLine { get; set; }

    public string Town { get; set; }

    public string County { get; set; }

    public string PostCode { get; set; }
  }
}