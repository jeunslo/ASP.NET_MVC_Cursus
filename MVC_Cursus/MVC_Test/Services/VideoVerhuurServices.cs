using MVC_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Test.Services
{
    public class VideoVerhuurServices
    {
        public Klant FindKlantByNaamPostcode(string naam, int postcode)
        {
            using (var entities = new VideoVerhuurEntities())
            {
                var query = (from Klant in entities.Klanten
                             where (Klant.Naam.ToUpper() == naam.ToUpper()) && (Klant.PostCode == postcode)
                             select Klant).FirstOrDefault();
                return query;
            }
        }

        public Klant FindKlantByNaamPostcode2(string naam, int? postcode)
        {
            using (var entities = new VideoVerhuurEntities())
            {
                Klant query = null;
                if (postcode != null)
                {
                    query = (from Klant in entities.Klanten
                                 where (Klant.Naam.ToUpper() == naam.ToUpper()) && (Klant.PostCode == postcode)
                                 select Klant).FirstOrDefault();
                }
                return query;
            }
        }

        public List<Genre> GetGenreList()
        {
            using (var entities = new VideoVerhuurEntities())
            {
                var query = (from Genre in entities.Genres
                            orderby Genre.GenreSoort
                            select Genre).ToList();
                return query;
            }
        }

        public List<Film> GetFilmListFromGenreNr(int? genreNr)
        {
            using (var entities = new VideoVerhuurEntities())
            {
                var query = (from Film in entities.Films
                             where Film.GenreNr == genreNr
                             orderby Film.Titel
                             select Film).ToList();
                return query;
            }
        }

        public Genre GetGenreByNr(int? nr)
        {
            using (var entities = new VideoVerhuurEntities())
            {
                return entities.Genres.Find(nr);
            }
        }

        public Film FindFilmByBandNr(int? nr)
        {
            using (var entities = new VideoVerhuurEntities())
            {
                return entities.Films.Find(nr);
            }
        }

        public List<Film> wijzigDb(List<Film> filmList, int klantNr)
        {
            using (var entities = new VideoVerhuurEntities())
            {
                List<Film> gewijzigdeFilms = new List<Film>();
                foreach (var item in filmList)
                {
                    Film film = entities.Films.Find(item.BandNr);
                    if (film.InVoorraad > 0)
                    {
                        film.InVoorraad -= 1;
                        film.UitVoorraad += 1;
                        Verhuur verhuur = new Verhuur
                        {
                            KlantNr = klantNr,
                            BandNr = item.BandNr
                        };
                        gewijzigdeFilms.Add(film);
                        entities.Verhuur.Add(verhuur);
                    }
                }
                entities.SaveChanges();
                return gewijzigdeFilms;
            }
        }
    }
}