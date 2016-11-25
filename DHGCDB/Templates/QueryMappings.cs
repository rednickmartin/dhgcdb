using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DHGCDB.Models;
using DHGCDB.ViewModels;

namespace DHGCDB.Templates
{
  public class QueryMappings
  {
    public static Dictionary<string, Func<Review, string>> ReviewData = new Dictionary<string, Func<Review, string>> {
      {
        "CLIENTNAME_ADDRESS", (Review review) => {
          return review.Client.Individuals;
        }
      },
      {
        "ADDRESS_FIRSTLINE", (Review review) => {
          return review.Client.Address.FirstLine;
        }
      },
      {
        "ADDRESS_TOWN", (Review review) => {
          return review.Client.Address.Town;
        }
      },
      {
        "ADDRESS_COUNTY", (Review review) => {
          return review.Client.Address.County;
        }
      },
      {
        "ADDRESS_POSTCODE", (Review review) => {
          return review.Client.Address.PostCode;
        }
      },
    };
  }
}