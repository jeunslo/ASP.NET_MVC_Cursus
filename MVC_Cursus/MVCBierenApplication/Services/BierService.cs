using MVCBierenApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBierenApplication.Services
{
    public class BierService
    {
        private static List<Bier> bieren = new List<Bier>();
        static BierService()
        {        
            bieren.Add(new Bier { ID = 1, Naam = "Cnudde kriek", Alcohol = 4.7f });
            bieren.Add(new Bier { ID = 2, Naam = "Liefmans goudenband", Alcohol = 8f });
            bieren.Add(new Bier { ID = 3, Naam = "Sloeber", Alcohol = 7.5f });
            bieren.Add(new Bier { ID = 4, Naam = "Felix kriekbier", Alcohol = 5f });
        }

        public List<Bier> GetBieren()
        {
            return bieren;
        }

        public Bier Read(int id)
        {
            return bieren[id-1];
        }

        public void Delete(int id)
        {
            Bier bier = bieren.Where(x => x.ID == id).FirstOrDefault();
            bieren.Remove(bier);
        }
    }
}