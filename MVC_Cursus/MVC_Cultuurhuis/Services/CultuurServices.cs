using MVC_Cultuurhuis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MVC_Cultuurhuis.Services
{
    public class CultuurServices
    {
        public List<Genre> GetGenres()
        {
            using (var entities = new CultuurHuisMVCEntities())
            {
                var query = (from Genre in entities.Genres orderby Genre.GenreNaam select Genre).ToList();
                
                return query;
            }
            
        }

        public Genre FindGenreById(int? id)
        {
            using (var entities = new CultuurHuisMVCEntities())
            {
                return entities.Genres.Find(id);
            }              
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

        public Klant GetKlantFromUserAndPass(InloggegevensVM login)
        {
            using (var entities = new CultuurHuisMVCEntities())
            {
                var query = (from klant in entities.Klanten
                            where (klant.GebruikersNaam == login.UserName) && (klant.Paswoord == login.Password)
                            select klant).FirstOrDefault();
                return query;
            }
        }

        public void AddKlant(Klant klant)
        {
            using (var entities = new CultuurHuisMVCEntities())
            {
                
                entities.Klanten.Add(klant);
                entities.SaveChanges();
            }
        }

        public Klant FindKlantByGebruikersnaam(string naam)
        {
            using (var entities = new CultuurHuisMVCEntities())
            {
                var query = (from klant in entities.Klanten
                             where klant.GebruikersNaam == naam
                             select klant).FirstOrDefault();
                return query;
            }
        }

        public ReservatieStatus MakeReservations(List<Reservatie> reservatieList)
        {
            List<Voorstelling> gelukteList = new List<Voorstelling>();
            List<Voorstelling> mislukteList = new List<Voorstelling>();
            using (var entities = new CultuurHuisMVCEntities())
            {
                foreach (var reservatie in reservatieList)
                {
                    var voorstelling = entities.Voorstellingen.Find(reservatie.VoorstellingsNr);
                    if (voorstelling.VrijePlaatsen >= reservatie.Plaatsen)
                    {
                        voorstelling.VrijePlaatsen -= reservatie.Plaatsen;
                        gelukteList.Add(voorstelling);
                        entities.Reservaties.Add(reservatie);
                        entities.SaveChanges();
                    }
                    else
                    {
                        mislukteList.Add(voorstelling);
                    }

                }
                ReservatieStatus status = new ReservatieStatus
                {
                    GelukteReservaties = gelukteList,
                    MislukteReservaties = mislukteList,
                    Reservaties = reservatieList
                };
                return status;
            }
            
        }
    }
}