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

    private Dictionary<string, string> GetFundSelectionList()
    {
      return db.FundSelections.ToDictionary(t => t.ID.ToString(), t => t.Name);
    }

    private Dictionary<string, string> GetAttitudeToRiskList()
    {
      return db.AttitudeToRiskSelections.ToDictionary(t => t.ID.ToString(), t => t.Name);
    }

    private Dictionary<string, string> GetAttitudeToRiskCategoryList()
    {
      return db.AttitudeToRiskCategories.ToDictionary(t => t.ID.ToString(), t => t.Name);
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
      reviewForView.HowConductedView = review.HowConducted.Name;
      reviewForView.KIIDsGivenView = review.KIIDSGiven.Name;
      reviewForView.ReviewTypeView = review.ReviewType.Name;

      foreach(var personReviewID in db.PersonReviews.Where(pr => pr.Review.ID == id).Select(pr => pr.ID).ToList()) {
        var personReview = db.PersonReviews.Find(personReviewID);
        var personReviewForView = new PersonReviewForView(personReview);
        personReviewForView.PersonName = personReview.Person.ToString();

        reviewForView.IndividualReviews.Add(personReviewForView);
      }

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

    private RedirectToRouteResult DoPersonReview(Review review, Person person)
    {
      // Need to figure out if they have any undefined ATRs or not
      AttitudeToRiskCategory atrCategory;
      if(person.MissingATR(out atrCategory)) {
        return RedirectToAction("AddPersonMissingATR", "Review", new { ID = review.ID, subID = person.ID });
      }
      else {
        return RedirectToAction("CreatePersonReview", "Review", new { ID = review.ID, subID = person.ID });
      }
    }

    // POST: Review/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(int? id, [Bind(Include = "ID,Name,ReviewDate,ValuationDate,IsJoint,PortfolioSize,AnnualCharges,NumberOfFunds,HowConducted,ReviewType,KIIDsGiven")] ReviewForView reviewForView)
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

        if(client.JointProducts.Any()) {
          // Need to evaluate the joint products
          var firstProductID = client.JointProducts.First().ID;
          return RedirectToAction("JointProductValuation", "Review", new { ID = review.ID, subID = firstProductID });
        }
        else {
          // No joint products - go straight to person review
          return DoPersonReview(review, client.Persons.First());
        }
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

      var reviewForView = new ReviewForView(review);
      reviewForView.HowConductedList = new SelectList(GetHowConductedList(), "Key", "Value");
      reviewForView.KIIDsGivenList = new SelectList(GetKIIDsGivenList(), "Key", "Value");
      reviewForView.ReviewTypeList = new SelectList(GetReviewTypeList(), "Key", "Value");

      return View(reviewForView);
    }

    // POST: Review/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "ID,Name,ReviewDate,ValuationDate,IsJoint,PortfolioSize,AnnualCharges,NumberOfFunds,HowConducted,ReviewType,KIIDsGiven")] ReviewForView reviewForView)
    {
      if(ModelState.IsValid) {
        Review review = db.Reviews.Find(reviewForView.ID);

        ReviewtHowConducted howConducted = db.ReviewHowConducted.Find(reviewForView.HowConducted);
        review.HowConducted = howConducted;

        ReviewType reviewType = db.ReviewTypes.Find(reviewForView.ReviewType);
        review.ReviewType = reviewType;

        KIIDSGiven kiidsGiven = db.KIIDSGivenTypes.Find(reviewForView.KIIDsGiven);
        review.KIIDSGiven = kiidsGiven;

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

      foreach(var personReview in db.PersonReviews.Where(pr => pr.Review.ID == id)) {
        db.PersonReviews.Remove(personReview);
      }

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

      Review review = db.Reviews.Find(id);
      if(review == null) {
        return HttpNotFound();
      }

      Person person = db.People.Find(subid);

      if(person == null) {
        return HttpNotFound();
      }

      ViewBag.AboveOrBelowOutputList = GetAboveOrBelowOutputList();
      ViewBag.PersonName = person.ToString();
      ViewBag.AllCurrentAttitudeToRisk = person.AllCurrentAttitudeToRisk;

      return View();
    }

    private RedirectToRouteResult NextPersonOrClientDetails(Client client, Review review)
    {
      foreach(var individual in client.Persons) {
        var existingPersonReviews = db.PersonReviews.Where(pr => pr.Person.ID == individual.ID && pr.Review.ID == review.ID);
        if(existingPersonReviews.Any())
          continue;
        else
          return DoPersonReview(review, individual);
      }

      return RedirectToAction("Details", "Client", new { ID = client.ID });
    }

    private RedirectToRouteResult IndividualProductValuationOrNextPerson(Review review, Client client, Person person)
    {
      if(person.PersonProducts.Any()) {
        return RedirectToAction("IndividualProductValuation", new { ID = review.ID, subid = person.PersonProducts.First().ID });
      }
      else {
        return NextPersonOrClientDetails(client, review);
      }

    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult CreatePersonReview(int? id, int? subid, [Bind(Include = "IsATRChanging,AboveOrBelowOutput")] PersonReviewForView personReviewForView)
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

        PersonReview personReview = personReviewForView.PersonReview;
        personReview.Person = person;
        personReview.Review = review;

        db.PersonReviews.Add(personReview);
        db.SaveChanges();

        if(personReview.IsATRChanging) {
          return RedirectToAction("ChangePersonATR", new { ID = review.ID, subid = person.ID });
        }
        else {
          return IndividualProductValuationOrNextPerson(review, client, person);
        }
      }

      return View(personReviewForView);
    }


    // GET: Review/EditPersonReview/5
    public ActionResult EditPersonReview(int? id)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      PersonReview personReview = db.PersonReviews.Find(id);
      if(personReview == null) {
        return HttpNotFound();
      }

      var personReviewForView = new PersonReviewForView(personReview);
      personReviewForView.ReviewID = personReview.Review.ID;
      personReviewForView.IsATRChanging = personReview.IsATRChanging;
      personReviewForView.AboveOrBelowOutputSelection = new SelectList(GetAboveOrBelowOutputList(), "Key", "Value");

      return View(personReviewForView);
    }

    // POST: Review/EditPersonReview/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditPersonReview(int? id, [Bind(Include = "ID,AboveOrBelowOutput,IsATRChanging")] PersonReviewForView personReviewForView)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      PersonReview personReview = db.PersonReviews.Find(id);
      if(personReview == null) {
        return HttpNotFound();
      }

      if(ModelState.IsValid) {
        personReview.ATROutput = personReviewForView.AboveOrBelowOutput;
        personReview.IsATRChanging = personReviewForView.IsATRChanging;
        db.SaveChanges();

        return RedirectToAction("Details", new { ID = personReview.Review.ID });
      }
      return View(personReviewForView);
    }


    // GET: Review/JointProductValuation/5/20
    public ActionResult JointProductValuation(int? id, int? subid)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      if(subid == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      Review review = db.Reviews.Find(id);
      if(review == null) {
        return HttpNotFound("Review not found");
      }

      Product jointProduct = db.Products.Find(subid);
      if(jointProduct == null) {
        return HttpNotFound("Product not found");
      }

      ViewBag.ProductName = string.Format("{0} ({1})", jointProduct.Name, jointProduct.BusinessType.Name);
      ViewBag.HasAssetMix = jointProduct.BusinessType.HasAssetMix;
      ViewBag.FundSelectionList = GetFundSelectionList();

      ViewBag.PreviousValuations = jointProduct.Valuations.OrderByDescending(v => v.Date);

      return View();
    }

    // POST: Review/JointProductValuation/5/20
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult JointProductValuation(int? id, int? subid, [Bind(Include = "Date,Value,AssetMix")] ProductValuationForView productValuationForView)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      if(subid == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      Review review = db.Reviews.Find(id);
      if(review == null) {
        return HttpNotFound("Review not found");
      }

      Product jointProduct = db.Products.Find(subid);
      if(jointProduct == null) {
        return HttpNotFound("Product not found");
      }

      if(ModelState.IsValid) {
        var productValuation = new ProductValuation {
          Date = productValuationForView.Date,
          Value = productValuationForView.Value,
          Product = jointProduct,
          AsPartOfReview = review
        };

        if(jointProduct.BusinessType.HasAssetMix) {
          var assetMix = db.FundSelections.Find(productValuationForView.AssetMix);
          if(assetMix == null) {
            return HttpNotFound("Asset Mix not found");
          }

          productValuation.AssetMix = assetMix;
        }

        db.ProductValuations.Add(productValuation);
        jointProduct.Valuations.Add(productValuation);

        db.SaveChanges();

        // Are there any other joint products to evaluate?
        var unvaluedProducts = review.Client.JointProducts.Where(p => {
          var v = p.LatestValuation;
          return (v == null || v.AsPartOfReview == null || v.AsPartOfReview.ID != review.ID);
        });

        if(unvaluedProducts.Any()) {
          // Joint products to value - redirect to first in the list
          return RedirectToAction("JointProductValuation", new { ID = review.ID, subid = unvaluedProducts.First().ID });
        }
        else {
          return DoPersonReview(review, review.Client.Persons.First());
        }
      }

      return View(productValuationForView);
    }


    // GET: Review/IndividualProductValuation/5/20
    public ActionResult IndividualProductValuation(int? id, int? subid)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      if(subid == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      Review review = db.Reviews.Find(id);
      if(review == null) {
        return HttpNotFound("Review not found");
      }

      Product individualProduct = db.Products.Find(subid);
      if(individualProduct == null) {
        return HttpNotFound("Product not found");
      }

      ViewBag.ProductName = string.Format("{0} ({1})", individualProduct.Name, individualProduct.BusinessType.Name);
      ViewBag.PersonName = individualProduct.Person.ToString();
      ViewBag.HasAssetMix = individualProduct.BusinessType.HasAssetMix;
      ViewBag.FundSelectionList = GetFundSelectionList();

      ViewBag.PreviousValuations = individualProduct.Valuations.OrderByDescending(v => v.Date);

      return View();
    }

    // POST: Review/IndividualProductValuation/5/20
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult IndividualProductValuation(int? id, int? subid, [Bind(Include = "Date,Value,AssetMix")] ProductValuationForView productValuationForView)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      if(subid == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      Review review = db.Reviews.Find(id);
      if(review == null) {
        return HttpNotFound("Review not found");
      }

      Product individualProduct = db.Products.Find(subid);
      if(individualProduct == null) {
        return HttpNotFound("Product not found");
      }

      if(ModelState.IsValid) {
        var productValuation = new ProductValuation {
          Date = productValuationForView.Date,
          Value = productValuationForView.Value,
          Product = individualProduct,
          AsPartOfReview = review
        };

        if(individualProduct.BusinessType.HasAssetMix) {
          var assetMix = db.FundSelections.Find(productValuationForView.AssetMix);
          if(assetMix == null) {
            return HttpNotFound("Asset Mix not found");
          }

          productValuation.AssetMix = assetMix;
        }

        db.ProductValuations.Add(productValuation);
        individualProduct.Valuations.Add(productValuation);

        db.SaveChanges();

        var Person = individualProduct.Person;

        // Are there any other joint products to evaluate?
        var unvaluedProducts = Person.PersonProducts.Where(p => {
          var v = p.LatestValuation;
          return (v == null || v.AsPartOfReview == null || v.AsPartOfReview.ID != review.ID);
        });

        if(unvaluedProducts.Any()) {
          // Individual products to value - redirect to first in the list
          return RedirectToAction("IndividualProductValuation", new { ID = review.ID, subid = unvaluedProducts.First().ID });
        }
        else {
          return NextPersonOrClientDetails(review.Client, review);
        }
      }

      return View(productValuationForView);
    }

    // GET: Review/AddPersonMissingATR/5/20
    public ActionResult AddPersonMissingATR(int? id, int? subid)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      if(subid == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      Review review = db.Reviews.Find(id);
      if(review == null) {
        return HttpNotFound("Review not found");
      }

      Person person = db.People.Find(subid);
      if(person == null) {
        return HttpNotFound("Person not found");
      }
      
      AttitudeToRiskCategory atrCategory;
      if(!person.MissingATR(out atrCategory)) {
        return HttpNotFound("Missing ATR Category not found");
      }

      ViewBag.ATRType = atrCategory.ID;
      ViewBag.ATRTypeDisplay = atrCategory.Name;
      ViewBag.PersonName = person.ToString();
      ViewBag.AttitudeToRiskList = GetAttitudeToRiskList();

      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddPersonMissingATR(int? id, int? subid, [Bind(Include = "AttitudeToRiskCategory,AttitudeToRisk")] PersonsAttitudeToRiskForView patrv)
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
          return HttpNotFound("Review not found");
        }

        Person person = db.People.Find(subid);
        if(person == null) {
          return HttpNotFound("Person not found");
        }

        var atrcat = db.AttitudeToRiskCategories.Find(patrv.AttitudeToRiskCategory);
        if(atrcat == null) {
          return HttpNotFound();
        }

        var atr = db.AttitudeToRiskSelections.Find(patrv.AttitudeToRisk);
        if(atr == null) {
          return HttpNotFound("Attitude To Risk not found");
        }

        var patr = new PersonsAttitudeToRisk {
          Person = person,
          FromDate = review.ReviewDate,
          AttitudeToRisk = atr,
          AttitudeToRiskCategory = atrcat,
          AsPartOfReview = review
        };

        person.AttitudeToRiskHistory.Add(patr);
        db.PeoplesAttitudeToRisks.Add(patr);

        db.SaveChanges();

        return DoPersonReview(review, person);
      }

      return View(patrv);
    }


    // GET: Review/ChangePersonATR/5/20
    public ActionResult ChangePersonATR(int? id, int? subid)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      if(subid == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      Review review = db.Reviews.Find(id);
      if(review == null) {
        return HttpNotFound("Review not found");
      }

      Person person = db.People.Find(subid);
      if(person == null) {
        return HttpNotFound("Person not found");
      }

      ViewBag.PersonName = person.ToString();
      ViewBag.AttitudeToRiskList = GetAttitudeToRiskList();
      ViewBag.AttitudeToRiskCategoryList = GetAttitudeToRiskCategoryList();

      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult ChangePersonATR(int? id, int? subid, [Bind(Include = "AttitudeToRiskCategory,AttitudeToRisk")] PersonsAttitudeToRiskForView patrv)
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
          return HttpNotFound("Review not found");
        }

        Person person = db.People.Find(subid);
        if(person == null) {
          return HttpNotFound("Person not found");
        }

        var atrcat = db.AttitudeToRiskCategories.Find(patrv.AttitudeToRiskCategory);
        if(atrcat == null) {
          return HttpNotFound();
        }

        var atr = db.AttitudeToRiskSelections.Find(patrv.AttitudeToRisk);
        if(atr == null) {
          return HttpNotFound("Attitude To Risk not found");
        }

        var patr = new PersonsAttitudeToRisk {
          Person = person,
          FromDate = review.ReviewDate,
          AttitudeToRisk = atr,
          AttitudeToRiskCategory = atrcat,
          AsPartOfReview = review
        };

        person.AttitudeToRiskHistory.Add(patr);
        db.PeoplesAttitudeToRisks.Add(patr);

        db.SaveChanges();

        return IndividualProductValuationOrNextPerson(review, review.Client, person);
      }

      return View(patrv);
    }


    public ActionResult CreateReviewDocument(int? id)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Review review = db.Reviews.Find(id);
      if(review == null) {
        return HttpNotFound();
      }

      var location = Controllers.ReportCreater.Create(review);
      return Redirect(String.Format(@"~/Output/{0}", location));
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
