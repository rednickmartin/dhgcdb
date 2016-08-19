using System;
using DHGCDB.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DHGCDB.ViewModels
{
  public class ReviewForView
  {
    public ReviewForView() {
      IndividualReviews = new List<PersonReviewForView>();
    }

    public ReviewForView(Review review)
    {
      ID = review.ID;
      Name = review.Name;
      ReviewDate = review.ReviewDate;
      ValuationDate = review.ValuationDate;
      IsJoint = review.IsJoint;
      PortfolioSize = review.PortfolioSize;
      AnnualCharges = review.AnnualCharges;
      NumberOfFunds = review.NumberOfFunds;
      IndividualReviews = new List<PersonReviewForView>();
    }

    public Review Review {
      get {
        return new Review {
          ID = ID,
          Name = Name,
          ReviewDate = ReviewDate,
          ValuationDate = ValuationDate,
          IsJoint = IsJoint,
          PortfolioSize = PortfolioSize,
          AnnualCharges = AnnualCharges,
          NumberOfFunds = NumberOfFunds
        };
      }
    }

    public int ID { get; set; }

    public string Name { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime ReviewDate { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime ValuationDate { get; set; }

    public bool IsJoint { get; set; }

    public int PortfolioSize { get; set; }

    public int AnnualCharges { get; set; }

    public int NumberOfFunds { get; set; }

    public int HowConducted { get; set; }

    public SelectList HowConductedList { get; set; }

    public int ReviewType { get; set; }

    public SelectList ReviewTypeList { get; set; }

    public int KIIDsGiven { get; set; }

    public SelectList KIIDsGivenList { get; set; }

    public int? ClientID { get; set; }

    public ICollection<PersonReviewForView> IndividualReviews { get; set; }
  }
}