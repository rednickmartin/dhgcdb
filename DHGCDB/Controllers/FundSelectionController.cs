using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DHGCDB.DAL;
using DHGCDB.ViewModels;

namespace DHGCDB.Models
{
  public class FundSelectionController : Controller
  {
    private ClientDBContext db = new ClientDBContext();

    // GET: FundSelections
    public ActionResult Index()
    {
      return View(db.FundSelections.ToList());
    }

    // GET: FundSelections/Details/5
    public ActionResult Details(int? id)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      FundSelection fundSelection = db.FundSelections.Find(id);
      if(fundSelection == null) {
        return HttpNotFound();
      }

      var currentSectorName = "";

      var fundSelectionForView = new FundSelectionForView(fundSelection);
      foreach(var fund in fundSelection.Funds.OrderBy(f => f.Sector.ID)) {
        var fundForView = new FundForView(fund);
        var sectorName = fund.Sector.Name;
        if(!sectorName.Equals(currentSectorName)) {
          fundForView.AssetSector = sectorName.ToUpper();
          currentSectorName = sectorName;
        }
        fundSelectionForView.Funds.Add(fundForView);
      }

      fundSelectionForView.AssetMix = new List<SectorGroupingForView>();
      foreach(var sectorGrouping in db.SectorGroupings.OrderBy(sg => sg.ID).ToList()) {
        var sectorGroupingForView = new SectorGroupingForView(sectorGrouping);
        sectorGroupingForView.ATR50Sum = fundSelectionForView.Funds.Where(f => f.SectorGroupingID == sectorGrouping.ID).Select(f => f.Percentage("50")).Sum();
        sectorGroupingForView.ATR60Sum = fundSelectionForView.Funds.Where(f => f.SectorGroupingID == sectorGrouping.ID).Select(f => f.Percentage("60")).Sum();
        sectorGroupingForView.ATR70Sum = fundSelectionForView.Funds.Where(f => f.SectorGroupingID == sectorGrouping.ID).Select(f => f.Percentage("70")).Sum();
        sectorGroupingForView.ATR80Sum = fundSelectionForView.Funds.Where(f => f.SectorGroupingID == sectorGrouping.ID).Select(f => f.Percentage("80")).Sum();
        sectorGroupingForView.ATR90Sum = fundSelectionForView.Funds.Where(f => f.SectorGroupingID == sectorGrouping.ID).Select(f => f.Percentage("90")).Sum();
        sectorGroupingForView.ATR100Sum = fundSelectionForView.Funds.Where(f => f.SectorGroupingID == sectorGrouping.ID).Select(f => f.Percentage("100")).Sum();
        fundSelectionForView.AssetMix.Add(sectorGroupingForView);
      }

      return View(fundSelectionForView);
    }

    // GET: FundSelections/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: FundSelections/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "ID,Name,DateCreated,IncludedInPensionFundSelections,IncludedInInvestmentFundSelections")] FundSelection fundSelection)
    {
      if(ModelState.IsValid) {
        db.FundSelections.Add(fundSelection);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      return View(fundSelection);
    }

    // GET: FundSelections/Edit/5
    public ActionResult Edit(int? id)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      FundSelection fundSelection = db.FundSelections.Find(id);
      if(fundSelection == null) {
        return HttpNotFound();
      }
      return View(fundSelection);
    }

    // POST: FundSelections/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "ID,Name,DateCreated,IncludedInPensionFundSelections,IncludedInInvestmentFundSelections")] FundSelection fundSelection)
    {
      if(ModelState.IsValid) {
        db.Entry(fundSelection).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Details", new { ID = fundSelection.ID });
      }
      return View(fundSelection);
    }

    // GET: FundSelections/Delete/5
    public ActionResult Delete(int? id)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      FundSelection fundSelection = db.FundSelections.Find(id);
      if(fundSelection == null) {
        return HttpNotFound();
      }
      return View(fundSelection);
    }

    // POST: FundSelections/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      FundSelection fundSelection = db.FundSelections.Find(id);
      db.FundSelections.Remove(fundSelection);
      db.SaveChanges();
      return RedirectToAction("Index");
    }

    // GET: FundSelections/FundDetails/5
    public ActionResult FundDetails(int? id)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Fund fund = db.Funds.Find(id);
      if(fund == null) {
        return HttpNotFound();
      }

      var fundForView = new FundForView(fund);

      return View(fundForView);
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
