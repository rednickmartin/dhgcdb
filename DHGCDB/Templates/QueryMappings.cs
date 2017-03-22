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

    private static string ListAsText(IEnumerable<String> list)
    {
      string first = list.First();
      if(list.Count() == 1) {
        return first;
      }
      else {
        string last = list.Last();
        IEnumerable<string> middle = list.Skip(1).Reverse().Skip(1).Reverse();
        if(middle.Any()) {
          return string.Format("{0}, {1} and {2}", first, string.Join(", ", middle), last);
        }
        else {
          return string.Format("{0} and {1}", first, last);
        }
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
            review.ReviewDate.ToString("MMMM"),
            review.ReviewDate.Year);
        }
      },
      {
        "CLIENTNAME_DEAR", (Review review) => {
          var persons = review.Client.Persons.Select(p => {
              int ordering = p.IsPrimary ? 0 : 1;
              return new Tuple<int, string>(ordering, p.FirstName);
            }).OrderBy(t => t.Item1).Select(t => t.Item2);

          return ListAsText(persons);
        }
      },
      {
        "MEETING_DESCRIPTION", (Review review) => {
          return review.HowConducted.ReportText;
        }
      },
      {
        "INVESTMENT_CHOICE", (Review review) => {
          var allProductTypes = new List<String>();
          allProductTypes.AddRange(review.Client.JointProducts.Select(j => j.BusinessType.ATRCategory.Name));

          foreach(var person in review.Client.Persons) {
            allProductTypes.AddRange(person.PersonProducts.Select(i => i.BusinessType.ATRCategory.Name));
          }

          return ListAsText(allProductTypes.Distinct());
        }
      },
      {
        "PSI_NEW_BUSINESS_INTRO1", (Review review) => {
          if(review.ReviewType.NewBusiness) {
            return "together with details of your proposed new investments";
          }
          else {
            return "";
          }
        }
      },
      {
        "KIIDS_SENT", (Review review) => {
          return review.KIIDSGiven.ReportText;
        }
      }
    };
  }
}