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
        
        public ActionResult Index(int? id)
        {
            var vm = new HomeIndexViewModel();
            vm.GenreList = cultuurService.GetGenres();
            if (id != null)
            {
                var chosenGenre = cultuurService.FindGenreById(id);
                if (chosenGenre != null)
                {
                    vm.Genre = chosenGenre;
                    vm.VoorstellingsList = cultuurService.FindVoorstellingenByGenreNr(chosenGenre.GenreNr);
                }
            }
            return View(vm);
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
            //var aantalPlaatsen = uint.Parse(Request["aantalPlaatsen"]);
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
            List<Voorstelling> voorstellingenList = new List<Voorstelling>();
            foreach(string number in Session)
            {
                int voorstellingsNr;
                if (int.TryParse(number, out voorstellingsNr))
                {
                    var voorstelling = cultuurService.FindVoorstellingById(voorstellingsNr);
                    voorstellingenList.Add(voorstelling);
                }
            }
            return View(voorstellingenList);
        }

        [HttpPost]
        public ActionResult VerwijderVoorstellingMandje(IEnumerable<int> toDeleteList)
        {
            if ((toDeleteList.Count() != 0) && (toDeleteList.Count() != null))
            {
                foreach (var item in toDeleteList)
                {
                    Session[item.ToString()] = null;
                }
            }
            return RedirectToAction("Mandje", "Home");
        }
    }
}