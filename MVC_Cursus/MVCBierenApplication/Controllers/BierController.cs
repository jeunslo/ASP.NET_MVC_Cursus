using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCBierenApplication.Models;
using MVCBierenApplication.Services;

namespace MVCBierenApplication.Controllers
{
    public class BierController : Controller
    {
        private MVCBierenEntities db = new MVCBierenEntities();
        private BierService bierService = new BierService();

        // GET: Bier
        public ActionResult Index()
        {
            var bieren = bierService.GetBieren();
            return View(bieren);
        }

        // GET: Bier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bier bier = bierService.Read(id);
            if (bier == null)
            {
                return HttpNotFound();
            }
            return View(bier);
        }

        // GET: Bier/Create
        public ActionResult Create()
        {
            ViewBag.BrouwerNr = new SelectList(db.Brouwers, "BrouwerNr", "BrNaam");
            ViewBag.SoortNr = new SelectList(db.Soorten, "SoortNr", "Naam");
            return View();
        }

        // POST: Bier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BierNr,Naam,BrouwerNr,SoortNr,Alcohol")] Bier bier)
        {
            if (ModelState.IsValid)
            {
                bierService.Add(bier);
                return RedirectToAction("Index");
            }

            ViewBag.BrouwerNr = new SelectList(db.Brouwers, "BrouwerNr", "BrNaam", bier.BrouwerNr);
            ViewBag.SoortNr = new SelectList(db.Soorten, "SoortNr", "Naam", bier.SoortNr);
            return View(bier);
        }

        // GET: Bier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bier bier = bierService.Read(id);
            if (bier == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrouwerNr = new SelectList(db.Brouwers, "BrouwerNr", "BrNaam", bier.BrouwerNr);
            ViewBag.SoortNr = new SelectList(db.Soorten, "SoortNr", "Naam", bier.SoortNr);
            return View(bier);
        }

        // POST: Bier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BierNr,Naam,BrouwerNr,SoortNr,Alcohol")] Bier bier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrouwerNr = new SelectList(db.Brouwers, "BrouwerNr", "BrNaam", bier.BrouwerNr);
            ViewBag.SoortNr = new SelectList(db.Soorten, "SoortNr", "Naam", bier.SoortNr);
            return View(bier);
        }

        // GET: Bier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bier bier = bierService.Read(id);
            if (bier == null)
            {
                return HttpNotFound();
            }
            return View(bier);
        }

        // POST: Bier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bierService.Delete(id);
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
