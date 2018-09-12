using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCBierenSecurityAPP.Models;

namespace MVCBierenSecurityAPP.Controllers
{
    public class BierController : Controller
    {
        private MVCBierenEntities db = new MVCBierenEntities();

        // GET: Bier
        public ActionResult Index()
        {
            return View(db.Bieren.ToList());
        }

        // GET: Bier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bieren bieren = db.Bieren.Find(id);
            if (bieren == null)
            {
                return HttpNotFound();
            }
            return View(bieren);
        }

        // GET: Bier/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BierNr,Naam,BrouwerNr,SoortNr,Alcohol")] Bieren bieren)
        {
            if (ModelState.IsValid)
            {
                db.Bieren.Add(bieren);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bieren);
        }

        // GET: Bier/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bieren bieren = db.Bieren.Find(id);
            if (bieren == null)
            {
                return HttpNotFound();
            }
            return View(bieren);
        }

        // POST: Bier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BierNr,Naam,BrouwerNr,SoortNr,Alcohol")] Bieren bieren)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bieren).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bieren);
        }

        // GET: Bier/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bieren bieren = db.Bieren.Find(id);
            if (bieren == null)
            {
                return HttpNotFound();
            }
            return View(bieren);
        }

        // POST: Bier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bieren bieren = db.Bieren.Find(id);
            db.Bieren.Remove(bieren);
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
