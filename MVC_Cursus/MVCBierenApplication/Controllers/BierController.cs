using MVCBierenApplication.Models;
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
        public ActionResult Index()
        {
            List<Bier> bieren = new List<Bier>();
            bieren.Add(new Bier { ID = 1, Naam = "Tripple", Alcohol = 0.5f });
            bieren.Add(new Bier { ID = 1, Naam = "Pils", Alcohol = 0.2f });

            return View(bieren);
        }
    }
}