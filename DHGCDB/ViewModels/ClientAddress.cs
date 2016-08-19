using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHGCDB.ViewModels
{
  public class ClientAddress
  {
    public string ClientName { get; set; }

    public string FirstLine { get; set; }

    public string SecondLine { get; set; }

    public string Town { get; set; }

    public string County { get; set; }

    public string PostCode { get; set; }
  }
}