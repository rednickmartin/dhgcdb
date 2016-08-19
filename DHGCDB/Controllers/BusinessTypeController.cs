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

namespace DHGCDB.Controllers
{
    public class BusinessTypeController : Controller
    {
        private ClientDBContext db = new ClientDBContext();

        // GET: BusinessType
        public ActionResult Index()
        {
            return View(db.BusinessTypes.ToList());
        }

        // GET: BusinessType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessType businessType = db.BusinessTypes.Find(id);
            if (businessType == null)
            {
                return HttpNotFound();
            }
            return View(businessType);
        }

        // GET: BusinessType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusinessType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] BusinessType businessType)
        {
            if (ModelState.IsValid)
            {
                db.BusinessTypes.Add(businessType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(businessType);
        }

        // GET: BusinessType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessType businessType = db.BusinessTypes.Find(id);
            if (businessType == null)
            {
                return HttpNotFound();
            }
            return View(businessType);
        }

        // POST: BusinessType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] BusinessType businessType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(businessType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(businessType);
        }

        // GET: BusinessType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessType businessType = db.BusinessTypes.Find(id);
            if (businessType == null)
            {
                return HttpNotFound();
            }
            return View(businessType);
        }

        // POST: BusinessType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusinessType businessType = db.BusinessTypes.Find(id);
            db.BusinessTypes.Remove(businessType);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
