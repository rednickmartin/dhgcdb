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
    private static string GetSuffix(int day)
    {
      var lsd = day % 10;
      switch (lsd) {
      case 1:
        return "st";
      case 2:
        return "nd";
      case 3:
        return "rd";
      default:
        return "th";
      }
    }

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
      {
        "REPORT_DATE", (Review review) => {
          return String.Format("{0}{1} {2} {3}",
            review.ReviewDate.Day,
            GetSuffix(review.ReviewDate.Day),
            review.ReviewDate.ToString("M"),
            review.ReviewDate.Year);
        }
      },
    };
  }
}