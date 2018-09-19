using MVC_Cultuurhuis.Models;
using MVC_Cultuurhuis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Cultuurhuis.Controllers
{
    public class HomeController : Controller
    {
        public CultuurServices cultuurService = new CultuurServices();

        public ActionResult Index(int? id = null)
        {
            if (Session.Keys.Count != 0)
                ViewBag.Mandje = true;
            else
                ViewBag.Mandje = false;
            return View(id);
        }

        public PartialViewResult GenreLijst(int? id)
        {
            ViewBag.Id = id;
            return PartialView(cultuurService.GetGenres());
        }

        public PartialViewResult GetVoorstellingenGenre(int? id)
        {
            List<Voorstelling> voorstellingList = new List<Voorstelling>();
            var chosenGenre = cultuurService.FindGenreById(id);
            if (chosenGenre != null)
            {
                voorstellingList = cultuurService.FindVoorstellingenByGenreNr(chosenGenre.GenreNr);
            }
            return PartialView(voorstellingList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Reserveren(int id)
        {
            var voorstelling = cultuurService.FindVoorstellingById(id);
            return View(voorstelling);
        }

        [HttpPost]
        public ActionResult Reserveer(int voorstellingsNr, int aantalPlaatsen)
        {
            var voorstelling = cultuurService.FindVoorstellingById(voorstellingsNr);
            if (voorstelling.VrijePlaatsen < aantalPlaatsen)
            {
                return RedirectToAction("Reserveren", "Home", new { id = voorstellingsNr });
            }
            else
            {
                Session[voorstellingsNr.ToString()] = aantalPlaatsen;
                return RedirectToAction("Mandje", "Home");
            }
        }

        public ActionResult Mandje()
        {
            List<Voorstelling> voorstellingenList = GetVoorstellingFromSession();
            ViewBag.Prijs = 0;
            return View(voorstellingenList);
        }

        public PartialViewResult PartialVerwijderMandje(FormCollection form)
        {
            if (form != null)
            {
                foreach (var item in form.AllKeys)
                {
                    int number;
                    if (int.TryParse(item, out number))
                    {
                        Session.Remove(number.ToString());
                    }
                }
            }
            List<Voorstelling> voorstellingenList = GetVoorstellingFromSession();
            ViewBag.Prijs = 0;
            return PartialView(voorstellingenList);
        }

        //[HttpPost]
        //public ActionResult VerwijderVoorstellingMandje(FormCollection form)
        //{
        //    if (form != null)
        //    {
        //        foreach (var item in form.AllKeys)
        //        {
        //            int number;
        //            if (int.TryParse(item, out number))
        //            {
        //                Session.Remove(number.ToString());
        //            }
        //        }
        //    }
        //    List<Voorstelling> voorstellingenList = GetVoorstellingFromSession();
        //    ViewBag.Prijs = 0;
        //    return View("Mandje", voorstellingenList);
        //}

        public ActionResult Kassa()
        {
            InloggegevensVM login = new InloggegevensVM();
            ViewBag.Nietgevonden = false;
            return View(login);
        }

        [HttpPost]
        public ActionResult Opzoeken(InloggegevensVM login)
        {
            Klant klant = cultuurService.GetKlantFromUserAndPass(login);
            if (klant != null)
            {
                ViewBag.Nietgevonden = false;
                Session["klant"] = klant;
            }
            else
            {
                ViewBag.Nietgevonden = true;
            }
            return View("Kassa", login);
        }


        public ActionResult Registreer()
        {
            NieuweKlantVM klant = new NieuweKlantVM();
            return View(klant);
        }

        [HttpPost]
        public ActionResult Registreren(NieuweKlantVM klant)
        {
            if(ModelState.IsValid)
            {
                Klant nieuweKlant = new Klant
                {
                    Voornaam = klant.Voornaam,
                    Familienaam = klant.Familienaam,
                    Straat = klant.Straat,
                    HuisNr = klant.HuisNr,
                    Postcode = klant.Postcode,
                    Gemeente = klant.Gemeente,
                    GebruikersNaam = klant.GebruikersNaam,
                    Paswoord = klant.Paswoord
                };
                cultuurService.AddKlant(nieuweKlant);
                Klant deKlant = cultuurService.FindKlantByGebruikersnaam(klant.GebruikersNaam);
                Session["klant"] = deKlant;
                InloggegevensVM login = new InloggegevensVM();
                return View("Kassa", login);
            }
            else
            {
                return View("Registreer", klant);
            }
        }

        public ActionResult Bevestigen()
        {
            List<Voorstelling> voorstellingList = GetVoorstellingFromSession();
            List<Reservatie> reservatieList = new List<Reservatie>();
            foreach (var voorstelling in voorstellingList)
            {
                Reservatie reservatie = new Reservatie
                {
                    KlantNr = ((Klant)Session["klant"]).KlantNr,
                    VoorstellingsNr = voorstelling.VoorstellingsNr,
                    Plaatsen = short.Parse(Session[voorstelling.VoorstellingsNr.ToString()].ToString())
                };
                reservatieList.Add(reservatie);
            }
            ReservatieStatus status = cultuurService.MakeReservations(reservatieList);
            foreach (var item in status.GelukteReservaties)
            {
                Session.Remove(item.VoorstellingsNr.ToString());
            }
            return View(status);
        }

        //**************Methods********************************
        private List<Voorstelling> GetVoorstellingFromSession()
        {
            List<Voorstelling> voorstellingenList = new List<Voorstelling>();
            foreach (string number in Session)
            {
                int voorstellingsNr;
                if (int.TryParse(number, out voorstellingsNr))
                {
                    var voorstelling = cultuurService.FindVoorstellingById(voorstellingsNr);
                    voorstellingenList.Add(voorstelling);
                }
            }
            return voorstellingenList;
        }

        public JsonResult ValidateDoubleUsername(string GebruikersNaam)
        {
            Klant klant = cultuurService.FindKlantByGebruikersnaam(GebruikersNaam);
            if (klant != null)
                return Json("Een klant met deze gebruikersnaam komt al voor in de database.Kies een andere naam.", JsonRequestBehavior.AllowGet);
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}