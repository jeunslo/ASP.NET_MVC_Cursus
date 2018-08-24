using MVC_Voorbeeld2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Voorbeeld2.Controllers
{
    public class WerknemerController : Controller
    {
        // GET: Werknemer
        [NonAction]
        public void GeenAction()
        {

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read(int? id)
        {
            return Content("Kiekeboe");
        }

        [ActionName("Lijst")]
        public ActionResult AlleWerknemers()
        {
            var werknemers = new List<Werknemer>();
            werknemers.Add(new Werknemer { Voornaam = "Steven", Wedde = 1000, InDienst = DateTime.Today });
            werknemers.Add(new Werknemer { Voornaam = "Prosper", Wedde = 2000, InDienst = DateTime.Today.AddDays(2) });
            return View("AlleWerknemers", werknemers);

        }
    }
}