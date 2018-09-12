using MVCBierenSecurityAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBierenSecurityAPP.Services
{
    public class AdresService
    {
        private static Adres adres = new Adres() { Naam = "De Biertempel",
                                                   Straat = "Hoppestraat",
                                                   HuisNr = 103,
                                                   Gemeente = "Bierbeek",
                                                   PostCode = "3360",
                                                   TelefoonNr = "016123456",
                                                   EMail = "info@biertempel.be" };
        
        public Adres GetAdres()
        {
            return adres;
        }
    }
}