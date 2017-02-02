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

namespace DHGCDB.Controllers
{
  public class ClientController : Controller
  {
    private ClientDBContext db = new ClientDBContext();

    private Dictionary<string, string> GetAttitudeToRiskList()
    {
      return db.AttitudeToRiskSelections.ToDictionary(t => t.ID.ToString(), t => t.Name);
    }

    private Dictionary<string, string> GetAttitudeToRiskCategoryList()
    {
      return db.AttitudeToRiskCategories.ToDictionary(t => t.ID.ToString(), t => t.Name);
    }

    private Dictionary<string, string> GetBusinessTypeList()
    {
      return db.BusinessTypes.ToDictionary(t => t.ID.ToString(), t => t.Name);
    }

    // GET: Client
    public ActionResult Index()
    {
      return View(db.Clients.ToList());
    }

    // GET: Client/Details/5
    public ActionResult Details(int? id)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Client client = db.Clients.Find(id);
      if(client == null) {
        return HttpNotFound();
      }

      return View(client);
    }

    // GET: Client/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: Client/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "ClientName,FirstLine,SecondLine,Town,County,PostCode")] ClientAddress clientAddress)
    {
      if(ModelState.IsValid) {
        Client clientToAdd = new Client { Name = clientAddress.ClientName };
        Address addressToAdd = new Address {
          FirstLine = clientAddress.FirstLine,
          SecondLine = clientAddress.SecondLine,
          Town = clientAddress.Town,
          County = clientAddress.County,
          PostCode = clientAddress.PostCode
        };

        clientToAdd.Address = addressToAdd;

        db.Clients.Add(clientToAdd);
        db.Adresses.Add(addressToAdd);
        db.SaveChanges();
        return RedirectToAction("Details", new { ID = clientToAdd.ID });
      }

      return View(clientAddress);
    }

    // GET: Client/Create
    public ActionResult AddIndividual(int? id)
    {
      return View();
    }


    // POST: Client/AddIndividual/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddIndividual(int? id, [Bind(Include = "Title,FirstName,Surname,Gender,BirthDate")] Person personToAdd)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      if(ModelState.IsValid) {
        Client client = db.Clients.Find(id.Value);
        personToAdd.Client = client;
        db.People.Add(personToAdd);
        client.Persons.Add(personToAdd);

        db.SaveChanges();
        return RedirectToAction("Details", new { ID = id });
      }

      return View(personToAdd);
    }


    // GET: Client/Edit/5
    public ActionResult Edit(int? id)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Client client = db.Clients.Find(id);
      if(client == null) {
        return HttpNotFound();
      }

      ClientAddress clientAddress = new ClientAddress {
        ClientName = client.Name,
        FirstLine = client.Address.FirstLine,
        SecondLine = client.Address.SecondLine,
        Town = client.Address.Town,
        County = client.Address.County,
        PostCode = client.Address.PostCode
      };
      return View(clientAddress);
    }

    // POST: Client/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int? id, [Bind(Include = "ClientName,FirstLine,SecondLine,Town,County,PostCode")] ClientAddress clientAddress)
    {
      if(ModelState.IsValid) {
        Client client = db.Clients.Find(id);
        if(client == null) {
          return HttpNotFound();
        }

        client.Name = clientAddress.ClientName;
        client.Address.FirstLine = clientAddress.FirstLine;
        client.Address.SecondLine = clientAddress.SecondLine;
        client.Address.Town = clientAddress.Town;
        client.Address.County = clientAddress.County;
        client.Address.PostCode = clientAddress.PostCode;

        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(clientAddress);
    }

    // GET: Client/Delete/5
    public ActionResult Delete(int? id)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Client client = db.Clients.Find(id);
      if(client == null) {
        return HttpNotFound();
      }
      return View(client);
    }

    // POST: Client/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Client client = db.Clients.Find(id);
      db.Clients.Remove(client);
      db.SaveChanges();
      return RedirectToAction("Index");
    }

    // GET: Client/Edit/5
    public ActionResult EditIndividual(int? id)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      Person person = db.People.Find(id);
      if(person == null) {
        return HttpNotFound();
      }

      return View(person);
    }

    // POST: Client/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditIndividual([Bind(Include = "ID,Title,FirstName,Surname,Gender,BirthDate,IsPrimary")] Person person)
    {
      if(ModelState.IsValid) {
        var dbPerson = db.People.Find(person.ID);

        dbPerson.Title = person.Title;
        dbPerson.FirstName = person.FirstName;
        dbPerson.Surname = person.Surname;
        dbPerson.BirthDate = person.BirthDate;
        dbPerson.Gender = person.Gender;
        dbPerson.IsPrimary = person.IsPrimary;

        db.SaveChanges();

        return RedirectToAction("Details", new { ID = dbPerson.Client.ID });
      }
      return View(person);
    }

    // GET: Client/DeleteIndividual/5
    public ActionResult DeleteIndividual(int? id)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      Person person = db.People.Find(id);
      if(person == null) {
        return HttpNotFound();
      }

      return View(person);
    }

    // POST: Client/DeteIndividual/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost, ActionName("DeleteIndividual")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteIndividualConfirmed(int? id)
    {
      Person person = db.People.Find(id);
      Client client = person.Client;
      db.People.Remove(person);
      db.SaveChanges();
      return RedirectToAction("Details", new { ID = client.ID });
    }



    // GET: Client/AddPersonAttitudeToRisk/5
    public ActionResult AddPersonAttitudeToRisk(int? id)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      ViewBag.AttitudeToRiskList = GetAttitudeToRiskList();
      ViewBag.AttitudeToRiskCategoryList = GetAttitudeToRiskCategoryList();

      Person person = db.People.Find(id);
      if(person == null) {
        return HttpNotFound();
      }

      return View();
    }

    // POST: Client/AddPersonAttitudeToRisk/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddPersonAttitudeToRisk(int? id, [Bind(Include = "ID,FromDate,AttitudeToRiskCategory,AttitudeToRisk")] PersonsAttitudeToRiskForView patrv)
    {
      if(ModelState.IsValid) {
        var person = db.People.Find(id);
        if(person == null) {
          return HttpNotFound();
        }

        var atrcat = db.AttitudeToRiskCategories.Find(patrv.AttitudeToRiskCategory);
        if(atrcat == null) {
          return HttpNotFound();
        }

        var atr = db.AttitudeToRiskSelections.Find(patrv.AttitudeToRisk);
        if(atr == null) {
          return HttpNotFound();
        }

        var patr = new PersonsAttitudeToRisk { Person = person, FromDate = patrv.FromDate, AttitudeToRisk = atr, AttitudeToRiskCategory = atrcat };
        person.AttitudeToRiskHistory.Add(patr);
        db.PeoplesAttitudeToRisks.Add(patr);

        db.SaveChanges();

        return RedirectToAction("EditIndividual", new { ID = id });
      }

      return View(patrv);
    }


    // GET: Client/EditJointProduct/1/5
    public ActionResult EditJointProduct(int? id, int? subid)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      if(subid == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      Client client = db.Clients.Find(id);
      if(client == null) {
        return HttpNotFound();
      }

      Product product = db.Products.Find(subid);
      if(product == null) {
        return HttpNotFound();
      }

      var productForView = new ProductForView(product);
      productForView.ClientID = id.Value;
      productForView.BusinessTypeList = new SelectList(GetBusinessTypeList(), "Key", "Value");
      return View(productForView);
    }

    // POST: Client/EditJointProduct/1/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditJointProduct(int? id, int? subid, [Bind(Include = "ID,ClientID,Name,StartDate,BusinessType,ProductFeeApplies,ProductFeePercentage")] ProductForView productForView)
    {
      if(ModelState.IsValid) {
        var product = db.Products.Find(subid);

        if(product == null) {
          return HttpNotFound();
        }

        var businessType = db.BusinessTypes.Find(productForView.BusinessType);
        if(businessType == null) {
          return HttpNotFound();
        }

        product.Name = productForView.Name;
        product.StartDate = productForView.StartDate;
        product.BusinessType = businessType;

        if(product.ProductFeeAttached) {
          // Thre is a product fee to remove
          product.ProductFeeAttached = false;
          var productFeeToRemove = product.ProductFee;
          db.ProductFees.Remove(productFeeToRemove);
        }

        if(productForView.ProductFeeApplies) {
          product.ProductFeeAttached = true;
          var productFee = new ProductFee { Product = product, Percentage = productForView.ProductFeePercentage };
          product.ProductFee = productFee;
          db.ProductFees.Add(productFee);
        }

        db.SaveChanges();

        return RedirectToAction("Details", new { ID = productForView.ClientID });
      }

      return View(productForView);
    }

    // GET: Client/EditIndividualProduct/1/5
    public ActionResult EditIndividualProduct(int? id, int? subid)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      if(subid == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      Person person = db.People.Find(id);
      if(person == null) {
        return HttpNotFound();
      }

      Product product = db.Products.Find(subid);
      if(product == null) {
        return HttpNotFound();
      }

      var productForView = new ProductForView(product);
      productForView.ClientID = person.Client.ID;
      productForView.PersonID = person.ID;
      productForView.BusinessTypeList = new SelectList(GetBusinessTypeList(), "Key", "Value");
      return View(productForView);
    }

    // POST: Client/EditIndividualProduct/1/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditIndividualProduct(int? id, int? subid, [Bind(Include = "ID,ClientID,PersonID,Name,StartDate,BusinessType,ProductFeeApplies,ProductFeePercentage")] ProductForView productForView)
    {
      if(ModelState.IsValid) {
        var product = db.Products.Find(subid);

        if(product == null) {
          return HttpNotFound();
        }

        var businessType = db.BusinessTypes.Find(productForView.BusinessType);
        if(businessType == null) {
          return HttpNotFound();
        }

        product.Name = productForView.Name;
        product.StartDate = productForView.StartDate;
        product.BusinessType = businessType;

        if(product.ProductFeeAttached) {
          // Thre is a product fee to remove
          product.ProductFeeAttached = false;
          var productFeeToRemove = product.ProductFee;
          db.ProductFees.Remove(productFeeToRemove);
        }

        if(productForView.ProductFeeApplies) {
          product.ProductFeeAttached = true;
          var productFee = new ProductFee { Product = product, Percentage = productForView.ProductFeePercentage };
          product.ProductFee = productFee;
          db.ProductFees.Add(productFee);
        }

        db.SaveChanges();

        return RedirectToAction("EditIndividual", new { ID = productForView.PersonID });
      }

      return View(productForView);
    }

    // GET: Client/AddJointProduct/5
    public ActionResult AddJointProduct(int? id)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      Client client = db.Clients.Find(id);
      if(client == null) {
        return HttpNotFound();
      }

      ViewBag.BusinessTypeList = GetBusinessTypeList();

      return View();
    }

    // POST: Client/AddJointProduct/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddJointProduct([Bind(Include = "ID,ClientID,Name,StartDate,BusinessType")] ProductForView productForView)
    {
      if(ModelState.IsValid) {
        Client client = db.Clients.Find(productForView.ClientID);
        if(client == null) {
          return HttpNotFound();
        }

        var businessType = db.BusinessTypes.Find(productForView.BusinessType);
        if(businessType == null) {
          return HttpNotFound();
        }

        var product = new Product {
          Name = productForView.Name,
          Client = client,
          BusinessType = businessType,
          StartDate = productForView.StartDate
        };

        db.Products.Add(product);
        client.Products.Add(product);

        db.SaveChanges();

        return RedirectToAction("Details", new { ID = productForView.ClientID });
      }

      return View(productForView);
    }

    // GET: Client/AddIndividualProduct/5
    public ActionResult AddIndividualProduct(int? id)
    {
      if(id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      Person person = db.People.Find(id);
      if(person == null) {
        return HttpNotFound();
      }

      ViewBag.BusinessTypeList = GetBusinessTypeList();

      return View();
    }


    // POST: Client/AddIndividualProduct/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddIndividualProduct([Bind(Include = "ID,PersonID,Name,StartDate,BusinessType")] ProductForView productForView)
    {
      if(ModelState.IsValid) {
        Person person = db.People.Find(productForView.PersonID);
        if(person == null) {
          return HttpNotFound();
        }

        var businessType = db.BusinessTypes.Find(productForView.BusinessType);
        if(businessType == null) {
          return HttpNotFound();
        }

        var product = new Product {
          Name = productForView.Name,
          Client = person.Client,
          Person = person,
          BusinessType = businessType,
          StartDate = productForView.StartDate
        };

        db.Products.Add(product);
        person.PersonProducts.Add(product);

        db.SaveChanges();

        return RedirectToAction("EditIndividual", new { ID = person.ID });
      }

      return View(productForView);
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
