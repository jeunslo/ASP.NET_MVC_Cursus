using MVCBierenApplication.Models;
using MVCBierenApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCBierenApplication.Controllers
{
    public class BierController : Controller
    {
        // GET: Bier
        private AdresService adresService = new AdresService();
        private BierService bierService = new BierService();
        public ActionResult Index()
        {
            var adres = adresService.GetAdres();
            ViewBag.Adres = adres;
            //List<Bier> bieren = new List<Bier>();
            //bieren.Add(new Bier { ID = 1, Naam = "Cnudde kriek", Alcohol = 4.7f });
            //bieren.Add(new Bier { ID = 2, Naam = "Liefmans goudenband", Alcohol = 8f });
            //bieren.Add(new Bier { ID = 3, Naam = "Sloeber", Alcohol = 7.5f });
            //bieren.Add(new Bier { ID = 4, Naam = "Felix kriekbier", Alcohol = 5f });
            List<Bier> bieren = bierService.GetBieren();
            return View(bieren);
        }

        public ActionResult Verwijderen(int id)
        {
            Bier bier = bierService.Read(id);
            return View(bier);
        }

        [HttpPost]
        public ActionResult Verwijderd(int id)
        {
            Bier bier = bierService.Read(id);
            this.TempData["VerwijderdBier"] = bier;
            bierService.Delete(id);
            //return View(bier);
            return RedirectToAction("Delete");
        }

        public ActionResult Delete()
        {
            Bier bier = (Bier)this.TempData["VerwijderdBier"];
            return View(bier);
        }
    }
}