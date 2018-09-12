using MVC_Cultuurhuis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MVC_Cultuurhuis.Services
{
    public class CultuurServices
    {
        private static List<Genre> genreList = new List<Genre>();
        
        public CultuurServices()
        {
            using (var entities = new CultuurHuisMVCEntities())
            {
                var query = (from Genre in entities.Genres orderby Genre.GenreNaam select Genre).ToList();
                genreList = query;
            }
        }

        public List<Genre> GetGenres()
        {
            return genreList;
        }

        public Genre FindGenreById(int? id)
        {
            var genre = new Genre();
            if (id != null)
                genre = genreList.FirstOrDefault(g => g.GenreNr == id);
            return genre;
        }

        public List<Voorstelling> FindVoorstellingenByGenreNr(int id)
        {
            using (var entities = new CultuurHuisMVCEntities())
            {
                List<Voorstelling> voorstellingen = new List<Voorstelling>();
                var genre = entities.Genres.Find(id);
                if (genre != null)
                {
                    voorstellingen = (genre.Voorstellingen.OrderBy(x => x.Datum).Where(x => x.Datum >= DateTime.Today)).ToList();
                }
                return voorstellingen;
            }         
        }

        public Voorstelling FindVoorstellingById(int id)
        {
            using (var entities = new CultuurHuisMVCEntities())
            {
                var voorstelling = entities.Voorstellingen.Find(id);
                return voorstelling;
            }
        }
    }
}