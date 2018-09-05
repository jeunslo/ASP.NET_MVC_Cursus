using MVCBierenApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBierenApplication.Services
{
    public class BierService
    {
        public List<Bier> GetBieren()
        {
            using (var db = new MVCBierenEntities())
            {
                return db.Bieren.ToList();
            }
        }

        public Bier Read(int? id)
        {
            using (var db = new MVCBierenEntities())
            {
                return db.Bieren.Find(id);
            }
        }
    

        public void Delete(int id)
        {
            using (var db = new MVCBierenEntities())
            {
                Bier bier = db.Bieren.Find(id);
                db.Bieren.Remove(bier);
                db.SaveChanges();
            }
        }


        public void Add(Bier bier)
        {
            using (var db = new MVCBierenEntities())
            {
                db.Bieren.Add(bier);
                db.SaveChanges();
            }
        }
    }
}