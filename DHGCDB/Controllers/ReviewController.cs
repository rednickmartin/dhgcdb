using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DHGCDB.DAL;
using DHGCDB.Models;
using DHGCDB.ViewModels;

namespace DHGCDB.Views
{
  public class ReviewController : Controller
  {
    private ClientDBContext db = new ClientDBContext();

    // GET: Review
    public ActionResult Index()
    {
      return View(db.Reviews.Select(x => new ReviewForView(x)).ToList());
    }

    private Dictionary<string,string> GetHowConductedList()
    {
      return db.ReviewHowConducted.ToDictionary(t => t.ID.ToString(), t => t.Name);
    }

    private Dictionary<string, string> GetReviewTypeList()
    {
      return db.ReviewTypes.ToDictionary(t => t.ID.ToString(), t => t.Name);
    }

    private Dictionary<string, string> GetKIIDsGivenList()
    {
      return db.KIIDSGivenTypes.ToDictionary(t => t.ID.ToString(), t => t.Name);
    }

    private Dictionary<string, string> GetAttitudeToRiskList()
    {
      return db.AttitudeToRiskSelections.ToDictionary(t => t.ID.ToString(), t => t.Name);
    }

    private Dictionary<string, string> GetFundSelectionList()
    {
      return db.FundSelections.ToDictionary(t => t.ID.ToString(), t => t.Name);
    }

    private Dictionary<string,string> GetRiskChangedList()
    {
      return new Dictionary<string,string> { { "Up", "Up" }, { "Same", "Same" }, { "Down", "Down" } };
    }

    private Dictionary<string,string> GetAboveOrBelowOutputList()
    {
      return new Dictionary<string, string> { { "Above", "Above" }, { "Below", "Below" } };
    }

    // GET: Review/Details/5
    public ActionResult Details(int? id)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Review review = db.Reviews.Find(id);
      if(review == null) {
        return HttpNotFound();
      }

      Client client = review.Client;
      ReviewForView reviewForView = new ReviewForView(review);
      reviewForView.ClientID = client.ID;

      return View(reviewForView);
    }

    // GET: Review/CreateS
    public ActionResult Create()
    {
      ViewBag.HowConductedList = GetHowConductedList();
      ViewBag.ReviewTypeList = GetReviewTypeList();
      ViewBag.KIIDsGivenList = GetKIIDsGivenList();
      return View();
    }

    // POST: Review/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(int? id, [Bind(Include = "ID,Name,ReviewDate,ValuationDate,IsJoint,PortfolioSize,AnnualCharges,GrowthIndividual,NumberOfFunds,HowConducted,ReviewType,KIIDsGiven")] ReviewForView reviewForView)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Client client = db.Clients.Find(id);
      if(client == null) {
        return HttpNotFound();
      }
      if(ModelState.IsValid) {
        Review review = reviewForView.Review;

        ReviewtHowConducted howConducted = db.ReviewHowConducted.Find(reviewForView.HowConducted);
        review.HowConducted = howConducted;

        ReviewType reviewType = db.ReviewTypes.Find(reviewForView.ReviewType);
        review.ReviewType = reviewType;

        KIIDSGiven kiidsGiven = db.KIIDSGivenTypes.Find(reviewForView.KIIDsGiven);
        review.KIIDSGiven = kiidsGiven;

        client.Reviews.Add(review);
        db.Reviews.Add(review);
        db.SaveChanges();

        var firstPersonID = client.Persons.First().ID;
        return RedirectToAction("CreatePersonReview", "Review", new { ID = client.ID, subID = firstPersonID });
      }

      return View(reviewForView);
    }

    // GET: Review/Edit/5
    public ActionResult Edit(int? id)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Review review = db.Reviews.Find(id);
      if(review == null) {
        return HttpNotFound();
      }
      return View(new ReviewForView(review));
    }

    // POST: Review/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "ID,Name,ReviewDate,ValuationDate,IsJoint,PortfolioSize,AnnualCharges,GrowthIndividual,NumberOfFunds")] ReviewForView reviewForView)
    {
      if(ModelState.IsValid) {
        Review review = reviewForView.Review;
        db.Entry(review).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Details", new { ID = review.ID });
      }
      return View(reviewForView);
    }

    // GET: Review/Delete/5
    public ActionResult Delete(int? id)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Review review = db.Reviews.Find(id);
      if(review == null) {
        return HttpNotFound();
      }
      Client client = review.Client;
      ReviewForView reviewForView = new ReviewForView(review);
      reviewForView.ClientID = client.ID;
      return View(reviewForView);
    }

    // POST: Review/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Review review = db.Reviews.Find(id);
      Client client = review.Client;

      db.Reviews.Remove(review);
      db.SaveChanges();
      return RedirectToAction("Details", "Client", new { ID = client.ID });
    }

    // GET: Review/CreatePersonReview/5/20
    public ActionResult CreatePersonReview(int? id, int? subid)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      if(subid == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ViewBag.AttitudeToRiskList = GetAttitudeToRiskList();
      ViewBag.FundSelectionList = GetFundSelectionList();
      ViewBag.RiskChangedLust = GetRiskChangedList();
      ViewBag.AboveOrBelowOutputList = GetAboveOrBelowOutputList();

      Review review = db.Reviews.Find(id);
      if(review == null) {
        return HttpNotFound();
      }

      Person person = db.People.Find(subid);

      if(person == null) {
        return HttpNotFound();
      }

      ViewBag.PersonName = person.ToString();

      return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult CreatePersonReview(int? id, int? subid, [Bind(Include = "InvestmentAttitudeToRisk,InvestmentFundSelection,PensionAttitudeToRisk,PensionFundSelection,YearOfRiskScore,RiskChanged,AboveOrBelowOutput")] PersonReviewForView personReviewForView)
    {
      if(ModelState.IsValid) {
        if(id == null) {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        if(subid == null) {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        Review review = db.Reviews.Find(id);
        if(review == null) {
          return HttpNotFound();
        }

        Person person = db.People.Find(subid);
        if(person == null) {
          return HttpNotFound();
        }

        Client client = review.Client;

        foreach(var individual in client.Persons) {
          db.PersonReviews.Find(new { Person = individual.ID, Review = review.ID });
        }

        return RedirectToAction("Details", "Client", new { ID = client.ID });

      }
      return View(personReviewForView);
    }


    protected override void Dispose(bool disposing)
    {
      if(disposing) {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
