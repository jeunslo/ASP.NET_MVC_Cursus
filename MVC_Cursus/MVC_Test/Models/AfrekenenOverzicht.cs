using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Test.Models
{
    public class AfrekenenOverzicht
    {
        public List<Film> FilmList { get; set; }
        public decimal Prijs { get; set; }
        public string Naam { get; set; }
        public string Straat { get; set; }
        public string Gemeente { get; set; }
    }
}