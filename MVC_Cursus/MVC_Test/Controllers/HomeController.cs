using MVC_Test.Filters;
using MVC_Test.Models;
using MVC_Test.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Test.Controllers
{
    public class HomeController : Controller
    {
        private VideoVerhuurServices service = new VideoVerhuurServices();
        public ActionResult Index()
        {
            return View();
        }

        //****Ajax2*********
        public ActionResult InloggenAjax2(string naam, int? postcode)
        {
            Klant klant = null;
            klant = service.FindKlantByNaamPostcode2(naam, postcode);
            if (klant != null)
            {
                Session["user"] = klant;
                return PartialView("VerhuurLink");
            }
            if (klant == null && Request["Aanmelden"] != null)
                return PartialView("FoutieveLink");
            else
                return PartialView();
        }

        public PartialViewResult VerhuurLink()
        {
            return PartialView();
        }

        public PartialViewResult FoutieveLink()
        {
            return PartialView();
        }

        public ActionResult Uitloggen()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        [AuthorizeActionFilter]
        public ActionResult GenreView()
        {
            return View();
        }

        public PartialViewResult GenreList()
        {
            List<Genre> genreList = service.GetGenreList();
            return PartialView(genreList);
        }

        public ActionResult GenreDetail(int? gekozenGenreNr)
        {
            Genre selectedGenre = service.GetGenreByNr(gekozenGenreNr);
            if (selectedGenre != null)
            {
                ViewBag.GenreSoort = selectedGenre.GenreSoort;
            }
            List<Film> filmList = service.GetFilmListFromGenreNr(gekozenGenreNr);
            if (Request.IsAjaxRequest())
            {
                return PartialView(filmList);
            }
            else
                return View(filmList);
        }

        [AuthorizeActionFilter]
        public ActionResult Mandje()
        {
            return View();
        }

        public PartialViewResult WinkelMandje(int? bandNr)
        {
            Film film = service.FindFilmByBandNr(bandNr);
            if (film != null)
            {
                if (Session[bandNr.ToString()] != null)
                {
                    Session[bandNr.ToString()] = (int)Session[bandNr.ToString()] + 1;
                }
                else
                    Session[bandNr.ToString()] = 1;
            }
            List<Film> filmList = GetFilmsFromSession();
            if (filmList.Count > 0)
                Session["mandje"] = true;
            else
                Session.Remove("mandje");
            return PartialView(filmList);
        }

        [AuthorizeActionFilter]
        public ActionResult VerwijderFilmBevestiging(int bandNr)
        {
            Film film = service.FindFilmByBandNr(bandNr);
            if (Request.IsAjaxRequest())
                return PartialView(film);
            else
                return View(film);
        }

        public ActionResult VerwijderFilm(int bandNr)
        {
            Session.Remove(bandNr.ToString());
            return RedirectToAction("Mandje");
        }

        public ActionResult AfrekenenOverzicht()
        {
            AfrekenenOverzicht afrekenenGegevens = new AfrekenenOverzicht();

            Klant klant = Session["user"] as Klant;
            afrekenenGegevens.Naam = klant.Voornaam + " " + klant.Naam;
            afrekenenGegevens.Straat = klant.Straat_Nr;
            afrekenenGegevens.Gemeente = klant.PostCode.ToString() + " " + klant.Gemeente;

            List<Film> filmList = GetFilmsFromSession();


            decimal prijs = filmList.Sum(x => x.Prijs);
            afrekenenGegevens.Prijs = prijs;

            List<Film> gewijzigdeFilms = service.wijzigDb(filmList, klant.KlantNr);
            afrekenenGegevens.FilmList = gewijzigdeFilms;

            Session.Clear();
            return View(afrekenenGegevens);
        }

        //**************Methods******************
        private List<Film> GetFilmsFromSession()
        {
            List<Film> filmList = new List<Film>();
            foreach (string nr in Session)
            {
                int number;
                if (int.TryParse(nr, out number))
                {
                    Film sessionFilm = service.FindFilmByBandNr(number);
                    filmList.Add(sessionFilm);
                }
            }
            return filmList;
        }
    }
}